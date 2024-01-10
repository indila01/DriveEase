namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// get car request
/// </summary>
public class GetCarRequest
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/cars/{id:Guid}";

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public string Model { get; set; }
}
