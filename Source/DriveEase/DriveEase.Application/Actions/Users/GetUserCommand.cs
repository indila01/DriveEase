using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users;

/// <summary>
/// user command
/// </summary>
/// <seealso cref="MediatR.IRequest&lt;DriveEase.SharedKernel.Primitives.Result.Result&lt;DriveEase.Application.Actions.Users.UserDto&gt;&gt;" />
public record GetUserCommand(string Username) : IRequest<Result<UserDto>>;
