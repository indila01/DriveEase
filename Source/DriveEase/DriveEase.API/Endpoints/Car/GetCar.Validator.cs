using FastEndpoints;
using FluentValidation;

namespace DriveEase.API.Endpoints.Car;

/// <summary>
/// fluent validator for get car request
/// </summary>
public class GetCarValidator : Validator<GetCarRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCarValidator"/> class.
    /// </summary>
    public GetCarValidator()
    {
        this.RuleFor(x => x.model)
            .NotEmpty().WithMessage("Model name is required")
            .MinimumLength(3).WithMessage("Model name is too short");
    }
}
