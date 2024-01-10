using DriveEase.Domain.Abstraction;
using DriveEase.SharedKernel.Util;

namespace DriveEase.Domain.Entities;

/// <summary>
/// Car entity.
/// </summary>
public class Car : BaseEntity, IAuditableEntity, ISoftDeletableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Car"/> class.
    /// </summary>
    /// <param name="make">The make.</param>
    /// <param name="model">The model.</param>
    /// <param name="status">The status.</param>
    /// <param name="createdBy">The created by.</param>
    private Car(string make, string model)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(make, "The make is required", nameof(make));
        Ensure.NotEmpty(model, "The model is required", nameof(model));
        //Ensure.NotEmpty(status, "The status is required", nameof(status));
        //Ensure.NotEmpty(createdBy, "The createdBy is required", nameof(createdBy));

        this.Make = make;
        this.Model = model;
        //this.Status = status;
        this.CreatedDate = DateTime.UtcNow;
        //this.CreatedBy = createdBy;
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="Car"/> class from being created.
    /// </summary>
    private Car()
    {
    }

    /// <summary>
    /// Gets the make.
    /// </summary>
    /// <value>
    /// The make.
    /// </value>
    public string Make { get; private set; }

    /// <summary>
    /// Gets the model.
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public string Model { get; private set; }

    /// <summary>
    /// Gets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string Status { get; private set; }

    /// <inheritdoc/>
    public DateTime CreatedDate { get; }

    /// <inheritdoc/>
    public string CreatedBy { get; }

    /// <inheritdoc/>
    public DateTime UpdatedDate { get; }

    /// <inheritdoc/>
    public string? UpdatedBy { get; }

    /// <inheritdoc />
    public DateTime? DeletedDate { get; }

    /// <inheritdoc />
    public bool IsDeleted { get; }

    /// <summary>
    /// Creates the specified make.
    /// </summary>
    /// <param name="make">The make.</param>
    /// <param name="model">The model.</param>
    /// <param name="status">The status.</param>
    /// <param name="createdBy">The created by.</param>
    /// <returns>Car.</returns>
    public static Car Create(string make, string model)
    {
        // add domainevent
        return new Car(make, model);
    }
}
