using DriveEase.API.Extensions;
using DriveEase.API.Model;
using DriveEase.Application.Actions.Users.Create;
using DriveEase.SharedKernel;
using FastEndpoints;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace DriveEase.API.Endpoints.Auth;

/// <summary>
/// CreateUser endpoint
/// </summary>
public class CreateUser
    : Endpoint<CreateUserRequest, IResult>
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
    /// Initializes a new instance of the <see cref="CreateUser"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="appSettings">The application settings.</param>
    public CreateUser(IMediator mediator, IOptionsSnapshot<ApplicationConfig> appSettings)
    {
        this.mediator = mediator;
        this.appSettings = appSettings.Value;
    }

    /// <inheritdoc/>
    public override void Configure()
    {
        this.Post(CreateUserRequest.Route);
        this.AllowAnonymous();
        this.Description(x => x
            .Accepts<CreateUserRequest>(MediaTypeNames.Application.Json)
            .Produces<Guid>(201, MediaTypeNames.Application.Json)
            .Produces<CustomProblemDetails>(400, MediaTypeNames.Application.Json));
    }

    /// <inheritdoc/>
    public override async Task<IResult> ExecuteAsync(CreateUserRequest req, CancellationToken ct)
    {
        var result = await this.mediator.Send(
            new CreateUserCommand(
                 req.firstName,
                 req.lastName,
                 req.email,
                 req.password));

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
