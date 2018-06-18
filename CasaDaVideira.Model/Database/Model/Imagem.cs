using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace CasaDaVideira.Model.Database.Model
{
    public class Imagem : EntityBase
    {
        public virtual Produto Produto { get; set; }
        public virtual string Caminho { get; set; }
        public virtual DateTime DataInclusao { get; set; }

        public Imagem() : base()
        {

        }

        public Imagem(Produto produto)
        {
            this.Produto = produto;
            this.DataInclusao = DateTime.Now;
        }

        public Imagem(Produto produto, string caminho)
        {
            this.Produto = produto;
            this.Caminho = caminho;
            this.DataInclusao = DateTime.Now;
        }
    }

    public class ImagemMap : ClassMapping<Imagem>
    {
        public ImagemMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idImagem");
            });
            Property(x => x.Caminho);
            Property(x => x.DataInclusao);
            Property(x => x.Ativo, m => m.NotNullable(true));
            Property(m => m.CreatedAt);
            Property(m => m.UpdatedAt);
            ManyToOne(x => x.Produto, m =>
            {
                m.Cascade(Cascade.All);
                m.Column("idProduto");
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
