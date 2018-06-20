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

        public int CountProdutos()
        {            
            return this.Session.QueryOver<Produto>().RowCount();
        }

        public override Produto Update(Produto entity)
        {
            return base.Update(entity);
        }

        public IEnumerable<Produto> FindAllByCategoria(Guid idCategoria)
        {
            var produtos = this.Session.Query<Produto>().Where(x => x.Ativo == true && x.Categoria.Id == idCategoria).ToList<Produto>();
            return produtos;
        }

        public IEnumerable<Produto> FindByName(string name)
        {
            var produtos = this.Session.Query<Produto>().Where(f => f.Nome.ToLower().Contains(name.ToLower())).ToList<Produto>();
            return produtos;
        }
    }
}
