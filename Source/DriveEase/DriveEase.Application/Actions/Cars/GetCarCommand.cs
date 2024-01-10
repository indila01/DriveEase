using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Cars;

/// <summary>
/// command 
/// </summary>
public record GetCarCommand(string model) : IRequest<Result<CarDto>>;