using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlataformaController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PlataformaController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPlataforma(Plataforma plataforma)
        {
            _appDbContext.Plataforma.Add(plataforma);

            await _appDbContext.SaveChangesAsync();
            return Ok(plataforma);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plataforma>>> GetPlataformas()
        {
            var plataformas = await _appDbContext.Plataforma.ToListAsync();
            return Ok(plataformas);
        }
    }
}