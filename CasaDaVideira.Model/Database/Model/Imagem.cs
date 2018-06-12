using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace CasaDaVideira.Model.Database.Model
{
    public class Imagem
    {
        public virtual Guid IdImagem { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual string Caminho { get; set; }
        public virtual DateTime DataInclusao { get; set; }

        public Imagem()
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
            Id(x => x.IdImagem, m => m.Generator(Generators.Guid));
            Property(x => x.Caminho);
            Property(x => x.DataInclusao);
            ManyToOne(x => x.Produto, m =>
            {
                m.Cascade(Cascade.All);
                m.Column("IdProduto");
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
