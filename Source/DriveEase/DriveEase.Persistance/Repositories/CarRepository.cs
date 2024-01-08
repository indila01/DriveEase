using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;

namespace DriveEase.Persistance.Repositories;

/// <inheritdoc/>
public sealed class CarRepository : GenericRepository<Car>, ICarRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public CarRepository(DriveEaseDbContext dbContext)
        : base(dbContext)
    {
    }
}
