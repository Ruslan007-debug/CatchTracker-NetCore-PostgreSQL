using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CatchTrackerApi.Services
{
    public class JWTService : IJWTService
    {
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _accessTokenExpiresMinutes;

        public JWTService(IConfiguration configuration)
        {
            _jwtSecret = configuration["JwtSettings:SecretKey"]
               ?? throw new ArgumentNullException("JWT SecretKey not configured");
            _issuer = configuration["JwtSettings:Issuer"]
                ?? throw new ArgumentNullException("JWT Issuer not configured");
            _audience = configuration["JwtSettings:Audience"]
                ?? throw new ArgumentNullException("JWT Audience not configured");
            _accessTokenExpiresMinutes = int.Parse(
                configuration["JwtSettings:AccessTokenExpirationMinutes"] ?? "15"
            );
        }
        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenExpiresMinutes),
                signingCredentials: credentails
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
