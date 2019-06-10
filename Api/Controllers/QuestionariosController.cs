using System.Collections.Generic;
using System.Threading.Tasks;
using ColetaApi.Data;
using ColetaApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColetaApi.Helper;
using System.Linq;

namespace ColetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionariosController : ControllerBase
    {
        private readonly ColetaContext db;

        public QuestionariosController(ColetaContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionarioDto>>> GetQuestionariosAsync()
        {
            return Ok(await db.Questionario.ToListAsync(p => new QuestionarioDto(p)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionarioDto>> GetQuestionarioAsync(int id)
        {
            var questionario = await db.Questionario.FindAsync(id);
            if (questionario == null)
                return NotFound();

            return Ok(new QuestionarioDto(questionario));
        }

        [HttpGet("{id}/Perguntas")]
        public async Task<ActionResult<IEnumerable<PerguntaDto>>> GetPerguntasQuestionarioAsync(int id, [FromQuery] bool listaSimplificada = true)
        {
            var questionario = await db.Questionario.FindAsync(id);
            if (questionario == null)
                return NotFound();

            return await new PerguntasController(db).GetInternalAsync(listaSimplificada, id);
        }


        [HttpPost("{id}/Respostas")]
        public async Task<ActionResult> PostRespostasAsync(int id, [FromBody] ColetaDto dados)
        {
            var idUsuario = User.GetUserId();

            var coleta = new Coleta
            {
                Data = dados.Data,
                IdQuestionario = id,
                IdUsuario = idUsuario,
                Latitude = dados.Latitude,
                Longitude = dados.Longitude
            };


            db.Coleta.Add(coleta);

            foreach (var resposta in dados.Respostas)
            {
                db.RespostaColeta.Add(new RespostaColeta
                {
                    IdColetaNavigation = coleta,
                    IdOpcaoResposta = resposta.IdOpcaoResposta,
                    IdPergunta = resposta.IdPergunta,
                    Valor = resposta.Valor
                });
            }

            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<ActionResult> PostQuestionarioAsync([FromBody] QuestionarioPerguntaDto dados)
        {
            var idUsuario = User.GetUserId();

            var questionario = new Questionario
            {
                Nome = dados.Descricao,
                QuestionarioPergunta = dados.Perguntas.Select(a => new QuestionarioPergunta() { IdPergunta = a.IdPergunta }).ToList()
            };

            db.Questionario.Add(questionario);

            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteQuestionarioAsync(int id)
        {
            var questionario = await db.Questionario.Include(p => p.QuestionarioPergunta).FirstOrDefaultAsync(p => p.Id == id);

            if (questionario == null)
                return NotFound();
            else
            {
                foreach (var item in questionario.QuestionarioPergunta)
                    db.QuestionarioPergunta.Remove(item);

                db.Questionario.Remove(questionario);
                db.SaveChanges();
                return NoContent();
            }
        }
    }
}