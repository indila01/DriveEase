using DriveEase.Domain.Entities;

namespace DriveEase.Domain.Abstraction;

/// <summary>
/// JWT provider interface
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Creates the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>jwt token</returns>
    string Create(User user);
}
