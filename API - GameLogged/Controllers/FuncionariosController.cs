using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController] // Define que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota base para este controlador, onde [controller] será substituído por "funcionarios"
    public class FuncionarioController : ControllerBase
    {
        //constructor para injetar o AppDbContext
        private readonly AppDbContext _context;

        //constructor para injetar o AppDbContext
        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        
        //efetuar a consulta para verificar se o funcionário existe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionario.ToListAsync();
            return Ok(funcionarios);
        }

        //efetuar a consulta para verificar se o funcionário existe pelo RF
        [HttpGet("{rf}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int rf)
        {
            var funcionario = await _context.Funcionario.FindAsync(rf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");
            return Ok(funcionario);
        }

        //efetuar a consulta para verificar se o funcionário existe pelo RF, se não existir, criar um novo funcionário
        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] Funcionario funcionario)
        {
            _context.Funcionario.Add(funcionario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFuncionario), new { rf = funcionario.rf }, funcionario);
        }

        //efetuar a atualização de um funcionário existente
        [HttpPut("{rf}")]
        public async Task<IActionResult> UpdateFuncionario(int rf, [FromBody] Funcionario funcionarioUpdate)
        {
            if (rf != funcionarioUpdate.rf)
                return BadRequest("RF não corresponde.");

            var funcionarioExistente = await _context.Funcionario.FindAsync(rf);
            if (funcionarioExistente == null)
                return NotFound("Funcionário não encontrado.");

            _context.Entry(funcionarioExistente).CurrentValues.SetValues(funcionarioUpdate);
            await _context.SaveChangesAsync();
            return Ok(funcionarioExistente);
        }

        //efetuar a exclusão de um funcionário existente
        [HttpDelete("{rf}")]
        public async Task<IActionResult> DeleteFuncionario(int rf)
        {
            var funcionario = await _context.Funcionario.FindAsync(rf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
