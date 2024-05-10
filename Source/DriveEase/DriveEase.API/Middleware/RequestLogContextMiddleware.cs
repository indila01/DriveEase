using Serilog.Context;

namespace DriveEase.API.Middleware;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return this.next(context);
        }
    }
}
