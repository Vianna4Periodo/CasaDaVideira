using CasaDaVideira.Model.Database.Model;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class PesquisaRepository : RepositoryBase<Pesquisa>
    {
        public PesquisaRepository(ISession session) : base(session)
        {
        }

        public bool UsuarioJaRespondeu(Guid idUsuario)
        {
            var jarespondeu = this.Session.Query<Pesquisa>().FirstOrDefault(f => f.Usuario.Id == idUsuario);
            return jarespondeu != null ? true : false;
        }

        public Pesquisa FindByUserId(Guid id)
        {
            return this.Session.Query<Pesquisa>().FirstOrDefault(f => f.Usuario.Id == id);
        }
    }
}
