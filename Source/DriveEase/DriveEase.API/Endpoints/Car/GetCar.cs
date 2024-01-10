using DriveEase.API.Extensions;
using DriveEase.Application.Actions.Cars.Get;
using FastEndpoints;
using MediatR;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// get cars endpoint
/// </summary>
/// <seealso cref="FastEndpoints.Endpoint&lt;DriveEase.API.Endpoints.Car.GetCarRequest, DriveEase.API.Endpoints.Car.GetCarResponse&gt;" />
public class GetCar : Endpoint<GetCarRequest, IResult>
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCar"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public GetCar(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <inheritdoc/>
    public override void Configure()
    {
        this.Get(GetCarRequest.Route);
        this.AllowAnonymous();
    }

    /// <inheritdoc/>
    public override async Task<IResult> ExecuteAsync(GetCarRequest req, CancellationToken ct)
    {
        var result = await this.mediator.Send(new GetCarCommand(req.model));

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
}
