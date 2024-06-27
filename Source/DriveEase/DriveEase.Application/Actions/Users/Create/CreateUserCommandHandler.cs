using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.Domain.ValueObjects;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Users.Create;

/// <summary>
/// create user command handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler&lt;DriveEase.Application.Actions.Users.Create.CreateUserCommand, DriveEase.SharedKernel.Primitives.Result.Result&lt;System.Guid&gt;&gt;" />
/// <remarks>
/// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
/// </remarks>
/// <param name="userRepository">The user repository.</param>
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider)
     : IRequestHandler<CreateUserCommand, Result<string>>
{
    /// <summary>
    /// Gets or sets the Iunit of work.
    /// </summary>
    /// <value>
    /// Iunit of work.
    /// </value>
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private readonly IUserRepository userRepository = userRepository;

    /// <summary>
    /// Gets or sets the password hasher.
    /// </summary>
    /// <value>
    /// The password hasher.
    /// </value>
    private readonly IPasswordHasher passwordHasher = passwordHasher;

    /// <summary>
    /// The JWT provider
    /// </summary>
    /// <value>
    /// The JWT provider.
    /// </value>
    private readonly IJwtProvider jwtProvider = jwtProvider;

    /// <inheritdoc/>
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.email);
        var firstName = FirstName.Create(request.firstName);
        var lastName = LastName.Create(request.lastName);
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

        string passwordHash = this.passwordHasher.HashPassword(password.Value);
        var user = User.Create(firstName.Value, lastName.Value, email.Value, passwordHash);

        this.userRepository.Add(user);
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        string token = this.jwtProvider.Create(user);

        return Result.Success(token);
    }
}
