using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginBackend.Data;  // Para acceder a ApplicationDbContext
using LoginBackend.Models; // Para el modelo UsuarioRegistro
using LoginBackend.Services; // Asegúrate de que IAuthService y AuthService estén aquí
using BCrypt.Net; // Para el uso de BCrypt en el controlador

namespace LoginBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica si el correo ya está registrado
            var existingUser = await _context.UsuariosRegistros // Cambié UsuariosRegistro por UsuariosRegistros
                .FirstOrDefaultAsync(u => u.UsuCorreo == usuarioRegistro.UsuCorreo);
            if (existingUser != null)
                return Conflict("El correo ya está registrado.");

            // Crear el nuevo registro directamente sin necesidad de dos modelos separados
            usuarioRegistro.UsuContrasenia = BCrypt.Net.BCrypt.HashPassword(usuarioRegistro.UsuContrasenia);

            // Agregar el usuario al contexto
            _context.UsuariosRegistros.Add(usuarioRegistro); // Cambié UsuariosRegistro por UsuariosRegistros
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado exitosamente." });
        }
    }
}
