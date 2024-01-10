namespace DriveEase.API.Endpoints.Car;

public record CreateCarRequest(string make, string model)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/cars/{id:Guid}";
}
