using DriveEase.Domain.Abstraction;
using DriveEase.Domain.ValueObjects;
using DriveEase.SharedKernel.Util;

namespace DriveEase.Domain.Entities;

/// <summary>
/// User Entity.
/// </summary>
/// <seealso cref="DriveEase.Domain.Entities.BaseEntity" />
public class User : BaseEntity, IAuditableEntity, ISoftDeletableEntity
{
    private string passwordHash;

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="firstName">The user first name.</param>
    /// <param name="lastName">The user last name.</param>
    /// <param name="email">The user email instance.</param>
    /// <param name="passwordHash">The user password hash.</param>
    private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
        Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));
        Ensure.NotEmpty(email, "The email is required.", nameof(email));
        Ensure.NotEmpty(passwordHash, "The password hash is required", nameof(passwordHash));

        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.passwordHash = passwordHash;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private User()
    {
    }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    public FirstName FirstName { get; private set; }

    /// <summary>
    /// Gets the user last name.
    /// </summary>
    public LastName LastName { get; private set; }

    /// <summary>
    /// Gets the user full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public Email Email { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedDate { get; }

    /// <inheritdoc />
    public DateTime? UpdatedDate { get; }

    /// <inheritdoc />
    public DateTime? DeletedDate { get; }

    /// <inheritdoc />
    public bool IsDeleted { get; }

    /// <summary>
    /// Creates a new user with the specified first name, last name, email and password hash.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="email">The email.</param>
    /// <param name="passwordHash">The password hash.</param>
    /// <returns>The newly created user instance.</returns>
    public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash)
    {
        return new User(firstName, lastName, email, passwordHash);
    }

    /// <summary>
    /// Verifies that the provided password hash matches the password hash.
    /// </summary>
    /// <param name="password">The password to be checked against the user password hash.</param>
    /// <param name="passwordHashChecker">The password hash checker.</param>
    /// <returns>True if the password hashes match, otherwise false.</returns>
    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
        => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(passwordHash, password);
}
