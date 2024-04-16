namespace DriveEase.SharedKernel;

/// <summary>
/// Jwt settings.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    /// <value>
    /// The issuer.
    /// </value>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the se Audience.
    /// </summary>
    /// <value>
    /// The se Audience.
    /// </value>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>
    /// The key.
    /// </value>
    public string Key { get; set; }
}
