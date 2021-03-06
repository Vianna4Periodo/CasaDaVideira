﻿using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using System;
using System.Web;
using System.Web.Security;

namespace CasaDaVideira.Model.Database.Utils
{
    public class LoginUtils
    {
        private static Usuario _usuario;
        public static Usuario Usuario
        {
            get
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = null;

                if (authCookie != null)
                {
                    ticket = FormsAuthentication.Decrypt(authCookie.Value);
                }

                return ticket != null ? _usuario : null;
            }
            set { _usuario = value; }
        }
        public static Usuario Logar(string login, string senha)
        {
            try
            {
                var usuario = DbConfig.Instance.UsuarioRepository.Buscar(login, senha);
                Usuario = usuario ?? throw new Exception("Usuario não encontrado!");
                if (!usuario.Admin)
                {
                    usuario.Carrinho = new Carrinho() { Usuario = usuario };
                    DbConfig.Instance.UsuarioRepository.Update(usuario);
                }
               

                FormsAuthentication.SetAuthCookie(usuario.Email, true);

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível logar!", ex);
            }
        }

        public static void Deslogar()
        {
            try
            {
                if (!Usuario.Admin)
                {                 
                    Usuario.Carrinho.Ativo = false;                    
                }
                Usuario.UltimoAcesso = DateTime.Now;
                DbConfig.Instance.UsuarioRepository.SaveOrUpdate(Usuario);
                Usuario = null;
                _usuario = null;
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível deslogar!", ex);
            }
        }
    }
}
