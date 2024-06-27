using FastEndpoints;
using FluentValidation;

namespace DriveEase.API.Endpoints.Auth;

/// <summary>
/// login validator
/// </summary>
public class LoginValidator : Validator<LoginRequeust>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginValidator"/> class.
    /// </summary>
    public LoginValidator()
    {
        this.RuleFor(x => x.email)
            .NotEmpty().WithMessage("Email is required")
            .MinimumLength(3).WithMessage("email should contain at least 3 characters");

        this.RuleFor(x => x.password)
            .NotEmpty().WithMessage("password is required")
            .Matches("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage("password should contain at least 8 characters, 1 upper case, 1 lower case character and a special characer");
    }
}
