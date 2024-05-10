using DriveEase.Application.Actions.Cars;
using FastEndpoints;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Get Car summary
/// </summary>
public class GetCarSummary : Summary<GetCar>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCarSummary"/> class.
    /// </summary>
    public GetCarSummary()
    {
        this.ExampleRequest = new GetCarRequest("520d");
        this.ResponseExamples[200] = new CarResponse(
            Guid.NewGuid(),
            "BMW",
            "520d");
    }
}
