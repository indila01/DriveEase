using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Get;

/// <summary>
/// get car handler
/// </summary>
/// <seealso cref="IRequestHandler&lt;GetCarCommand, Result&lt;CarDto&gt;&gt;" />
public class GetCarCommandHandler : IRequestHandler<GetCarCommand, Result<CarDto>>
{
    /// <summary>
    /// The car repository
    /// </summary>
    private readonly ICarRepository carRepository;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCarCommandHandler"/> class.
    /// </summary>
    /// <param name="carRepository">The car repository.</param>
    public GetCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
    {
        this.carRepository = carRepository;
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// Response from the request.
    /// </returns>
    public async Task<Result<CarDto>> Handle(GetCarCommand request, CancellationToken cancellationToken)
    {
        var result = await carRepository.GetCarByModelAsync(request.model);

        if (result is null)
        {
            return Result.Failure<CarDto>(DomainErrors.Car.NotFound);
        }

        return Result.Success<CarDto>(
            new(
                result.Id,
                result.Make,
                result.Model));
    }
}
