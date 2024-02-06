using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users.Get;

/// <summary>
/// user command
/// </summary>
/// <seealso cref="IRequest&lt;Result&lt;UserDto&gt;&gt;" />
public record GetUserCommand(string username) : IRequest<Result<UserDto>>;
