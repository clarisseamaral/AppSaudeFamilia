using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ColetaApi.Data;
using ColetaApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColetaApi.Helper;

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
        public Task<List<QuestionarioDto>> GetQuestionariosAsync()
        {
            return db.Questionario.ToListAsync(p => new QuestionarioDto(p));
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
                //coleta.RespostaColeta.Add(new RespostaColeta
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
    }
}