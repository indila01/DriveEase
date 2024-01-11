using DriveEase.Domain.Core.Errors;
using DriveEase.SharedKernel.Primitives;
using DriveEase.SharedKernel.Primitives.Result;

namespace DriveEase.Domain.ValueObjects;

/// <summary>
/// Represents the Last name value object.
/// </summary>
/// <seealso cref="DriveEase.SharedKernel.Primitives.ValueObject" />
public sealed class LastName : ValueObject
{

    /// <summary>
    /// The last name maximum length.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="LastName"/> class.
    /// </summary>
    /// <param name="value">The last name value.</param>
    private LastName(string value) => this.Value = value;

    /// <summary>
    /// Gets the last name value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(LastName lastName) => lastName.Value;

    /// <summary>
    /// Creates a new <see cref="FirstName"/> instance based on the specified value.
    /// </summary>
    /// <param name="lastName">The last name value.</param>
    /// <returns>The result of the last name creation process containing the last name or an error.</returns>
    public static Result<LastName> Create(string lastName) =>
        Result.Create(lastName, DomainErrors.LastName.NullOrEmpty)
            .Ensure(l => !string.IsNullOrWhiteSpace(l), DomainErrors.LastName.NullOrEmpty)
            .Ensure(l => l.Length <= MaxLength, DomainErrors.LastName.LongerThanAllowed)
            .Map(l => new LastName(l));

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return this.Value;
    }
}
