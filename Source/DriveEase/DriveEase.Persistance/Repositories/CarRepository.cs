using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;

namespace DriveEase.Persistance.Repositories;
internal sealed class CarRepository : ICarRepository
{
    public Task<Car?> GetByIdAsyn(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
