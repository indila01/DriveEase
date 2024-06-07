using DriveEase.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DriveEase.Persistance.Interceptors;

/// <summary>
/// soft delete interceptor
/// </summary>
internal sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            DateTime utcNow = DateTime.UtcNow;
            var entities = eventData
                .Context.ChangeTracker.Entries<ISoftDeletableEntity>()
                .Where(x => x.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeletableEntity> entity in entities)
            {
                entity.Property(nameof(ISoftDeletableEntity.DeletedDate)).CurrentValue = utcNow;
                entity.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;
                entity.State = EntityState.Modified;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
