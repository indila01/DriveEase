using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users.Get;

/// <summary>
/// user command handler
/// </summary>
/// <seealso cref="IRequestHandler&lt;GetUserCommand, Result&lt;UserDto&gt;&gt;" />
/// <remarks>
/// Initializes a new instance of the <see cref="GetUserCommandHandler"/> class.
/// </remarks>
/// <param name="userRepository">The user repository.</param>
public class GetUserCommandHandler(IUserRepository userRepository)
: IRequestHandler<GetUserCommand, Result<UserDto>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private IUserRepository userRepository { get; set; } = userRepository;

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>task</returns>
    public async Task<Result<UserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var query = await this.userRepository.GetUserByName(request.username);

        return Result.Success<UserDto>(new(query.Id, query.FirstName));
    }
}
