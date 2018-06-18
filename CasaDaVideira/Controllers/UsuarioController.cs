using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using CasaDaVideira.Model.Database.Utils;
using Mvc.Model.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (LoginUtils.Usuario.Admin)
            {
                var usuarios = DbConfig.Instance.UsuarioRepository.FindAll();
                return View(usuarios);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public PartialViewResult CreateUser(Boolean admin = false)
        {
            var user = new Usuario();
            if (LoginUtils.Usuario != null)
            {
                if (LoginUtils.Usuario.Admin)
                {
                    user.Admin = admin;
                }
            }
            return PartialView("_CreateUser", user);
        }

        public ActionResult EditUser(Guid idUsuario)
        {
            Usuario user;
            if (LoginUtils.Usuario.Admin)
                user = DbConfig.Instance.UsuarioRepository.FindFirstById(idUsuario);
            else
                user = LoginUtils.Usuario;

            return View("CreateUser", user);
        }

        public ActionResult DeleteUser(Guid idUsuario)
        {
            if (LoginUtils.Usuario.Admin)
            {
                var user = DbConfig.Instance.UsuarioRepository.FindFirstById(idUsuario);
                DbConfig.Instance.UsuarioRepository.Delete(user);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DetailsUser()
        {
            var user = LoginUtils.Usuario;

            return View(user);
        }

        [AllowAnonymous]
        public PartialViewResult GravarUsuario(Usuario user)
        {
            try
            {
                if (user.Admin)
                {
                    DbConfig.Instance.UsuarioRepository.Update(user);
                }
                else
                {
                    DbConfig.Instance.UsuarioRepository.SaveOrUpdate(user);
                }
            }
            catch
            {
                throw new Exception("Erro ao gravar o usuário");
            }
            LoginUtils.Logar(user.Email, user.Senha);
            return PartialView("_LoginMenu",LoginUtils.Usuario);
        }

        public ActionResult BuscarUsuario(string busca)
        {
            if (busca == "")
            {
                return RedirectToAction("Index");
            }

            var user = DbConfig.Instance.UsuarioRepository
                    .FindAll().Where(f => f.Nome.ToLower().Contains(busca.ToLower()));

            return View("Index", user);
        }

        public ActionResult Telefones(Guid id)
        {
            var user = DbConfig.Instance.TelefoneRepository.FirstTel(id);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CriarTelefone(int id, int idUsuario)
        {
            var tel = new Telefone();
            ViewData["IdUsuario"] = idUsuario;

            return View(tel);
        }

        public ActionResult GravarTelefone(Telefone telefone, Guid idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FindFirstById(idUsuario);

            telefone.Usuario = user;
            try
            {
                DbConfig.Instance.TelefoneRepository.Save(telefone);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Enderecos", user);
        }

        public ActionResult EditarTelefone(Guid id)
        {
            Telefone tel;
            try
            {
                tel = DbConfig.Instance.TelefoneRepository.FirstTel(id);
                ViewData["IdUsuario"] = tel.Usuario.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("CriarTelefone", tel);
        }

        public ActionResult DeletarTelefone(Guid id)
        {
            try
            {
                var tel = DbConfig.Instance.TelefoneRepository.FirstTel(id);
                DbConfig.Instance.TelefoneRepository.Delete(tel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Enderecos(Guid id)
        {
            var user = DbConfig.Instance.EnderecoRepository.FindFirstById(id);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CriarEndereco(int id, int idUsuario)
        {
            var tel = new Endereco();
            ViewData["IdUsuario"] = idUsuario;

            return View(tel);
        }

        public ActionResult GravarEndereco(Endereco endereco, Guid idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FindFirstById(idUsuario);
            endereco.Usuario = user;
            DbConfig.Instance.EnderecoRepository.Save(endereco);
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult EditarEndereco(Guid id)
        {
            var end = DbConfig.Instance.EnderecoRepository.FindFirstById(id);

            ViewData["IdUsuario"] = end.Usuario.Id;
            return View("CriarEndereco", end);

        }

        public ActionResult DeletarEndereco(Guid id)
        {
            try
            {
                var end = DbConfig.Instance.EnderecoRepository.FindFirstById(id);
                DbConfig.Instance.EnderecoRepository.Delete(end);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public PartialViewResult FazerLogin(string email, string senha)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                    throw new Exception("Email e senha são obrigatórios");
                if (DbConfig.Instance.UsuarioRepository.SystemHasAdmin())
                {
                    LoginUtils.Logar(email, senha);
                }
                else
                {
                    throw new Exception("É preciso ter um administrador!");
                }
                return PartialView("_LoginMenu", LoginUtils.Usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao logar! ", ex);
            }
        }

        public PartialViewResult CadastraAdmin(string email, string senha)
        {
            var u = new Usuario();
            var iniFile = IniUtils.LerArquivoIni();
            if (email.Equals(iniFile["AdminFirstUser"]["key"]) && senha.Equals(iniFile["AdminFirstUser"]["password"]))
            {
                u.Admin = true;
                u.Email = email;
                u.Nome = "";
                u.Sobrenome = "";
                u.Senha = senha;
                u.Cpf = "";
            }
            return PartialView("_CreateUser", DbConfig.Instance.UsuarioRepository.Save(u));
    }

    public ActionResult Deslogar(Usuario user)
    {
        LoginUtils.Deslogar();

        return RedirectToAction("Index", "Home");
    }
}
}
