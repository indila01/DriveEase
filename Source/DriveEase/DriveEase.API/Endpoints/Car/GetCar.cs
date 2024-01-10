using DriveEase.Application.Actions.Cars;
using FastEndpoints;
using MediatR;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// get cars endpoint
/// </summary>
/// <seealso cref="FastEndpoints.Endpoint&lt;DriveEase.API.Endpoints.Car.GetCarRequest, DriveEase.API.Endpoints.Car.GetCarResponse&gt;" />
public class GetCar : Endpoint<GetCarRequest, GetCarResponse>
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

    /// <summary>
    /// use this method to configure how the endpoint should be listening to incoming requests.
    /// <para>HINT: it is only called once during endpoint auto registration during app startup.</para>
    /// </summary>
    public override void Configure()
    {
        this.Get(GetCarRequest.Route);
        this.AllowAnonymous();
    }

    /// <summary>
    /// the handler method for the endpoint. this method is called for each request received.
    /// </summary>
    /// <param name="req">the request dto</param>
    /// <param name="ct">a cancellation token</param>
    /// <returns></returns>
    public override async Task HandleAsync(GetCarRequest req, CancellationToken ct)
    {
        var result = await this.mediator.Send(new GetCarCommand(req.Model));

        Response = new();


    }
}
