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
    public class ColetasRespostaController : ControllerBase
    {
        private readonly ColetaContext db;

        public ColetasRespostaController(ColetaContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<ColetaRespostaDto>> GetColetasRespostaAsync()
        {
            var coletas = await db.Coleta.OrderByDescending(o=> o.Data).ToListAsync(c => new ColetaRespostaDto(c));

            foreach (var item in coletas)
                item.NomeUsuario = db.Usuario.Where(u => u.Id == item.IdUsuario).First().Nome;

            return coletas;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DetalhesRespostaDto>>> GetColetasRespostaDetalhesAsync(int id)
        {
            var coletas = await db.RespostaColeta.Include(c => c.IdPerguntaNavigation)
                                                        .Include(c => c.IdOpcaoRespostaNavigation)
                                                        .Where(c=> c.IdColeta == id)
                                                        .ToListAsync(c => new DetalhesRespostaDto(c));

            if (coletas == null)
                return NotFound();

            return Ok(coletas);
        }

    }
}