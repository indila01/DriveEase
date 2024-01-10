using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DriveEase.Persistance.EFCustomizations;

/// <summary>
/// db context
/// </summary>
public class DriveEaseDbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// The date time.
    /// </summary>
    private readonly IDateTime dateTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="DriveEaseDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="dateTime">The date time.</param>
    public DriveEaseDbContext(DbContextOptions<DriveEaseDbContext> options, IDateTime dateTime)
        : base(options)
    {
        this.dateTime = dateTime;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriveEaseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Gets or sets the cars.
    /// </summary>
    /// <value>
    /// The cars.
    /// </value>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>
    /// The users.
    /// </value>
    public DbSet<User> Users { get; set; }

    /// <inheritdoc/>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime utcNow = this.dateTime.UtcNow;

        this.UpdateAuditableEntities(utcNow);
        this.UpdateSoftDeletableEntities(utcNow);
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the specified entity entry's referenced entries in the deleted state to the modified state.
    /// This method is recursive.
    /// </summary>
    /// <param name="entityEntry">The entity entry.</param>
    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }

    /// <summary>
    /// Updates the entities implementing <see cref="IAuditableEntity"/> interface.
    /// </summary>
    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedDate)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.UpdatedDate)).CurrentValue = utcNow;
            }
        }
    }

    /// <summary>
    /// Updates the entities implementing <see cref="ISoftDeletableEntity"/> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted)
            {
                continue;
            }

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedDate)).CurrentValue = utcNow;

            entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }
}