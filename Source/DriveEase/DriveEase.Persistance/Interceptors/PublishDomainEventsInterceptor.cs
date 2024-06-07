﻿using DriveEase.Domain.Entities;
using DriveEase.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DriveEase.Persistance;

public class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
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

    private async Task publishDomainEventsAsync(DbContext context)
    {

        var domainEvents = context
        .ChangeTracker
        .Entries<BaseEntity>()
        .Select(entry => entry.Entity)
        .SelectMany(entity =>
         {
             List<IDomainEvent> domainEvents = entity.DomainEvents.ToList();
             entity.ClearDomainEvents();
             return domainEvents;
         });

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}