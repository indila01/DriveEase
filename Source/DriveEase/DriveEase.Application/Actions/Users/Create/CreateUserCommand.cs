using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users.Create;
public record CreateUserCommand(string firstName, string lastName, string email, string password)
: IRequest<Result<string>>;
