namespace DriveEase.Domain.Abstraction;

/// <summary>
/// password hasher interface.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>password hash</returns>
    string HashPassword(string password);
}
