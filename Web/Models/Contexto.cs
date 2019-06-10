namespace Coleta.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=ColetaAPI")
        {
        }

        public virtual DbSet<Coleta> Coletas { get; set; }
        public virtual DbSet<Coletor> Coletores { get; set; }
        public virtual DbSet<OpcaoRespostaPergunta> OpcaoRespostaPerguntas { get; set; }
        public virtual DbSet<Pergunta> Perguntas { get; set; }
        public virtual DbSet<RespostaColeta> RespostaColetas { get; set; }
        public virtual DbSet<TipoPergunta> TipoPerguntas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coletor>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Coletor>()
                .HasMany(e => e.Coletas)
                .WithRequired(e => e.Coletor)
                .HasForeignKey(e => e.idColetor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coletor>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Coletor)
                .HasForeignKey(e => e.idColetor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OpcaoRespostaPergunta>()
                .Property(e => e.opcao)
                .IsUnicode(false);

            modelBuilder.Entity<OpcaoRespostaPergunta>()
                .HasMany(e => e.RespostaColetas)
                .WithOptional(e => e.OpcaoRespostaPergunta)
                .HasForeignKey(e => e.idOpcaoResposta);

            modelBuilder.Entity<Pergunta>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Pergunta>()
                .HasMany(e => e.OpcaoRespostaPerguntas)
                .WithRequired(e => e.Pergunta)
                .HasForeignKey(e => e.idPergunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pergunta>()
                .HasMany(e => e.RespostaColetas)
                .WithRequired(e => e.Pergunta)
                .HasForeignKey(e => e.idPergunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPergunta>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPergunta>()
                .HasMany(e => e.Perguntas)
                .WithRequired(e => e.TipoPergunta)
                .HasForeignKey(e => e.IdTipoPergunta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.senha)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<ColetaApi.Models.QuestionarioDto> QuestionarioDtoes { get; set; }
    }
}
