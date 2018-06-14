using CasaDaVideira.Model.Database;
using Mvc.Model.Utils;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index() => View();

        [AllowAnonymous]
        public PartialViewResult Login()
        {
            var existeAdmin = DbConfig.Instance.UsuarioRepository.FindAll().Where(w => w.Admin).FirstOrDefault();
            ViewBag.ExisteAdmin = existeAdmin != null ? true : false;
            return PartialView("_Login", LoginUtils.Usuario);
        }

        
        public PartialViewResult Configuracoes()
        {
            if(LoginUtils.Usuario != null)
            {
                if (LoginUtils.Usuario.Admin)
                {
                    return PartialView("_Configuracoes");
                }
            }
            return PartialView("_IndexContent");
        }
    }
}