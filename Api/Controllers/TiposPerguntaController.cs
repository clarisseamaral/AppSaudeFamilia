using System.Collections.Generic;
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
    public class TiposPerguntaController : ControllerBase
    {
        private readonly ColetaContext db;

        public TiposPerguntaController(ColetaContext db)
        {
            this.db = db;
        }

        // GET: api/TipoPergunta/5
        [HttpGet]
        public async Task<ActionResult<List<TipoPerguntaDto>>> GetTipoPerguntaAsync()
        {
            return Ok(await db.TipoPergunta.ToListAsync(p => new TipoPerguntaDto(p)));
        }
    }
}