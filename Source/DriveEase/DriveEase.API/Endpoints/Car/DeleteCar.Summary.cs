using FastEndpoints;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Get Car summary
/// </summary>
public class DeleteCarSummary : Summary<DeleteCar>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCarSummary"/> class.
    /// </summary>
    public DeleteCarSummary()
    {
        this.ExampleRequest = new DeleteCarRequest(new Guid());
    }
}
