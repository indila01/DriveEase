using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;

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
    /// <returns></returns>
    Task<User> GetUserByName(string username);
}
