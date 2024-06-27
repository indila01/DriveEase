using FastEndpoints;
using FluentValidation;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// fluent validator for get car request
/// </summary>
public class DeleteCarValidator : Validator<DeleteCarRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCarValidator"/> class.
    /// </summary>
    public DeleteCarValidator()
    {
        this.RuleFor(x => x.id)
            .NotEmpty().WithMessage("Id is required");
    }
}
