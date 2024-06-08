using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Get;

/// <summary>
/// get car handler
/// </summary>
/// <seealso cref="IRequestHandler&lt;GetCarCommand, Result&lt;CarResponse&gt;&gt;" />
/// <remarks>
/// Initializes a new instance of the <see cref="GetCarCommandHandler"/> class.
/// </remarks>
/// <param name="carRepository">The car repository.</param>
public class GetCarCommandHandler(ICarRepository carRepository)
 : IRequestHandler<GetCarCommand, Result<CarResponse>>
{
    /// <summary>
    /// The car repository
    /// </summary>
    private readonly ICarRepository carRepository = carRepository;

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// Response from the request.
    /// </returns>
    public async Task<Result<CarResponse>> Handle(GetCarCommand request, CancellationToken cancellationToken)
    {
        var result = await this.carRepository.GetCarByModelAsync(request.model);

        if (result is null)
        {
            return Result.Failure<CarResponse>(DomainErrors.Car.NotFound);
        }

        return Result.Success<CarResponse>(
            new(
                result.Id,
                result.Make,
                result.Model));
    }
}
