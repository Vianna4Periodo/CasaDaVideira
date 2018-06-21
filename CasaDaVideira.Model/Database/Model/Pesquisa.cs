
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Pesquisa : EntityBase
    {
        public virtual bool CostumaFazerComprasPelaInternet { get; set; }
        public virtual bool SePreocupaEmAdquirirAlimentosSemAgrotoxicos { get; set; }
        public virtual string Comentario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Pesquisa() : base(){}
    }
    
    public class PesquisaMap : ClassMapping<Pesquisa>{
        public PesquisaMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idPesquisa");
            });
            Property(x => x.Ativo, m => m.NotNullable(true));
            Property(m => m.CreatedAt);
            Property(m => m.UpdatedAt);

            Property(x => x.CostumaFazerComprasPelaInternet);
            Property(x => x.SePreocupaEmAdquirirAlimentosSemAgrotoxicos);
            Property(x => x.Comentario);

            ManyToOne(x => x.Usuario, x => { x.Column("idUsuario"); });
        }
    }
}
