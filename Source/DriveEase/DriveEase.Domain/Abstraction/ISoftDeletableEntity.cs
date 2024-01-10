namespace DriveEase.Domain.Abstraction;

/// <summary>
/// Represents the marker interface for soft-deletable entities.
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// Gets the deleted date.
    /// </summary>
    DateTime? DeletedDate { get; }

    /// <summary>
    /// Gets a value indicating whether the entity has been deleted.
    /// </summary>
    bool IsDeleted { get; }
}
