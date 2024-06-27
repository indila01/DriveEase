using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Auth.Login;
public record LoginCommand(string email, string password)
    : IRequest<Result<string>>;
