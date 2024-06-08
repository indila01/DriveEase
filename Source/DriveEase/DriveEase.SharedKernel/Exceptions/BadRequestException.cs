using FluentValidation.Results;

namespace DriveEase.SharedKernel.Exceptions;

/// <summary>
/// bad request exception.
/// </summary>
/// <seealso cref="System.Exception" />
public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BadRequestException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="validationResult">The validation result.</param>
    public BadRequestException(string message, ValidationResult validationResult)
        : base(message)
    {
        this.ValidationErrors = validationResult.ToDictionary();
    }

    /// <summary>
    /// Gets or sets the validation errors.
    /// </summary>
    /// <value>
    /// The validation errors.
    /// </value>
    public IDictionary<string, string[]> ValidationErrors { get; set; }
}
