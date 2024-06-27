namespace DriveEase.Domain.Abstraction;

/// <summary>
/// checks password
/// </summary>
public interface IPasswordHashChecker
{
    /// <summary>
    /// Hasheses the match.
    /// </summary>
    /// <param name="passwordHash">The password hash.</param>
    /// <param name="providedPassword">The provided password.</param>
    /// <returns></returns>
    bool HashesMatch(string passwordHash, string providedPassword);
}
