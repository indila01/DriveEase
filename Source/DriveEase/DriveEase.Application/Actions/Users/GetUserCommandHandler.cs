using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users;

/// <summary>
/// user command handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler&lt;DriveEase.Application.Actions.Users.GetUserCommand, DriveEase.SharedKernel.Primitives.Result.Result&lt;DriveEase.Application.Actions.Users.UserDto&gt;&gt;" />
public class GetUserCommandHandler : IRequestHandler<GetUserCommand, Result<UserDto>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private IUserRepository userRepository { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    public GetUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Result<UserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var query = await this.userRepository.GetUserByName(request.Username);

        return Result.Success<UserDto>(new(query.Id, query.FirstName));
    }
}
