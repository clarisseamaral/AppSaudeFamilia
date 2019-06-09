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
    public class ColetoresController : Controller
    {
        Contexto db;

        public ColetoresController()
        {
            db = new Contexto();
        }

        public async Task<ActionResult> Index(string searchString)
        {
            using (var cliente = Api.CriaCliente())
            {
                var coletores = await cliente.GetUsuariosAsync();

                if (!String.IsNullOrEmpty(searchString))
                {
                    coletores = coletores.Where(s => s.Nome.Contains(searchString)).ToList();
                }

                return View(coletores);

            }
           
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