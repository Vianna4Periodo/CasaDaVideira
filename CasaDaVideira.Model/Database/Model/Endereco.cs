using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Endereco : EntityBase
    {
        public virtual string Rua { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual int Cep { get; set; }

        public virtual Usuario Usuario { get; set; }

        public Endereco() : base()
        {

        }
    }

    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idEndereco");
            });

            Property(x => x.Rua);
            Property(x => x.Numero);
            Property(x => x.Complemento);
            Property(x => x.Bairro);
            Property(x => x.Cidade);
            Property(x => x.Cep);
            Property(x => x.Ativo, m => m.NotNullable(true));
            ManyToOne(x => x.Usuario, m =>
            {
                m.Column("idUsuario");
            });
        }
    }
}
