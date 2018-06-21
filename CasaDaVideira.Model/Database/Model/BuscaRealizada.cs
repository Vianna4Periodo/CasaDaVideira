using System;

namespace CasaDaVideira.Model.Database.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class BuscaRealizada : EntityBase
    {
        public virtual String Busca  { get; set; }

        public virtual Usuario Usuario  { get; set; }

        public BuscaRealizada() : base() { }
    }

    public class BuscaRealizadaMap : ClassMapping<BuscaRealizada>
    {
        public BuscaRealizadaMap()
        {
            Id(x => x.Id, m =>
                {
                    m.Generator(Generators.Guid);
                    m.Column("idCarrinho");
                });
            Property(x => x.Ativo, m => m.NotNullable(true));
            Property(m => m.CreatedAt);
            Property(m => m.UpdatedAt);

            Property(x => x.Busca);
            ManyToOne(x => x.Usuario, m =>
                {
                    m.Cascade(Cascade.All);
                    m.Column("idUsuario");
                    m.Lazy(LazyRelation.NoLazy);
                });
        }
    }
}
