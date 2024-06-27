using DriveEase.SharedKernel.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace DriveEase.Application.Behaviors;

/// <summary>
/// requestlogging pipeline behavior
/// </summary>
/// <typeparam name="TRequest">t request</typeparam>
/// <typeparam name="TResponse">t response</typeparam>
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger = logger;

    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        this.logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            this.logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                this.logger.LogError("Completed request {RequestName} with error", requestName);
            }
        }

        return result;
    }
}
