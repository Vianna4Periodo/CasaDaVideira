using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    using NHibernate.Mapping.ByCode;

    public class Carrinho : EntityBase
    {
        public virtual IList<Produto> Produtos { get; set; }
        public virtual Usuario Usuario { get; set; }


        public Carrinho() : base()
        {
            
        }
    }

    public class CarrinhoMap : ClassMapping<Carrinho>
    {
        public CarrinhoMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idCarrinho");
            });
            Property(x => x.Ativo, m => m.NotNullable(true));
            Property(m => m.CreatedAt);
            Property(m => m.UpdatedAt);
            Bag<Produto>(x => x.Produtos, 
                m =>{
                        m.Table("Carrinho_has_Produtos");
                        m.Cascade(Cascade.All);
                        m.Key(k => k.Column("idCarrinho"));
                        m.Lazy(CollectionLazy.NoLazy);
                        m.Inverse(true);
                    },
                r => r.ManyToMany(map => map.Column("idProduto"))
            );
            ManyToOne(x => x.Usuario, m =>
                {
                    m.Column("idUsuario");
                    m.Cascade(Cascade.All);
                });

            
        }
    }
}
