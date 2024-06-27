using DriveEase.SharedKernel.Primitives;

namespace DriveEase.Domain.Core.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the user errors.
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Gets the not found.
        /// </summary>
        public static Error NotFound => Error.NotFound("User.NotFound", "The user with the specified identifier was not found.");

        /// <summary>
        /// Gets the invalid permissions.
        /// </summary>
        public static Error InvalidPermissions => Error.Validation(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation.");

        /// <summary>
        /// Gets the invalid password.
        /// </summary>
        public static Error InvalidPassword => Error.Validation("User.InvalidPassword", "The password is invalid");

        /// <summary>
        /// Gets the duplicate email.
        /// </summary>
        /// <value>
        /// The duplicate email.
        /// </value>
        public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail", "The specified email is already in use.");

        /// <summary>
        /// Gets the cannot change password.
        /// </summary>
        /// <value>
        /// The cannot change password.
        /// </value>
        public static Error CannotChangePassword => Error.Validation(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.");
    }

    /// <summary>
    ///  Contains the car errors.
    /// </summary>
    public static class Car
    {
        /// <summary>
        /// Gets the not found.
        /// </summary>
        /// <value>
        /// The not found.
        /// </value>
        public static Error NotFound => Error.NotFound("Car.NotFound", "The car with the specified identifier was not found.");
    }

    /// <summary>
    /// Contains the name errors.
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("Name.NullOrEmpty", "The name is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.Validation("Name.LongerThanAllowed", "The name is longer than allowed.");
    }

    /// <summary>
    /// Contains the first name errors.
    /// </summary>
    public static class FirstName
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("FirstName.NullOrEmpty", "The first name is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.NotFound("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }

    /// <summary>
    /// Contains the last name errors.
    /// </summary>
    public static class LastName
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("LastName.NullOrEmpty", "The last name is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.Validation("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("Email.NullOrEmpty", "The email is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.Validation("Email.LongerThanAllowed", "The email is longer than allowed.");

        /// <summary>
        /// gets invalid format error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidFormat => Error.Validation("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the password errors.
    /// </summary>
    public static class Password
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("Password.NullOrEmpty", "The password is required.");

        /// <summary>
        /// gets too short string
        /// </summary>
        /// <returns>Error</returns>
        public static Error TooShort => Error.Validation("Password.TooShort", "The password is too short.");

        /// <summary>
        /// gets missing upper case error
        /// </summary>
        /// <returns>Error</returns>
        public static Error MissingUppercaseLetter => Error.Validation(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        /// <summary>
        /// gets missing lower case error
        /// </summary>
        /// <returns>Error</returns>
        public static Error MissingLowercaseLetter => Error.Validation(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        /// <summary>
        /// gets missing digit error
        /// </summary>
        /// <returns>Error</returns>
        public static Error MissingDigit => Error.Validation(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        /// <summary>
        /// gets missing non numaric error
        /// </summary>
        /// <returns>Error</returns>
        public static Error MissingNonAlphaNumeric => Error.Validation(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// gets unproccessed request error
        /// </summary>
        /// <returns>Error</returns>
        public static Error UnProcessableRequest => Error.Failiure(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        /// <summary>
        /// gets server error
        /// </summary>
        /// <returns>Error</returns>
        public static Error ServerError => Error.Failiure("General.ServerError", "The server encountered an unrecoverable error.");
    }

    /// <summary>
    /// Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        /// gets invalid email or password error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidEmailOrPassword => Error.Validation(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");
    }
}