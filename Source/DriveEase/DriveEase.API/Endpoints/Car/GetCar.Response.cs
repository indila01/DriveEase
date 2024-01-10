namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// car response
/// </summary>
public class GetCarResponse
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the make.
    /// </summary>
    /// <value>
    /// The make.
    /// </value>
    public string Make { get; set; }

}
