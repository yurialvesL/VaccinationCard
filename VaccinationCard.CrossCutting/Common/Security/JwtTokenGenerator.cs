using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.CrossCutting.Common.Options;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.CrossCutting.Common.Security;

/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWT) for authenticated users.
/// </summary>
/// <remarks>This class generates JWTs based on user information and configuration settings.  The generated tokens
/// include claims such as the user's identifier, name, and role,  and are signed using a symmetric security key derived
/// from the application's configuration.</remarks>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _opts;
    private readonly SigningCredentials _creds;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _opts = options.Value;

        var keyBytes = Convert.FromBase64String(_opts.SecretKey);
        var key = new SymmetricSecurityKey(keyBytes);
        _creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }

    /// <summary>
    /// Generates a JSON Web Token (JWT) for the specified person.
    /// </summary>
    /// <remarks>The generated token includes claims for the person's unique identifier, name, and role.  The
    /// token is signed using the HMAC-SHA256 algorithm and a secret key retrieved from the application configuration.
    /// The token is valid for 8 hours from the time of generation.</remarks>
    /// <param name="person">The person for whom the token is being generated. The person's ID, name, and role are included as claims in the
    /// token.</param>
    /// <returns>A string representation of the generated JWT.</returns>
    public string GenerateToken(IPerson person)
    {
        var now = DateTime.UtcNow;

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
           new Claim(ClaimTypes.NameIdentifier, person.Id),
           new Claim(ClaimTypes.Name, person.Name),
           new Claim(ClaimTypes.Role, person.IsAdmin ? "Admin" : "User")

        };

        var token = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(_opts.ExpirationInMinutes),
            signingCredentials: _creds
        );


        return tokenHandler.WriteToken(token);
    }
}