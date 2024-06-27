using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars.Get;

/// <summary>
/// command
/// </summary>
public record DeleteCarCommand(Guid id)
: IRequest<Result>;