﻿using CasaDaVideira.Model.Database.Model;
using CasaDaVideira.Model.Database.Repository;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public UsuarioRepository(ISession session) : base(session)
        {

        }
      
        public Usuario GetUserByLoginAndPassword(string login, string password)
        {
            try
            {
                var user = this.Session.Query<Usuario>().FirstOrDefault(f => f.Email == login && f.Senha == password);
                return user;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public IEnumerable<Usuario> FindUsuariosByAdminStatus(bool isAdmin = true)
        {
            try
            {
                var users = this.Session.CreateCriteria(typeof(Usuario)).List<Usuario>().Where(f => f.Admin == isAdmin);
                return users;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Usuario FindUserByEmail(string email)
        {
            try
            {
                var user = this.Session.Query<Usuario>().FirstOrDefault(f => f.Email == email);
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
        }

        public bool SystemHasAdmin()
        {
            var user = this.Session.Query<Usuario>().FirstOrDefault(f => f.Admin == true);
            return user != null ? true : false;
        }

        public Usuario Buscar(string email, string senha)
        {
            return this.Session.Query<Usuario>().FirstOrDefault(f => f.Senha == senha && f.Email == email);
        }
    }
}
