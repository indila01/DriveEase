using DriveEase.API.Extensions;
using DriveEase.API.Model;
using DriveEase.Application.Actions.Cars.Create;
using DriveEase.SharedKernel;
using FastEndpoints;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Creates a car.
/// </summary>
public class CreateCar
    : Endpoint<CreateCarRequest, IResult>
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator mediator;

    /// <summary>
    /// The application settings
    /// </summary>
    private readonly ApplicationConfig appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCar"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="appSettings">The application settings.</param>
    public CreateCar(IMediator mediator, IOptionsSnapshot<ApplicationConfig> appSettings)
    {
        this.mediator = mediator;
        this.appSettings = appSettings.Value;
    }

    /// <inheritdoc/>
    public override void Configure()
    {
        this.Post(CreateCarRequest.Route);
        this.AllowAnonymous();
        this.Description(x => x
            .Accepts<CreateCarRequest>(MediaTypeNames.Application.Json)
            .Produces<Guid>(201, MediaTypeNames.Application.Json)
            .Produces<CustomProblemDetails>(400, MediaTypeNames.Application.Json)
        );
    }

    /// <summary>
    /// the handler method for the endpoint that returns the response dto. this method is called for each request received.
    /// </summary>
    /// <param name="req">the request dto</param>
    /// <param name="ct">a cancellation token</param>
    /// <returns>Guid</returns>
    public override async Task<IResult> ExecuteAsync(CreateCarRequest req, CancellationToken ct)
    {
        var result = await this.mediator.Send(new CreateCarCommand(req.make, req.model));

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }
        else
        {
            return result.ToProblemDetails(this.appSettings.IncludeExceptionDetailsInResponse);
        }
    }
}
