using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColetaApi.Data;
using ColetaApi.Dtos;
using ColetaApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase

    {
        private readonly ColetaContext db;

        public UsuariosController(ColetaContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> GetUsuariosAsync()
        {
            return Ok(await db.Usuario.Where(u => u.AcessoAdministrador == false).ToListAsync(u => new UsuarioDto(u)));
        }

    }
}