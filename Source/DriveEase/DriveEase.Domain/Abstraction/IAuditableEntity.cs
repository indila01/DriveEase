namespace DriveEase.Domain.Abstraction;

/// <summary>
/// Represents the marker interface for auditable entities.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    DateTime CreatedDate { get; }

    /// <summary>
    /// Gets the updated date.
    /// </summary>
    /// <value>
    /// The updated date.
    /// </value>
    DateTime? UpdatedDate { get; }
}
