using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosPlataformasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JogosPlataformasController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoPlataforma>>> GetJogosPlataformas()
        {
            var lista = await _context.JogosPlataformas
                .Include(jp => jp.Jogo)
                .Include(jp => jp.Plataforma)
                .ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JogoPlataforma>> GetJogoPlataforma(int id)
        {
            var jp = await _context.JogosPlataformas
                .Include(j => j.Jogo)
                .Include(j => j.Plataforma)
                .FirstOrDefaultAsync(j => j.id_jpjogos == id);

            if (jp == null)
                return NotFound("Registro não encontrado.");
            return Ok(jp);
        }

        [HttpGet("jogo/{idJogo}")]
        public async Task<ActionResult<IEnumerable<JogoPlataforma>>> GetPlataformasByJogo(int idJogo)
        {
            var lista = await _context.JogosPlataformas
                .Include(jp => jp.Plataforma)
                .Where(jp => jp.id_jogo == idJogo)
                .ToListAsync();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> CriarJogoPlataforma([FromBody] JogoPlataforma jogoPlataforma)
        {
            _context.JogosPlataformas.Add(jogoPlataforma);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJogoPlataforma), new { id = jogoPlataforma.id_jpjogos }, jogoPlataforma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogoPlataforma(int id)
        {
            var jp = await _context.JogosPlataformas.FindAsync(id);
            if (jp == null)
                return NotFound("Registro não encontrado.");

            _context.JogosPlataformas.Remove(jp);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
