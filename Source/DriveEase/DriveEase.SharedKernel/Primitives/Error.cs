namespace DriveEase.SharedKernel.Primitives;

/// <summary>
/// Error class
/// </summary>
/// <seealso cref="System.IEquatable&lt;DriveEase.SharedKernel.Primitives.Error&gt;" />
public record Error
{
    /// <summary>
    /// The none
    /// </summary>
    public static readonly Error None = new Error(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// The null value
    /// </summary>
    public static readonly Error NullValue = new Error("Error.NullValue", "Null value was provided", ErrorType.Failure);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="Code">The code.</param>
    /// <param name="Description">The description.</param>
    /// <param name="type">The type.</param>
    private Error(string code, string description, ErrorType type)
    {
        this.Code = code;
        this.Description = description;
        this.Type = type;
    }

    /// <summary>
    /// Gets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    public string Code { get; }

    /// <summary>
    /// Gets the decription.
    /// </summary>
    /// <value>
    /// The decription.
    /// </value>
    public string Description { get; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    public ErrorType Type { get; }

    /// <summary>
    /// Nots the found.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="description">The description.</param>
    /// <returns>not found error.</returns>
    public static Error NotFound(string code, string description)
        => new Error(code, description, ErrorType.Failure);

    /// <summary>
    /// Validations the specified code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="description">The description.</param>
    /// <returns>validation error.</returns>
    public static Error Validation(string code, string description)
       => new Error(code, description, ErrorType.Validation);

    /// <summary>
    /// Conflicts the specified code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="description">The description.</param>
    /// <returns>conflict error.</returns>
    public static Error Conflict(string code, string description)
       => new Error(code, description, ErrorType.Conflict);

    /// <summary>
    /// Failiures the specified code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="description">The description.</param>
    /// <returns>Failiure error</returns>
    public static Error Failiure(string code, string description)
       => new Error(code, description, ErrorType.Failure);
}
