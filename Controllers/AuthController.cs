using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginBackend.Data;
using BCrypt.Net;

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
    public async Task<IActionResult> Register([FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);  

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userModel.Email);
        if (existingUser != null)
            return Conflict("El correo ya está registrado.");

        var user = new User
        {
            Name = userModel.Name,
            LastName = userModel.LastName,
            DateOfBirth = userModel.DateOfBirth,
            Phone = userModel.Phone,
            Email = userModel.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
            State = 1
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Usuario registrado exitosamente." });
    }
}
