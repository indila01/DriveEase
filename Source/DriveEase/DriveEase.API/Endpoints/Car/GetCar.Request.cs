namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// get car request
/// </summary>
public record GetCarRequest(string model)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/cars";
}
