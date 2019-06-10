using Coleta.Integracao;
using ColetaApi;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    }
}