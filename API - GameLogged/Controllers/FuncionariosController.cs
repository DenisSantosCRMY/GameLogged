using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.ToListAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{rf}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int rf)
        {
            var funcionario = await _context.Funcionarios.FindAsync(rf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");
            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFuncionario), new { rf = funcionario.rf }, funcionario);
        }

        [HttpPut("{rf}")]
        public async Task<IActionResult> UpdateFuncionario(int rf, [FromBody] Funcionario funcionarioUpdate)
        {
            if (rf != funcionarioUpdate.rf)
                return BadRequest("RF não corresponde.");

            var funcionarioExistente = await _context.Funcionarios.FindAsync(rf);
            if (funcionarioExistente == null)
                return NotFound("Funcionário não encontrado.");

            _context.Entry(funcionarioExistente).CurrentValues.SetValues(funcionarioUpdate);
            await _context.SaveChangesAsync();
            return Ok(funcionarioExistente);
        }

        [HttpDelete("{rf}")]
        public async Task<IActionResult> DeleteFuncionario(int rf)
        {
            var funcionario = await _context.Funcionarios.FindAsync(rf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
