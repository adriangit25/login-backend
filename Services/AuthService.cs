using System;
using System.IdentityModel.Tokens.Jwt; // Necesario para trabajar con JWT
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LoginBackend.Models;  // Asegúrate de que la clase UsuarioRegistro esté en este namespace

namespace LoginBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _secretKey = "supersecretkey"; // Cambia esto por una clave más segura

        public string GenerateJwtToken(UsuarioRegistro usuarioRegistro) // Cambié User por UsuarioRegistro
        {
            // Verificamos si los valores son nulos antes de usarlos
            if (usuarioRegistro.UsuCorreo == null || usuarioRegistro.UsuUsuario == null)
            {
                throw new ArgumentException("El correo o el usuario no pueden ser nulos.");
            }

            var claims = new[] 
            {
                new Claim(ClaimTypes.Name, usuarioRegistro.UsuCorreo ?? string.Empty), // Si UsuCorreo es nulo, usa una cadena vacía
                new Claim(ClaimTypes.NameIdentifier, usuarioRegistro.UsuUsuario ?? string.Empty) // Si UsuUsuario es nulo, usa una cadena vacía
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
