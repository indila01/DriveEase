using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Create;

/// <summary>
/// Create car command handler.
/// </summary>
public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Result<Guid>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private ICarRepository carRepository { get; set; }

    /// <summary>
    /// Gets or sets the unit of work.
    /// </summary>
    /// <value>
    /// The unit of work.
    /// </value>
    private IUnitOfWork unitOfWork { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCarCommandHandler"/> class.
    /// </summary>
    /// <param name="carRepository">The car repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public CreateCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
    {
        this.carRepository = carRepository;
        this.unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = Car.Create(request.make, request.model);

        this.carRepository.Add(car);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return car.Id;
    }
}
