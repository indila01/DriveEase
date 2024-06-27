namespace DriveEase.API.Endpoints.Auth;

/// <summary>
/// login request
/// </summary>
public record LoginRequeust(string email, string password)
{
    /// <summary>
    /// The route
    /// </summary>
    public const string Route = "/api/auth/login";
}
