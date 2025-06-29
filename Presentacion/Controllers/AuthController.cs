using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var user = await _authService.LoginAsync(dto.Email, dto.Password);
                return Ok(new
                {
                    Usuario = user,
                    Rol = user.RolId == 1 ? "Administrador"
                         : user.RolId == 2 ? "Operador"
                         : "Invitado"
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("hash")]
        public IActionResult GenerarHash([FromBody] string plainPassword)
        {
            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(null, plainPassword);
            return Ok(hash);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            await _authService.RegistrarUsuarioAsync(new UsuarioDTO
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                RolId = dto.RolId
            }, dto.Password);

            return Ok("Usuario registrado exitosamente.");
        }
    }
}
