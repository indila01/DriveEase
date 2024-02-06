using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;

namespace DriveEase.Domain.Repositories;

/// <summary>
/// car repository.
/// </summary>
public interface ICarRepository : IBaseRepository<Car>
{
    /// <summary>
    /// Gets the car by model asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>car.</returns>
    Task<Car> GetCarByModelAsync(string model, CancellationToken cancellationToken = default);
}
