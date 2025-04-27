using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Asegúrate de importar también Entity Framework
using LoginBackend.Data;  // Para acceder a ApplicationDbContext
using LoginBackend.Models; // Para las clases como User, etc.
using LoginBackend.Services; // Asegúrate de que IAuthService y AuthService estén aquí
using BCrypt.Net; // Para el uso de BCrypt en el controlador

namespace LoginBackend.Controllers // Asegúrate de que el namespace esté correcto
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
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  

            // Verifica si el correo ya está registrado
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
                return Conflict("El correo ya está registrado.");

            // Crea un nuevo usuario
            var newUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Phone = user.Phone,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password), // Cifrado de la contraseña
                State = 1 // Estado activo
            };

            // Agregar el nuevo usuario a la base de datos
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado exitosamente." });
        }
    }
}
