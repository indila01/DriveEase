using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DriveEase.Persistance.EFCustomizations;

/// <inheritdoc/>
public class CarRepository : BaseRepository<Car>, ICarRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public CarRepository(DriveEaseDbContext dbContext)
         : base(dbContext)
    {
    }

    /// <inheritdoc/>
    public async Task<Car> GetCarByModelAsync(string model, CancellationToken cancellationToken = default)
        => await this.dbContext.Cars?
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Model == model, cancellationToken);
}
