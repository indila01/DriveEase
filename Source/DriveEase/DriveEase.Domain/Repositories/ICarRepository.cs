using DriveEase.Domain.Entities;
namespace DriveEase.Domain.Repositories;

public interface ICarRepository
{
    Task<Car?> GetByIdAsyn(Guid id, CancellationToken cancellationToken = default);
}
