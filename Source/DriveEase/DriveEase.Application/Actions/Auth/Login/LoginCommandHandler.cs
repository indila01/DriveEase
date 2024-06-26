﻿using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Core.Errors;
using DriveEase.Domain.Repositories;
using DriveEase.Domain.ValueObjects;
using DriveEase.SharedKernel.Primitives.Result;
using MediatR;

namespace DriveEase.Application.Actions.Auth.Login;

/// <summary>
/// Login command handler
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
/// </remarks>
/// <param name="jwtProvider">The JWT provider.</param>
/// <param name="passwordHashChecker">The password hasher.</param>
/// <param name="userRepository">The user repository.</param>
public class LoginCommandHandler(
    IJwtProvider jwtProvider,
    IPasswordHashChecker passwordHashChecker,
    IUserRepository userRepository)
        : IRequestHandler<LoginCommand, Result<string>>
{
    /// <summary>
    /// Gets or sets the user repository.
    /// </summary>
    /// <value>
    /// The user repository.
    /// </value>
    private readonly IUserRepository userRepository = userRepository;

    /// <summary>
    /// Gets or sets the password hasher checker.
    /// </summary>
    /// <value>
    /// The password hasher.
    /// </value>
    private readonly IPasswordHashChecker passwordHashChecker = passwordHashChecker;

    /// <summary>
    /// The JWT provider
    /// </summary>
    /// <value>
    /// The JWT provider.
    /// </value>
    private readonly IJwtProvider jwtProvider = jwtProvider;

    /// <inheritdoc/>
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.email);
        var password = Password.Create(request.password);

        var firstFailiureOrSuccess = Result.FirstFailureOrSuccess(email, password);

        if (firstFailiureOrSuccess.IsFailure)
        {
            return Result.Failure<string>(firstFailiureOrSuccess.Error);
        }

        var user = await this.userRepository.GetUserByEmail(email.Value);

        if (user is null)
        {
            return Result.Failure<string>(DomainErrors.Authentication.InvalidEmailOrPassword);
        }

        bool validPassword = user.VerifyPasswordHash(password.Value, this.passwordHashChecker);

        if (!validPassword)
        {
            return Result.Failure<string>(DomainErrors.Authentication.InvalidEmailOrPassword);
        }

        string token = this.jwtProvider.Create(user);

        return Result.Success(token);
    }
}
