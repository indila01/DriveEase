using FastEndpoints;
using FluentValidation;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// Car validator
/// </summary>
public class CreateCarValidator : Validator<CreateCarRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCarValidator"/> class.
    /// </summary>
    public CreateCarValidator()
    {
        this.RuleFor(x => x.model)
             .NotEmpty().WithMessage("Model name is required")
             .MinimumLength(3).WithMessage("Model name is too short");

        this.RuleFor(x => x.make)
            .NotEmpty().WithMessage("Nake name is required")
            .MinimumLength(3).WithMessage("Nake name is too short");
    }
}
