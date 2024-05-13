using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;
using DriveEase.Domain.ValueObjects;

namespace DriveEase.Domain.Repositories;

/// <summary>
/// user repository
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Gets the name of the user by.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>user entity. </returns>
    Task<User> GetUserByName(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the user by email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether [is email unique asynchronous] [the specified email].
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>email uniqueness. </returns>
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
}
