using DriveEase.Domain.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DriveEase.Infrastructure.Authentication;
public class PasswordHasher : IPasswordHasher
//, IDisposable
{
    private const int SaltSize = 128 / 8;
    private const int NumberOfBytesRequested = 256 / 8;
    private const int IterationCount = 100000;
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
