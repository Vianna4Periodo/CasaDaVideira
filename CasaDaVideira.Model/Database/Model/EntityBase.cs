using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public abstract class EntityBase 
    {
        public virtual Guid Id { get; set; }
        public virtual bool Ativo  { get; set; }

        public EntityBase()
        {
            this.Ativo = true;
        }

        public virtual bool isAtivo()
        {
            return this.Ativo;
        }
    }
}
