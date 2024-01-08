namespace DriveEase.Domain.Entities;

/// <summary>
/// Car entity.
/// </summary>
public class Car : BaseEntity
{
    /// <summary>
    /// Gets or sets the make.
    /// </summary>
    /// <value>
    /// The make.
    /// </value>
    public string Make { get; set; }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    /// <value>
    /// The created by.
    /// </value>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    /// <value>
    /// The updated date.
    /// </value>
    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated by.
    /// </summary>
    /// <value>
    /// The updated by.
    /// </value>
    public string? UpdatedBy { get; set; }
}
