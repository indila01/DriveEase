using DriveEase.SharedKernel.Util;

namespace DriveEase.Domain.Entities;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected BaseEntity(Guid id)
        : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    protected BaseEntity()
    {
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid Id { get; set; }
}
