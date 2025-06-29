using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public PerfilController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Obtener perfil por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerfil(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // Actualizar perfil
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerfil(int id, UsuarioDTO dto)
        {
            if (id != dto.UsuarioId) return BadRequest();
            await _usuarioService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
