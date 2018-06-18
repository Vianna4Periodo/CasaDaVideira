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
        public virtual DateTime UpdatedAt { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public EntityBase()
        {
            this.CreatedAt = DateTime.Now;
            this.Ativo = true;
        }

        public virtual bool isAtivo()
        {
            return this.Ativo;
        }
    }
}
