using DriveEase.API.Model;
using DriveEase.SharedKernel;
using DriveEase.SharedKernel.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace DriveEase.API.Middleware;

/// <summary>
/// Global exception handler
/// </summary>
public class GlobalExceptionHandler
{
    /// <summary>
    /// The next
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GlobalExceptionHandler> logger;

    /// <summary>
    /// Indicates if the responses should contain exception details.
    /// </summary>
    private readonly bool includeExceptionDetailsInResponse;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="logger">The logger.</param>
    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IOptionsSnapshot<ApplicationConfig> appSettings)
    {
        this.next = next;
        this.logger = logger;
        this.includeExceptionDetailsInResponse = appSettings.Value.IncludeExceptionDetailsInResponse;
    }

    /// <summary>
    /// Invokes the asynchronous.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>exception</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await this.next(httpContext);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Handles the exception asynchronous.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="ex">The ex.</param>
    /// <returns>exception</returns>
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch (ex)
        {
            //case BadRequestException badRequestException:
            //    statusCode = HttpStatusCode.BadRequest;
            //    problem = new CustomProblemDetails
            //    {
            //        Title = badRequestException.Message,
            //        Status = (int)statusCode,
            //        Detail = badRequestException.InnerException?.Message,
            //        Type = nameof(BadRequestException),
            //        Errors = badRequestException.ValidationErrors
            //    };
            //    break;
            case NotFoundException NotFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = NotFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = this.includeExceptionDetailsInResponse ? NotFound.InnerException?.Message : string.Empty,
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = this.includeExceptionDetailsInResponse ? ex.StackTrace : string.Empty,
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var logMessage = JsonConvert.SerializeObject(problem);
        this.logger.LogError(logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
