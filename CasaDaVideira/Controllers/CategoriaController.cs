using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    using Mvc.Model.Utils;

    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            var categorias = DbConfig.Instance.CategoriaRepository.FindAll();
            return View(categorias);
        }

        public PartialViewResult CreateCategoria()
        {
            if (LoginUtils.Usuario != null && LoginUtils.Usuario.Admin)
                return PartialView("_CadastroCategoria", new Categoria());
            throw new Exception("Tentativa ilegal de acesso - Not Admin user");
        }

        public ActionResult GravarCategoria(Categoria categoria)
        {
            DbConfig.Instance.CategoriaRepository.Save(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string s)
        {
            throw new NotImplementedException();
        }
    }
}