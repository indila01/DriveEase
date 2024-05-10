using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Create;

/// <summary>
/// create car command.
/// </summary>
public record CreateCarCommand(string make, string model) : IRequest<Result<Guid>>;
