using System;
using System.Collections.Generic;
using Coleta.Integracao;
using Coleta.Models;
using ColetaApi;
using ColetaApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Coleta.Controllers
{
    public class ColetasRespostaController : Controller
    {
        // GET: ColetasResposta
        public async Task<ActionResult> Index(string searchString)
        {
            using (var cliente = Api.CriaCliente())
            {
                var coletas = await cliente.GetColetasAsync();

                if (!String.IsNullOrEmpty(searchString))
                    coletas = coletas.Where(s => s.NomeUsuario.Contains(searchString)).ToList();

                return View(coletas);
            }
        }

        // GET: ColetasResposta/Details/5
        public ActionResult Details(int id)
        {



            return View();
        }
        
    }
}
