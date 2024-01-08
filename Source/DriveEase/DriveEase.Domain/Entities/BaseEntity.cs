namespace DriveEase.Domain.Entities;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid Id { get; set; }
}
