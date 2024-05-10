namespace DriveEase.API.Endpoints.Auth;

/// <summary>
/// create user request
/// </summary>
public record CreateUserRequest(string username, string firstName, string lastName, string email, string password)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/auth/register";

}
