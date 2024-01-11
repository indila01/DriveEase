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
    private Car(string make, string model)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(make, "The make is required", nameof(make));
        Ensure.NotEmpty(model, "The model is required", nameof(model));
        this.Make = make;
        this.Model = model;
        this.Status = "Available";
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
    public DateTime? UpdatedDate { get; }

    /// <inheritdoc />
    public DateTime? DeletedDate { get; }

    /// <inheritdoc />
    public bool IsDeleted { get; }

    /// <summary>
    /// Creates the specified make.
    /// </summary>
    /// <param name="make">The make.</param>
    /// <param name="model">The model.</param>
    /// <returns>Car.</returns>
    public static Car Create(string make, string model)
    {
        // add domainevent
        return new Car(make, model);
    }
}
