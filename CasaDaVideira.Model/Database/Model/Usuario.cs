using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CasaDaVideira.Model.Database.Model
{
    public class Usuario : EntityBase
    {
        [Required(ErrorMessage = "Email é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Senha { get; set; }
        [Required(ErrorMessage = "Nome é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Sobrenome { get; set; }
        [Required(ErrorMessage = "CPF é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Cpf { get; set; }
        public virtual int Pontos { get; set; }
        public virtual DateTime DtNascimento { get; set; }
        public virtual DateTime UltimoAcesso { get; set; }
        public virtual IList<Telefone> Telefones { get; set; }
        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual bool Admin { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual Pesquisa Pesquisa { get; set; }

        public virtual IList<BuscaRealizada> BuscasRealizadas { get; set; }

        public Usuario() : base()
        {
            this.Telefones = new List<Telefone>();
            this.Enderecos = new List<Endereco>();
            this.BuscasRealizadas = new List<BuscaRealizada>();
        }

        public virtual bool respondeuPesquisa()
        {
            var respondeu = DbConfig.Instance.PesquisaRepository.UsuarioJaRespondeu(this.Id);
            return respondeu;
        }
        public class UsuarioMap : ClassMapping<Usuario>
        {
            public UsuarioMap()
            {
                //esta mapeando uma primarykey
                Id(x => x.Id, m =>
                {
                    m.Generator(Generators.Guid);
                    m.Column("idUsuario");
                });

                Property(x => x.Email, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Senha, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Nome, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Sobrenome, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Cpf, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Pontos);
                Property(x => x.Admin, m =>
                {
                    m.Type(NHibernateUtil.Boolean);
                    m.NotNullable(true);
                });
                Property(x => x.DtNascimento, m =>
                {
                    m.Type(NHibernateUtil.Date);
                    m.NotNullable(false);
                });
                Property(x => x.UltimoAcesso, m =>
                {
                    m.Type(NHibernateUtil.Date);
                    m.NotNullable(false);
                });
                Property(x => x.Ativo, m => m.NotNullable(true));
                Property(m => m.CreatedAt);
                Property(m => m.UpdatedAt);
                Bag<Telefone>(x => x.Telefones, m =>
                {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("idUsuario"));
                    m.Lazy(CollectionLazy.NoLazy);
                    m.Inverse(true);
                },
                r => r.OneToMany());
                Bag<BuscaRealizada>(x => x.BuscasRealizadas, m =>
                        {
                            m.Cascade(Cascade.All);
                            m.Key(k => k.Column("idUsuario"));
                            m.Lazy(CollectionLazy.NoLazy);
                            m.Inverse(true);
                        },r => r.OneToMany());
            }
        }
    }
}