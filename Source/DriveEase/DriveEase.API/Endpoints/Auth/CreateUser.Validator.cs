using FastEndpoints;
using FluentValidation;

namespace DriveEase.API.Endpoints.Auth;

/// <summary>
/// Create user validator
/// </summary>
public class CreateUserValidator : Validator<CreateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
    /// </summary>
    public CreateUserValidator()
    {
        this.RuleFor(x => x.username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username should contain at least 3 characters");

        this.RuleFor(x => x.email)
            .NotEmpty().WithMessage("Email is required")
            .MinimumLength(3).WithMessage("email should contain at least 3 characters");

        this.RuleFor(x => x.firstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(3).WithMessage("First name should contain at least 3 characters");

        this.RuleFor(x => x.lastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(3).WithMessage("Last name should contain at least 3 characters");

        this.RuleFor(x => x.password)
            .NotEmpty().WithMessage("password is required")
            .Matches("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage("password should contain at least 8 characters, 1 upper case, 1 lower case character and a special characer");
    }
}
