using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColetaApi.Data;
using ColetaApi.Dtos;
using ColetaApi.Helper;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Coletas
        [HttpGet]
        public async Task<List<ColetaRespostaDto>> GetColetasAsync()
        {
            var coletas = await db.Coleta.ToListAsync(c => new ColetaRespostaDto(c));

            foreach (var item in coletas)
                item.NomeUsuario = db.Usuario.Where(u => u.Id == item.IdUsuario).First().Nome;

            return coletas;
        }
    }
}