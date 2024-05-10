using DriveEase.SharedKernel.Primitives;
using DriveEase.SharedKernel.Primitives.Result;

namespace DriveEase.API.Extensions;

/// <summary>
/// ResultExtensions.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts to problemdetails.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="includeErrorDetails">if set to <c>true</c> [include error details].</param>
    /// <returns>IResult.</returns>
    public static IResult ToProblemDetails(this Result result, bool includeErrorDetails = false)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: string.Empty,
            extensions: new Dictionary<string, object?>
            {
                {
                    "errors", includeErrorDetails ?
                    new[] { result.Error }
                    : Array.Empty<object>()
                },
            });

        static int GetStatusCode(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,
            };

        static string GetTitle(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Server Failure",
            };
    }
}
