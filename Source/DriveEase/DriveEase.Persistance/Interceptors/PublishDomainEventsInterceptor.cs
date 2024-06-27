using DriveEase.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DriveEase.Persistance;

/// <summary>
///  publish domain event inceptor
/// </summary>
public class PublishDomainEventsInterceptor(IPublisher publisher)
 : SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await this.publishDomainEventsAsync(eventData.Context);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// publish domain events async
    /// </summary>
    /// <param name="context">db contenxt</param>
    /// <returns>task </returns>
    private async Task publishDomainEventsAsync(DbContext context)
    {
        var domainEvents = context
        .ChangeTracker
        .Entries<BaseEntity>()
        .Select(entry => entry.Entity)
        .SelectMany(entity =>
         {
             var domainEvents = entity.DomainEvents;
             entity.ClearDomainEvents();
             return domainEvents;
         });

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}