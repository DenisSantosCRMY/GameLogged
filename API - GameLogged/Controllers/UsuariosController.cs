using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController] // Define que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota base para este controlador, onde [controller] será substituído por "usuarios"
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        //constructor para injetar o AppDbContext
        public UsuarioController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //efetuar a consulta para verificar se o usuário existe, se não existir, criar um novo usuário
        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            _appDbContext.Usuario.Add(usuario);

            await _appDbContext.SaveChangesAsync();
            return Ok(usuario);

        }

        //efetuar a consulta para verificar se o usuário existe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuario = await _appDbContext.Usuario.ToListAsync();
            return Ok(usuario);
        }

        //efetuar a consulta para verificar se o usuário existe pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _appDbContext.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Não foi localizado");
            }
            return Ok(usuario);
        }

        //efetuar a consulta para verificar se o usuário existe pelo ID, se não existir, criar um novo usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuarioUpdate)
        {
            if (id != usuarioUpdate.id)
            {
                return BadRequest("ID do usuário não corresponde ao ID fornecido.");
            }

            var usuarioExistente = await _appDbContext.Usuario.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _appDbContext.Entry(usuarioExistente).CurrentValues.SetValues(usuarioUpdate);

            await _appDbContext.SaveChangesAsync();
            return StatusCode(201, usuarioExistente);
        }

        //efetuar a consulta para verificar se o usuário existe pelo ID, se existir, excluir o usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuarioExistente = await _appDbContext.Usuario.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _appDbContext.Usuario.Remove(usuarioExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}