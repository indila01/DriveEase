using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Repositories;
using DriveEase.Domain.ValueObjects;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users.Create;

/// <summary>
/// create user command handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler&lt;DriveEase.Application.Actions.Users.Create.CreateUserCommand, DriveEase.SharedKernel.Primitives.Result.Result&lt;System.Guid&gt;&gt;" />
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private IUserRepository userRepository { get; set; }

    /// <summary>
    /// Gets or sets the password hasher.
    /// </summary>
    /// <value>
    /// The password hasher.
    /// </value>
    private IPasswordHasher passwordHasher { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.email);
        var firstName = FirstName.Create(request.firstName);
        var lastName = Email.Create(request.lastName);
        var password = Password.Create(request.password);

        var firstFailiureOrSuccess = Result.FirstFailureOrSuccess(email, firstName, lastName, password);

        if (firstFailiureOrSuccess.IsFailure)
        {
            return Result.Failure<string>(firstFailiureOrSuccess.Error);
        }

        if (!await this.userRepository.IsEmailUniqueAsync(email.Value))
        {
            return Result.Failure<string>(DomainErrors.User.DuplicateEmail);
        }

        string passwordHash = passwordHasher.HashPassword(password.Value);
        return Result.Success(String.Empty);

    }
}
