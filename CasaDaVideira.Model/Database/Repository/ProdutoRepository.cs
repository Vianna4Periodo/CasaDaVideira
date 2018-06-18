using CasaDaVideira.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace CasaDaVideira.Model.Database.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session)
        {

        }

        public override Produto Update(Produto entity)
        {
            return base.Update(entity);
        }
    }
}
