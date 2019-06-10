using Coleta.Integracao;
using Coleta.ViewsModel;
using ColetaApi;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Coleta.Controllers
{
    public class QuestionariosController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var cliente = Api.CriaCliente())
            {
                var questionarios = await cliente.GetQuestionariosAsync();

                return View(questionarios);
            }
        }

        public async Task<ActionResult> Create()
        {
            var model = new QuestionarioPerguntaDto();

            using (var cliente = Api.CriaCliente())
            {
                model.Perguntas = cliente.GetPerguntasAsync().Result.Select(a => new PerguntaItemDto() { Descricao = a.Descricao, IdPergunta = a.Id }).ToList(); ;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Create(QuestionarioPerguntaDto questionario)
        {
            if (ModelState.IsValid)
            {
                using (var cliente = Api.CriaCliente())
                {
                    await cliente.PostQuestionarioAsync(new ColetaApi.Models.QuestionarioPerguntaDto()
                    {
                        Perguntas = questionario.SelectedPerguntas.Select(p => new ColetaApi.Models.PerguntaSelecionadaDto() { IdPergunta = int.Parse(p) }).ToList(),
                        Descricao = questionario.Descricao
                    });
                }
                return Json(true);
            }

            return Json(false);
        }

        public async Task<ActionResult> Delete(int id)
        {
            using (var cliente = Api.CriaCliente())
            {
                await cliente.DeleteQuestionarioAsync(id);
            }

            return RedirectToAction("Index");
        }
    }
}