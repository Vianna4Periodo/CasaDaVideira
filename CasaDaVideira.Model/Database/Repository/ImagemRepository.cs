using CasaDaVideira.Model.Database.Model;
using NHibernate;

namespace CasaDaVideira.Model.Database.Repository
{
    public class ImagemRepository : RepositoryBase<Imagem>
    {
        public ImagemRepository(ISession session) : base(session)
        {
        }
    }
}
