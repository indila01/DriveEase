using DriveEase.Domain.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DriveEase.Infrastructure.Authentication;
public class PasswordHasher : IPasswordHasher, IPasswordHashChecker
//, IDisposable
{
    private const int SaltSize = 128 / 8;
    private const int NumberOfBytesRequested = 256 / 8;
    private const int IterationCount = 100000;

    public bool HashesMatch(string passwordHash, string providedPassword)
    {
        if (passwordHash is null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        if (providedPassword is null)
        {
            throw new ArgumentNullException(nameof(providedPassword));
        }

        byte[] decodedHashedPassword = Convert.FromBase64String(passwordHash);

        if (decodedHashedPassword.Length == 0)
        {
            return false;
        }

        byte[] salt = decodedHashedPassword.Take(SaltSize).ToArray();
        string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    providedPassword,
                    salt,
                    KeyDerivationPrf.HMACSHA256,
                    IterationCount,
                    decodedHashedPassword.Length - SaltSize));

        return hashed == passwordHash;

    }

    public string HashPassword(string password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);


        string hashed = Convert.ToBase64String(
             KeyDerivation.Pbkdf2(
                 password,
                 salt,
                 KeyDerivationPrf.HMACSHA256,
                 IterationCount,
                 NumberOfBytesRequested));

        return hashed;
    }
}
