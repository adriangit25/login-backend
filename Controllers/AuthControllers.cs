[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AuthController(IAuthService authService, ApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    // Registro de usuario
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userModel.Email);
        if (existingUser != null)
            return Conflict("El correo ya está registrado.");

        var newUser = new User
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
            State = 1 // Estado activo
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Usuario registrado exitosamente" });
    }

    // Login de usuario
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginModel.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            return Unauthorized("Credenciales inválidas");

        var token = _authService.GenerateJwtToken(user);

        return Ok(new { Token = token });
    }
}
