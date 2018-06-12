using CasaDaVideira.Model.Database;
using Mvc.Model.Utils;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var existeAdmin = DbConfig.Instance.UsuarioRepository.FindAll().Where(w => w.Admin).FirstOrDefault();
            ViewBag.ExisteAdmin = existeAdmin == null ? false : true;
            return View("Login", LoginUtils.Usuario);
        }

        
        public ActionResult Configuracoes()
        {
            if(LoginUtils.Usuario != null)
            {
                if (LoginUtils.Usuario.Admin)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
    }
}