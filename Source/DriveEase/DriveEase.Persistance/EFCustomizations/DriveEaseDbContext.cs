using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DriveEase.Persistance.EFCustomizations;

/// <summary>
/// db context
/// </summary>
public class DriveEaseDbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DriveEaseDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="dateTime">The date time.</param>
    public DriveEaseDbContext(DbContextOptions<DriveEaseDbContext> options)
        : base(options)
    {
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
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Car>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriveEaseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}