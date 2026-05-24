using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CatalogoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetCatalogos()
        {
            var catalogos = await _context.Catalogos
                .Include(c => c.Jogo)
                .Include(c => c.Usuario)
                .ToListAsync();
            return Ok(catalogos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Catalogo>> GetCatalogo(int id)
        {
            var catalogo = await _context.Catalogos
                .Include(c => c.Jogo)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.id == id);

            if (catalogo == null)
                return NotFound("Catálogo não encontrado.");
            return Ok(catalogo);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetCatalogoByUsuario(int idUsuario)
        {
            var catalogos = await _context.Catalogos
                .Include(c => c.Jogo)
                .Where(c => c.id_user == idUsuario)
                .ToListAsync();
            return Ok(catalogos);
        }

        [HttpPost]
        public async Task<IActionResult> CriarCatalogo([FromBody] Catalogo catalogo)
        {
            _context.Catalogos.Add(catalogo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCatalogo), new { id = catalogo.id }, catalogo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogo(int id, [FromBody] Catalogo catalogoUpdate)
        {
            if (id != catalogoUpdate.id)
                return BadRequest("ID não corresponde.");

            var catalogoExistente = await _context.Catalogos.FindAsync(id);
            if (catalogoExistente == null)
                return NotFound("Catálogo não encontrado.");

            _context.Entry(catalogoExistente).CurrentValues.SetValues(catalogoUpdate);
            await _context.SaveChangesAsync();
            return Ok(catalogoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
                return NotFound("Catálogo não encontrado.");

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
