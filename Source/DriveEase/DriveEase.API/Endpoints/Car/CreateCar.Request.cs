namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Create car requeust
/// </summary>
/// <param name="make"></param>
/// <param name="model"></param>
/// <returns></returns>
public record CreateCarRequest(string make, string model)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/cars";
}
