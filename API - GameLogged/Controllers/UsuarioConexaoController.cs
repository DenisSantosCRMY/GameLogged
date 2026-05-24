using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioConexaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioConexaoController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioConexao>>> GetConexoes()
        {
            var conexoes = await _context.UsuarioConexoes
                .Include(uc => uc.Usuario)
                .Include(uc => uc.Plataforma)
                .ToListAsync();
            return Ok(conexoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioConexao>> GetConexao(int id)
        {
            var conexao = await _context.UsuarioConexoes
                .Include(uc => uc.Usuario)
                .Include(uc => uc.Plataforma)
                .FirstOrDefaultAsync(uc => uc.id == id);

            if (conexao == null)
                return NotFound("Conexão não encontrada.");
            return Ok(conexao);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<UsuarioConexao>>> GetConexoesByUsuario(int idUsuario)
        {
            var conexoes = await _context.UsuarioConexoes
                .Include(uc => uc.Plataforma)
                .Where(uc => uc.id_user == idUsuario)
                .ToListAsync();
            return Ok(conexoes);
        }

        [HttpPost]
        public async Task<IActionResult> CriarConexao([FromBody] UsuarioConexao conexao)
        {
            _context.UsuarioConexoes.Add(conexao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConexao), new { id = conexao.id }, conexao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConexao(int id, [FromBody] UsuarioConexao conexaoUpdate)
        {
            if (id != conexaoUpdate.id)
                return BadRequest("ID não corresponde.");

            var conexaoExistente = await _context.UsuarioConexoes.FindAsync(id);
            if (conexaoExistente == null)
                return NotFound("Conexão não encontrada.");

            _context.Entry(conexaoExistente).CurrentValues.SetValues(conexaoUpdate);
            await _context.SaveChangesAsync();
            return Ok(conexaoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConexao(int id)
        {
            var conexao = await _context.UsuarioConexoes.FindAsync(id);
            if (conexao == null)
                return NotFound("Conexão não encontrada.");

            _context.UsuarioConexoes.Remove(conexao);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
