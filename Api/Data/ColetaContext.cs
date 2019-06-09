using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ColetaApi.Data
{
    public partial class ColetaContext : DbContext
    {
        public ColetaContext()
        {
        }

        public ColetaContext(DbContextOptions<ColetaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coleta> Coleta { get; set; }
        public virtual DbSet<OpcaoRespostaPergunta> OpcaoRespostaPergunta { get; set; }
        public virtual DbSet<Pergunta> Pergunta { get; set; }
        public virtual DbSet<Questionario> Questionario { get; set; }
        public virtual DbSet<QuestionarioPergunta> QuestionarioPergunta { get; set; }
        public virtual DbSet<RespostaColeta> RespostaColeta { get; set; }
        public virtual DbSet<TipoPergunta> TipoPergunta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=Perso.database.windows.net;initial catalog=Coleta;User=sistema;Password=s@pkdm98DFAOJF#13nf!cvb;MultipleActiveResultSets=true;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coleta>(entity =>
            {
                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.HasOne(d => d.IdQuestionarioNavigation)
                    .WithMany(p => p.Coleta)
                    .HasForeignKey(d => d.IdQuestionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coleta_Questionario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Coleta)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coleta_Usuario");
            });

            modelBuilder.Entity<OpcaoRespostaPergunta>(entity =>
            {
                entity.Property(e => e.Opcao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPerguntaNavigation)
                    .WithMany(p => p.OpcaoRespostaPergunta)
                    .HasForeignKey(d => d.IdPergunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OpcaoResp__IdPer__00200768");
            });

            modelBuilder.Entity<Pergunta>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoPerguntaNavigation)
                    .WithMany(p => p.Pergunta)
                    .HasForeignKey(d => d.IdTipoPergunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pergunta__IdTipo__778AC167");
            });

            modelBuilder.Entity<Questionario>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuestionarioPergunta>(entity =>
            {
                entity.HasKey(e => new { e.IdQuestionario, e.IdPergunta });

                entity.ToTable("Questionario_Pergunta");

                entity.HasOne(d => d.IdPerguntaNavigation)
                    .WithMany(p => p.QuestionarioPergunta)
                    .HasForeignKey(d => d.IdPergunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questiona__IdPer__7C4F7684");

                entity.HasOne(d => d.IdQuestionarioNavigation)
                    .WithMany(p => p.QuestionarioPergunta)
                    .HasForeignKey(d => d.IdQuestionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questiona__IdQue__7D439ABD");
            });

            modelBuilder.Entity<RespostaColeta>(entity =>
            {
                entity.HasKey(e => new { e.IdColeta, e.IdPergunta });

                entity.Property(e => e.Valor).HasMaxLength(500);

                entity.HasOne(d => d.IdColetaNavigation)
                    .WithMany(p => p.RespostaColeta)
                    .HasForeignKey(d => d.IdColeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resposta_Coleta");

                entity.HasOne(d => d.IdOpcaoRespostaNavigation)
                    .WithMany(p => p.RespostaColeta)
                    .HasForeignKey(d => d.IdOpcaoResposta)
                    .HasConstraintName("FK_Resposta_OpcaoRespostaPergunta");

                entity.HasOne(d => d.IdPerguntaNavigation)
                    .WithMany(p => p.RespostaColeta)
                    .HasForeignKey(d => d.IdPergunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resposta_Pergunta");
            });

            modelBuilder.Entity<TipoPergunta>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome).HasMaxLength(50);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
