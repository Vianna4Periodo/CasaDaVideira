﻿using CasaDaVideira.Model.Database.Model;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class EnderecoRepository : RepositoryBase<Endereco>
    {
        public EnderecoRepository(ISession session) : base(session)
        {

        }
    }
}
