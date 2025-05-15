using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ambev.DeveloperEvaluation.Common.Security;

/// <summary>
/// Implementation of JWT (JSON Web Token) generator.
/// </summary>
public sealed class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    /// <summary>
    /// Generate a new Refresh Token.
    /// </summary>
    /// <returns>An Refresh Token</returns>
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>
    /// Generates a JWT token for a specific user.
    /// </summary>
    /// <param name="id">Id of User for whom the token will be generated.</param>
    /// <param name="username">Username of User for whom the token will be generated.</param>
    /// <param name="role">Role of User for whom the token will be generated.</param>
    /// <returns>Valid JWT token as string.</returns>
    /// <remarks>
    /// The generated token includes the following claims:
    /// - NameIdentifier (User ID)
    /// - Name (Username)
    /// - Role (User role)
    ///
    /// The token is valid for 8 hours from the moment of generation.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when user or secret key is not provided.</exception>
    public string GenerateToken(int id, string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey!);

        var claims = new[]
        {
           new Claim(ClaimTypes.NameIdentifier, id.ToString()),
           new Claim(ClaimTypes.Name, username),
           new Claim(ClaimTypes.Role, role)
       };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
