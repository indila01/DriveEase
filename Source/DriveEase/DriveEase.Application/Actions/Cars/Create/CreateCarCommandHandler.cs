using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Create;

/// <summary>
/// Create car command handler.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CreateCarCommandHandler"/> class.
/// </remarks>
/// <param name="carRepository">The car repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
public class CreateCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<CreateCarCommand, Result<Guid>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private readonly ICarRepository carRepository = carRepository;

    /// <summary>
    /// Gets or sets the unit of work.
    /// </summary>
    /// <value>
    /// The unit of work.
    /// </value>
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = Car.Create(request.make, request.model);

        this.carRepository.Add(car);

        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return car.Id;
    }
}
