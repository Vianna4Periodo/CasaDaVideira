using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Pedido: EntityBase
    {
        public virtual Carrinho Carrinho { get; set; }
        public virtual bool Entregue { get; set; }
        public virtual bool Pago { get; set; }
        public Pedido() : base(){
            this.Entregue = false;
            this.Pago = false;
        }
    }
    public class PedidoMap : ClassMapping<Pedido>
    {
        public PedidoMap()
        {
            //EntityBase
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idPedido");
            });
            Property(x => x.Ativo, m => m.NotNullable(true));
            Property(m => m.CreatedAt);
            Property(m => m.UpdatedAt);
            //Properties
            Property(x => x.Entregue);
            Property(x => x.Pago);
            ManyToOne(x => x.Carrinho, m =>
            {
                m.Column("idCarrinho");
            });

        }
    }
}
