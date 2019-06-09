using Coleta.Models;
using Coleta.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index(string searchString)
        {
            var perguntas = from m in db.Perguntas
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                perguntas = perguntas.Where(s => s.Descricao.Contains(searchString));
            }

            return View(perguntas);
        }

        public ActionResult Edit(int id)
        {
            var model = new CadastroPergunta();
            model.Pergunta = db.Perguntas.Find(id);
            model.TiposPergunta = db.TipoPerguntas.ToList();
            model.OpcaoRespostaPergunta = db.OpcaoRespostaPerguntas.Where(o=> o.idPergunta == id).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CadastroPergunta();
            model.Pergunta = new Pergunta();
            model.TiposPergunta = db.TipoPerguntas.ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(Pergunta pergunta)
        {
            //TODO: corrigir exceção quando algum dos campos obrigatórios não for preenchido

            if (ModelState.IsValid)
            {
                pergunta.FlgAtivo = true;

                if(pergunta.IdTipoPergunta == 1 && pergunta.OpcaoRespostaPerguntas.Count == 0)
                    return Json(false);

                db.Perguntas.Add(pergunta);
                db.SaveChanges();
                return Json(true);
            }

            return Json(false);
        }


        [HttpPost]
        public JsonResult Edit(Pergunta pergunta)
        {
            //TODO: corrigir exceção quando algum dos campos obrigatórios não for preenchido

            if (ModelState.IsValid)
            {
                try
                {
                    if (pergunta.IdTipoPergunta == 1 && pergunta.OpcaoRespostaPerguntas.Count == 0)
                        return Json(false);

                    var perguntaAntiga = db.Perguntas.Find(pergunta.Id);

                    perguntaAntiga.FlgAtivo = pergunta.FlgAtivo;
                    perguntaAntiga.Descricao = pergunta.Descricao;
                    perguntaAntiga.IdTipoPergunta = pergunta.IdTipoPergunta;
                    
                    //TODO: alterar opções resposta (não funciona quando remove na edição)
                    //Incluir validação: não permitir edição quando a pergunta tiver sido respondida
                    perguntaAntiga.OpcaoRespostaPerguntas = pergunta.OpcaoRespostaPerguntas;

                    TryUpdateModel(perguntaAntiga);

                    db.SaveChanges();

                    return Json(true);
                }
                catch (Exception ex)
                {
                    return Json(false);
                }

            }

            return Json(false);
        }

        public ActionResult Delete(int id)
        {
            var opcoes = db.OpcaoRespostaPerguntas.Where(o => o.idPergunta == id).ToList();

            foreach (var item in opcoes)
                db.OpcaoRespostaPerguntas.Remove(item);

            var pergunta = db.Perguntas.Find(id);
            db.Perguntas.Remove(pergunta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var pergunta = db.Perguntas.Find(id);
            return View(pergunta);
        }
    }
}