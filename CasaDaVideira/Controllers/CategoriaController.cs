using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            var categorias = DbConfig.Instance.CategoriaRepository.FindAll();
            return View(categorias);
        }

        public ActionResult CreateCategoria()
        {
            var categoria = new Categoria();
            return View(categoria);
        }

        public ActionResult GravarCategoria(Categoria categoria)
        {
            DbConfig.Instance.CategoriaRepository.Salvar(categoria);
            return RedirectToAction("Index");
        }
    }
}