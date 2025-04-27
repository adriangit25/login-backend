using LoginBackend.Models;  // Asegúrate de que esté importado el namespace correcto para acceder a User

namespace LoginBackend.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);  // Método para generar el token JWT
    }
}
