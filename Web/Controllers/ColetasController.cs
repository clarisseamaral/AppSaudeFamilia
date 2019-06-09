using Coleta.Models;
using System.Linq;
using System.Web.Mvc;

namespace Coleta.Controllers
{
    public class ColetasController : Controller
    {
        Contexto db;

        public ColetasController()
        {
            db = new Contexto();
        }

        public ActionResult Index()
        {
            var coletas = from m in db.Coletas
                            select m;

            return View(coletas);
        }
    }
}