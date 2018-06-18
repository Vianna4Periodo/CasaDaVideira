using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace CasaDaVideira.Model.Database.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Produto : EntityBase
    {
        private double preco;
        public virtual string Nome { get; set; }
        public virtual string DescricaoResumida { get; set; }
        public virtual string DescricaoCompleta { get; set; }
        public virtual double Peso { get; set; }
        public virtual int Qtd { get; set; }
        public virtual double PrecoAntigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual bool Oferta { get; set; }
        public virtual int Classificacao { get; set; }
        public virtual IList<Imagem>  Imagens { get; set; }

        public virtual IList<Carrinho> Carrinhos { get; set; }
        [DataType(DataType.Currency)]
        public virtual double Preco
        {
            get
            {
                return preco;
            }
            set
            {
                if (this.preco != value)
                    this.PrecoAntigo = preco;
                preco = value;
            }
        }
        public Produto() : base()
        {
            preco = Preco;
            Imagens = new List<Imagem>();
        }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            //esta mapeando uma primarykey
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
                m.Column("idProduto");
            });

            Property(x => x.Nome);
            Property(x => x.DescricaoResumida);
            Property(x => x.DescricaoCompleta);
            Property(x => x.Peso);
            Property(x => x.Preco);
            Property(x => x.Qtd);
            Property(x => x.PrecoAntigo);
            Property(x => x.Oferta);
            Property(x => x.Classificacao);
            Property(x => x.Ativo, m => m.NotNullable(true));
            Bag<Imagem>(x => x.Imagens, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idProduto"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            ManyToOne(x => x.Categoria, m =>
            {
                m.Cascade(Cascade.All);
                m.Column("idCategoria");
                m.Lazy(LazyRelation.NoLazy);
            });

            Bag<Carrinho>(x => x.Carrinhos,
                m => {
                        m.Table("Carrinho_has_Produtos");
                        m.Cascade(Cascade.All);
                        m.Key(k => k.Column("idProduto"));
                        m.Lazy(CollectionLazy.NoLazy);
                        m.Inverse(true);
                    },
                r => r.ManyToMany(map => map.Column("idCarrinho"))
            );
        }
    }
}
