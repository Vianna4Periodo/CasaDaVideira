using CasaDaVideira.Model.Database;
using Mvc.Model.Utils;
using System;
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
            if (DbConfig.Instance.UsuarioRepository.SystemHasAdmin())
            {
                return PartialView("_Login", LoginUtils.Usuario);
            }
            else
            {
                return PartialView("_CadastraAdmin");
            }
            
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
            throw new Exception("Tentativa de fraude!");
        }
    }
}