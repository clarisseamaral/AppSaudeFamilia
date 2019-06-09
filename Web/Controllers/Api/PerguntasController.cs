using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Coleta.Models;
using Coleta.Models.Api;

namespace Coleta.Areas.Api.Controllers
{
    public class PerguntasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Perguntas
        public async Task<IEnumerable<PerguntaDto>> GetPerguntasAsync([FromUri] bool listaSimplificada = false)
        {
            db.Configuration.LazyLoadingEnabled = false;

            IQueryable<Pergunta> basePerguntas = db.Perguntas;
            if (!listaSimplificada)
                basePerguntas = basePerguntas.Include(p => p.OpcaoRespostaPerguntas).Include(p => p.TipoPergunta);

            var perguntas = await (from pergunta in basePerguntas
                                   where pergunta.FlgAtivo
                                   select pergunta).ToListAsync();

            return perguntas.Select(p => new PerguntaDto(p));
        }

        // GET: api/Perguntas/5
        [ResponseType(typeof(Pergunta))]
        public async Task<IHttpActionResult> GetPergunta(int id)
        {
            Pergunta pergunta = await db.Perguntas.FindAsync(id);
            if (pergunta == null)
                return NotFound();

            return Ok(new PerguntaDto(pergunta));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}