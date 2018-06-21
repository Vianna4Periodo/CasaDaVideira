using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasaDaVideira.Model.Database.Model;
using NHibernate;

namespace CasaDaVideira.Model.Database.Repository
{
   
    public class BuscaRealizadaRepository:RepositoryBase<BuscaRealizada>
    {
        public BuscaRealizadaRepository(ISession session)
            : base(session)
        {
        }
    }
}
