using Coleta.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Coleta.Controllers
{
    public class ColetoresController : Controller
    {
        Contexto db;

        public ColetoresController()
        {
            db = new Contexto();
        }

        public ActionResult Index(string searchString)
        {
            var coletas = from m in db.Coletores
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                coletas = coletas.Where(s => s.nome.Contains(searchString));
            }

            return View(coletas);
        }

        public ActionResult Edit(int id)
        {
            var coletor = db.Coletores.Find(id);
            return View(coletor);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Coletor coletor)
        {
            if (ModelState.IsValid)
            {
                db.Coletores.Add(coletor);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(coletor);
        }

        [HttpPost]
        public ActionResult Edit(Coletor coletor)
        {
            //TODO: melhorar exceção quando algum dos campos obrigatórios não for preenchido

            if (ModelState.IsValid)
            {
                try
                {
                    var coletorAntigo = db.Coletores.Find(coletor.id);

                    coletorAntigo.nome = coletor.nome;

                    TryUpdateModel(coletorAntigo);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                }

            }

            return View();

        }

        public ActionResult Delete(int id)
        {
            var coletor = db.Coletores.Find(id);
            db.Coletores.Remove(coletor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}