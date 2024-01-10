namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// car response
/// </summary>
public record GetCarResponse(Guid id, string make, string model);