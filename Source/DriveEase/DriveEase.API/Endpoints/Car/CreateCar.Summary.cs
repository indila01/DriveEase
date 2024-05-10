using FastEndpoints;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Create Car Endpoint Summary
/// </summary>
public sealed class CreateCarSummary : Summary<CreateCar>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCarSummary"/> class.
    /// </summary>
    public CreateCarSummary()
    {
        this.ExampleRequest = new CreateCarRequest("BMW", "520d");
        this.ResponseExamples[201] = Guid.NewGuid();
    }
}
