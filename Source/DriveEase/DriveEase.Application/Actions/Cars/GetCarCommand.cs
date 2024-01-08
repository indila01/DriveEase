namespace DriveEase.Application.Actions.Cars;

/// <summary>
/// command record
/// </summary>
public sealed record GetCarCommand(string Email, string Name, string Description);