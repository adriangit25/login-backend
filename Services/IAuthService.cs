using LoginBackend.Models;  // Asegúrate de que esté importado el namespace correcto para acceder a UsuarioRegistro

namespace LoginBackend.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(UsuarioRegistro usuarioRegistro);  // Cambié User por UsuarioRegistro
    }
}
