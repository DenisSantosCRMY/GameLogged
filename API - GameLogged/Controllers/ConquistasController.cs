using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConquistasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConquistasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conquista>>> GetConquistas()
        {
            var conquistas = await _context.Conquistas
                .Include(c => c.Jogo)
                .ToListAsync();
            return Ok(conquistas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conquista>> GetConquista(int id)
        {
            var conquista = await _context.Conquistas
                .Include(c => c.Jogo)
                .FirstOrDefaultAsync(c => c.id == id);

            if (conquista == null)
                return NotFound("Conquista não encontrada.");
            return Ok(conquista);
        }

        [HttpGet("jogo/{idJogo}")]
        public async Task<ActionResult<IEnumerable<Conquista>>> GetConquistasByJogo(int idJogo)
        {
            var conquistas = await _context.Conquistas
                .Where(c => c.id_jp == idJogo)
                .ToListAsync();
            return Ok(conquistas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarConquista([FromBody] Conquista conquista)
        {
            _context.Conquistas.Add(conquista);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConquista), new { id = conquista.id }, conquista);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConquista(int id, [FromBody] Conquista conquistaUpdate)
        {
            if (id != conquistaUpdate.id)
                return BadRequest("ID não corresponde.");

            var conquistaExistente = await _context.Conquistas.FindAsync(id);
            if (conquistaExistente == null)
                return NotFound("Conquista não encontrada.");

            _context.Entry(conquistaExistente).CurrentValues.SetValues(conquistaUpdate);
            await _context.SaveChangesAsync();
            return Ok(conquistaExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConquista(int id)
        {
            var conquista = await _context.Conquistas.FindAsync(id);
            if (conquista == null)
                return NotFound("Conquista não encontrada.");

            _context.Conquistas.Remove(conquista);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
