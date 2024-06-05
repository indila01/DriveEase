using DriveEase.API.Extensions;
using DriveEase.Application.Actions.Cars.Get;
using FastEndpoints;
using MediatR;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Returns a car by Id.
/// </summary>
public class DeleteCar : Endpoint<DeleteCarRequest, IResult>
{
    /// <summary>
    /// Gets a car.
    /// </summary>
    private readonly IMediator mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCar"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public DeleteCar(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <inheritdoc/>
    public override void Configure()
    {
        this.Delete(DeleteCarRequest.Route);
        this.AllowAnonymous();
        //this.Description(x => x
        //   //.Accepts<GetCarRequest>(MediaTypeNames.Application.Json)
        //   .Produces<CarResponse>(200, MediaTypeNames.Application.Json)
        //   .Produces<CustomProblemDetails>(400, MediaTypeNames.Application.Json));
    }

    /// <inheritdoc/>
    public override async Task<IResult> ExecuteAsync(DeleteCarRequest req, CancellationToken ct)
    {
        var result = await this.mediator.Send(new DeleteCarCommand(req.id));

        //if (result is { IsSuccess: true })
        //{
        //    Results.Ok(result.Value);
        //}

        //if (result is { IsSuccess: false, Error: not null })
        //{
        //    Results.
        //}

        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails(includeErrorDetails: true);
    }
}
