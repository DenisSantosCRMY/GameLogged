using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JogosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            var jogos = await _context.Jogos.ToListAsync();
            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> GetJogo(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null)
                return NotFound("Jogo não encontrado.");
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<IActionResult> CriarJogo([FromBody] Jogo jogo)
        {
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJogo), new { id = jogo.id }, jogo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJogo(int id, [FromBody] Jogo jogoUpdate)
        {
            if (id != jogoUpdate.id)
                return BadRequest("ID não corresponde.");

            var jogoExistente = await _context.Jogos.FindAsync(id);
            if (jogoExistente == null)
                return NotFound("Jogo não encontrado.");

            _context.Entry(jogoExistente).CurrentValues.SetValues(jogoUpdate);
            await _context.SaveChangesAsync();
            return Ok(jogoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogo(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null)
                return NotFound("Jogo não encontrado.");

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
