using Coleta.Integracao;
using Coleta.Models;
using Coleta.ViewsModel;
using ColetaApi;
using ColetaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coleta.Controllers
{
    public class PerguntasController : Controller
    {
        Contexto db;

        public PerguntasController()
        {
            db = new Contexto();
        }

        public async Task<ActionResult> Index(string searchString)
        {
            using (var cliente = Api.CriaCliente())
            {
                IEnumerable<PerguntaDto> perguntas = await cliente.GetPerguntasAsync();

                if (!String.IsNullOrEmpty(searchString))
                    perguntas = perguntas.Where(s => s.Descricao.Contains(searchString));

                return View(perguntas);
            }
        }

        //public async Task<ActionResult> Edit(int id)
        //{
        //    var model = new CadastroPergunta();

        //    using (var cliente = Api.CriaCliente())
        //    {
        //        var pergunta = await cliente.GetPerguntaAsync(id);
        //        model.Pergunta = pergunta;
        //        model.TiposPergunta = await cliente.GetTipoPerguntaAsync();

        //        return View(pergunta);
        //    }
        //}

        public async Task<ActionResult> Create()
        {
            var model = new CadastroPergunta();
            model.Pergunta = new PerguntaDto();

            using (var cliente = Api.CriaCliente())
            {
               model.TiposPergunta = await cliente.GetTipoPerguntaAsync();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Pergunta pergunta)
        {
            //TODO: corrigir exceção quando algum dos campos obrigatórios não for preenchido
            if (ModelState.IsValid)
            {
                if (pergunta.IdTipoPergunta == 1 && pergunta.OpcaoRespostaPerguntas.Count == 0)
                    return Json(false);

                using (var cliente = Api.CriaCliente())
                {
                    await cliente.PostPerguntasAsync(new PerguntaDto()
                    {
                        IdTipoPergunta = pergunta.IdTipoPergunta,
                        TipoPergunta = "tipopergunta", 
                        Alternativas = pergunta.OpcaoRespostaPerguntas.Select(a => new AlternativaDto() { Texto = a.opcao }).ToList(),
                        Descricao = pergunta.Descricao
                    });
                }
                return Json(true);
            }

            return Json(false);
        }


        //[HttpPost]
        //public JsonResult Edit(Pergunta pergunta)
        //{
        //    //TODO: corrigir exceção quando algum dos campos obrigatórios não for preenchido

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (pergunta.IdTipoPergunta == 1 && pergunta.OpcaoRespostaPerguntas.Count == 0)
        //                return Json(false);

        //            var perguntaAntiga = db.Perguntas.Find(pergunta.Id);

        //            perguntaAntiga.FlgAtivo = pergunta.FlgAtivo;
        //            perguntaAntiga.Descricao = pergunta.Descricao;
        //            perguntaAntiga.IdTipoPergunta = pergunta.IdTipoPergunta;

        //            //TODO: alterar opções resposta (não funciona quando remove na edição)
        //            //Incluir validação: não permitir edição quando a pergunta tiver sido respondida
        //            perguntaAntiga.OpcaoRespostaPerguntas = pergunta.OpcaoRespostaPerguntas;

        //            TryUpdateModel(perguntaAntiga);

        //            db.SaveChanges();

        //            return Json(true);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(false);
        //        }

        //    }

        //    return Json(false);
        //}

        public async Task<ActionResult> Delete(int id)
        {
            using (var cliente = Api.CriaCliente())
            {
                await cliente.DeletePerguntaAsync(id);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = new CadastroPergunta();

            using (var cliente = Api.CriaCliente())
            {
                var pergunta = await cliente.GetPerguntaAsync(id);
                model.Pergunta = pergunta;
                model.TiposPergunta = await cliente.GetTipoPerguntaAsync();
                model.OpcaoRespostaPergunta = pergunta.Alternativas.Select(a => new OpcaoRespostaPergunta() { opcao = a.Texto }).ToList();
                return View(model);
            }
        }
    }
}