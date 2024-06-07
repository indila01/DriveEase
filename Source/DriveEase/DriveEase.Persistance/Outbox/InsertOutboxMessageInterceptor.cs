using DriveEase.Domain.Entities;
using DriveEase.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace DriveEase.Persistance;

public class InsertOutboxMessageInterceptor : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData is not null)
        {
            insertOutboxMessages(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void insertOutboxMessages(DbContext context)
    {
        var utcNow = DateTime.UtcNow;
        var outboxMessages = context
               .ChangeTracker
               .Entries<BaseEntity>()
               .Select(entry => entry.Entity)
               .SelectMany(entity =>
                {
                    List<IDomainEvent> domainEvents = entity.DomainEvents.ToList();
                    entity.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage(
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings),
                    utcNow))
                .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}
