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
    /// <returns>Car.</returns>
    Task<Car> GetCarByModelAsync(string model);
}
