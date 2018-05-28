using CasaDaVideira.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
