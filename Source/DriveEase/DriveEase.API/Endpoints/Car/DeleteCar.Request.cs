namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// get car request
/// </summary>
public record DeleteCarRequest(Guid id)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/cars";
}
