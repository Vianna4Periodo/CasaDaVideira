using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using Mvc.Model.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            var produtos = DbConfig.Instance.ProdutoRepository.FindAll();

            return View(produtos);
        }

        public PartialViewResult CreateProduto()
        {
            if (LoginUtils.Usuario != null && LoginUtils.Usuario.Admin)
            {
                var tipos = DbConfig.Instance.CategoriaRepository.FindAll();

                ViewBag.Categorias = new SelectList(tipos, "Id", "Nome");

                return PartialView("_CadastraProduto", new Produto());
            }
                
            throw new Exception("Tentativa ilegal de acesso - Not Admin user");
        }
        public ActionResult UpdateProduto(Produto produto)
        {
            Produto p = DbConfig.Instance.ProdutoRepository.FindFirstById(produto.Id);

            p.Preco = produto.Preco;
            p.Categoria = produto.Categoria;
            p.Classificacao = produto.Classificacao;
            p.DescricaoCompleta = produto.DescricaoCompleta;
            p.DescricaoResumida = produto.DescricaoResumida;
            p.Imagens = produto.Imagens;
            p.Nome = produto.Nome;
            p.Oferta = produto.Oferta;
            p.Peso = produto.Peso;

            DbConfig.Instance.ProdutoRepository.Update(p);

            return View("Index", DbConfig.Instance.ProdutoRepository.FindAll());
        }
        public ActionResult EditProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.Id == idProduto);

            return View(prod);
        }

        public ActionResult DeleteProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.Id == idProduto);

            DbConfig.Instance.ProdutoRepository.Delete(prod);

            return RedirectToAction("Index");
        }

        public ActionResult DetailsProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository
                    .FindAll().FirstOrDefault(f => f.Id == idProduto);

            return View(prod);
        }

        public ActionResult GravarProduto(Produto prod, Guid idCategoria)
        {
            prod.Categoria = DbConfig.Instance.CategoriaRepository.FindFirstById(idCategoria);
            
            DbConfig.Instance.ProdutoRepository.Save(prod);
            //return View("Telefones");
            return RedirectToAction("Index");

        }

        public ActionResult AddImagem(Guid idProduto)
        {
            Produto produto = DbConfig.Instance.ProdutoRepository.FindFirstById(idProduto);
            var imagem = new Imagem(produto);
            return View("AddImagem", "_Layout", imagem);
        }

        public ActionResult BuscarProduto(string buscaP)
        {
            if (buscaP == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(f => f.Nome.ToLower().Contains(buscaP.ToLower()));
            return View("Index", prod);
        }
        
    }
}