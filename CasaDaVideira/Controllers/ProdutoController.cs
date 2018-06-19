﻿using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using CasaDaVideira.Model.Database.Utils;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            var produtos = DbConfig.Instance.ProdutoRepository.FindAll();
            ViewBag.Categorias = DbConfig.Instance.CategoriaRepository.FindAll();
            var qtdProdutos = DbConfig.Instance.ProdutoRepository.CountProdutos();
            int paginas = qtdProdutos / 9;
            if (qtdProdutos % 9 != 0)
                paginas++;
            ViewBag.QtdPaginas = paginas;
            return View(produtos);
        }    
        public ActionResult Listar()
        {
            var produtos = DbConfig.Instance.ProdutoRepository.FindAll();
            return View(produtos);
        }    
        public PartialViewResult CreateProduto()
        {
            if (LoginUtils.Usuario != null && LoginUtils.Usuario.Admin)
            {
                var categs = DbConfig.Instance.CategoriaRepository.FindAll();

                ViewBag.Categorias = new SelectList(categs, "Id", "Nome");

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

            return View("Listar", DbConfig.Instance.ProdutoRepository.FindAll());
        }
        public ActionResult EditProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.Id == idProduto);

            return View("_EditProduto",prod);
        }
        public ActionResult DeleteProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.Id == idProduto);
            prod.Ativo = false;
            DbConfig.Instance.ProdutoRepository.Update(prod);

            return RedirectToAction("Listar");
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
        public PartialViewResult AddImagem(Guid idProduto)
        {
            Produto produto = DbConfig.Instance.ProdutoRepository.FindFirstById(idProduto);
            return PartialView("AddImagem", produto);
        }    
        public ActionResult InsereImagem(HttpPostedFileBase file, Guid idProduto)
        {
            if (file != null && file.ContentLength > 0)
            {
                var produto = DbConfig.Instance.ProdutoRepository.FindFirstById(idProduto);

                
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);

                
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/uploads/produtos/"), fileName);
                file.SaveAs(path);

                var caminho = "images/uploads/produtos/" + fileName;
                var imagem = new Imagem()
                {
                    DataInclusao = DateTime.Now,
                    Produto = produto,
                    Caminho = caminho                    
                };
                DbConfig.Instance.ImagemRepository.Save(imagem);
                produto.Imagens.Add(imagem);
                DbConfig.Instance.ProdutoRepository.Update(produto);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
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

        public ActionResult DetalheProduto(Guid idProduto)
        {
            var produto = DbConfig.Instance.ProdutoRepository.FindFirstById(idProduto);
            return View(produto);
        }
        
    }
}