using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Get;

/// <summary>
/// get car handler
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DeleteCarCommandHandler"/> class.
/// </remarks>
/// <param name="carRepository">The car repository.</param>
public class DeleteCarCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
: IRequestHandler<DeleteCarCommand, Result>
{
    /// <summary>
    /// The car repository
    /// </summary>
    private readonly ICarRepository carRepository = carRepository;

    /// <summary>
    /// Iunit of work
    /// </summary>
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    /// <summary>
    /// Handles a request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// Response from the request.
    /// </returns>
    public async Task<Result> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var result = await this.carRepository.GetByIDAsync(request.id);

        if (result is not null)
        {
            this.carRepository.Remove(result);
            await this.unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        return Result.Failure<CarResponse>(DomainErrors.Car.NotFound);
    }
}
