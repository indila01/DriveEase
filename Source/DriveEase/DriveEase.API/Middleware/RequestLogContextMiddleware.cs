using Serilog.Context;

namespace DriveEase.API.Middleware;

/// <summary>
/// Request log middleware
/// </summary>
public class RequestLogContextMiddleware
{
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestLogContextMiddleware"/> class.
    /// </summary>
    /// <param name="next">next param</param>
    public RequestLogContextMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// invoke async
    /// </summary>
    /// <param name="context">context</param>
    /// <returns>task</returns>
    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return this.next(context);
        }
    }
}
