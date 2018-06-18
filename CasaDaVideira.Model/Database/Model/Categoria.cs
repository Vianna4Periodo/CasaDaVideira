using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace CasaDaVideira.Model.Database.Model
{
    public class Categoria : EntityBase
    {
        public virtual string Nome { get; set; }
        public virtual IList<Produto> Produtos { get; set; }

        public Categoria() : base()
        {
            
        }
    }

    public class CategoriaMap : ClassMapping<Categoria>
    {
        public CategoriaMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idCategoria");
            });

            Property(x => x.Nome);
            Property(x => x.Ativo, m => m.NotNullable(true));
            Bag<Produto>(x => x.Produtos, m =>
            {
                m.Cascade(Cascade.Persist);
                m.Key(k => k.Column("idCategoria"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());
        }
    }
}
