namespace DriveEase.Domain.Entities;

/// <summary>
/// User Entity.
/// </summary>
/// <seealso cref="DriveEase.Domain.Entities.BaseEntity" />
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>
    /// The first name.
    /// </value>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>
    /// The last name.
    /// </value>
    public string lastName { get; set; }
}
