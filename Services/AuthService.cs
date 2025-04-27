using System;
using System.IdentityModel.Tokens.Jwt; // Necesario para trabajar con JWT
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LoginBackend.Models;  // Asegúrate de que la clase User esté en este namespace

namespace LoginBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey = "supersecretkey"; // Cambia esto por una clave más segura

        // Implementación del método GenerateJwtToken
        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
