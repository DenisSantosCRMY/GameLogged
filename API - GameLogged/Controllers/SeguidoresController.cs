using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguidoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeguidoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seguidor>>> GetSeguidores()
        {
            var seguidores = await _context.Seguidores
                .Include(s => s.UsuarioSeguidor)
                .Include(s => s.UsuarioSeguindo)
                .ToListAsync();
            return Ok(seguidores);
        }

        [HttpGet("seguidores/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Seguidor>>> GetSeguidoresDoUsuario(int idUsuario)
        {
            var seguidores = await _context.Seguidores
                .Include(s => s.UsuarioSeguidor)
                .Where(s => s.id_seguindo == idUsuario)
                .ToListAsync();
            return Ok(seguidores);
        }

        [HttpGet("seguindo/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Seguidor>>> GetSeguindoDoUsuario(int idUsuario)
        {
            var seguindo = await _context.Seguidores
                .Include(s => s.UsuarioSeguindo)
                .Where(s => s.id_seguidor == idUsuario)
                .ToListAsync();
            return Ok(seguindo);
        }

        [HttpPost]
        public async Task<IActionResult> Seguir([FromBody] Seguidor seguidor)
        {
            var jaExiste = await _context.Seguidores
                .AnyAsync(s => s.id_seguidor == seguidor.id_seguidor && s.id_seguindo == seguidor.id_seguindo);

            if (jaExiste)
                return Conflict("Você já segue este usuário.");

            _context.Seguidores.Add(seguidor);
            await _context.SaveChangesAsync();
            return Ok(seguidor);
        }

        [HttpDelete]
        public async Task<IActionResult> Desseguir([FromQuery] int idSeguidor, [FromQuery] int idSeguindo)
        {
            var seguidor = await _context.Seguidores
                .FirstOrDefaultAsync(s => s.id_seguidor == idSeguidor && s.id_seguindo == idSeguindo);

            if (seguidor == null)
                return NotFound("Relação de seguidor não encontrada.");

            _context.Seguidores.Remove(seguidor);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
