using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;
using DriveEase.SharedKernel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DriveEase.Infrastructure.Authentication;

/// <summary>
/// JWTt provider class
/// </summary>
public class JwtProvider : IJwtProvider
{
    public JwtSettings jwtSettings { get; set; }
    public JwtProvider(IOptionsSnapshot<JwtSettings> jwtSettings)
    {
        this.jwtSettings = jwtSettings.Value;
    }
    public string Create(User user)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                                      SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
