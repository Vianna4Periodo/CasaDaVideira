using CasaDaVideira.Model.Database.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class CarrinhoRepository : RepositoryBase<Carrinho>
    {
        public CarrinhoRepository(ISession session) : base(session)
        {
        }
    }
}
