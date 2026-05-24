using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioConquistasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioConquistasController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioConquista>>> GetUsuarioConquistas()
        {
            var lista = await _context.UsuarioConquistas
                .Include(uc => uc.Usuario)
                .Include(uc => uc.Conquista)
                .ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioConquista>> GetUsuarioConquista(int id)
        {
            var uc = await _context.UsuarioConquistas
                .Include(u => u.Usuario)
                .Include(u => u.Conquista)
                .FirstOrDefaultAsync(u => u.id == id);

            if (uc == null)
                return NotFound("Registro não encontrado.");
            return Ok(uc);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<UsuarioConquista>>> GetConquistasByUsuario(int idUsuario)
        {
            var conquistas = await _context.UsuarioConquistas
                .Include(uc => uc.Conquista)
                .Where(uc => uc.id_user == idUsuario)
                .ToListAsync();
            return Ok(conquistas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuarioConquista([FromBody] UsuarioConquista usuarioConquista)
        {
            _context.UsuarioConquistas.Add(usuarioConquista);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarioConquista), new { id = usuarioConquista.id }, usuarioConquista);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioConquista(int id)
        {
            var uc = await _context.UsuarioConquistas.FindAsync(id);
            if (uc == null)
                return NotFound("Registro não encontrado.");

            _context.UsuarioConquistas.Remove(uc);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
