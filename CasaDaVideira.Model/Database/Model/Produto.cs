using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace CasaDaVideira.Model.Database.Model
{
    public class Produto
    {
        public virtual Guid IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual string DescricaoResumida { get; set; }
        public virtual string DescricaoCompleta { get; set; }
        //public virtual double Preco
        //{
        //    get
        //    {
        //        return this.Preco;
        //    }
        //    set
        //    {
        //        this.PrecoAntigo = this.Preco;
        //        this.Preco = value;
        //    }
        //}
        public virtual double Peso { get; set; }
        public virtual double Preco { get; set; }
        public virtual int Qtd { get; set; }
        public virtual double PrecoAntigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual bool Oferta { get; set; }
        public virtual int Classificacao { get; set; }
        public virtual IList<Imagem>  Imagens { get; set; }

        public Produto()
        {
            Imagens = new List<Imagem>();
        }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            //esta mapeando uma primarykey
            Id(x => x.IdProduto, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.DescricaoResumida);
            Property(x => x.DescricaoCompleta);
            Property(x => x.Peso);
            Property(x => x.Preco);
            Property(x => x.Qtd);
            Property(x => x.PrecoAntigo);
            Property(x => x.Oferta);
            Property(x => x.Classificacao);

            Bag<Imagem>(x => x.Imagens, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("IdProduto"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            ManyToOne(x => x.Categoria, m =>
            {
                m.Cascade(Cascade.All);
                m.Column("IdCategoria");
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
