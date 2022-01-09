using Dloc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dloc.Data.Context
{
    public partial class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public virtual DbSet<Ave> Ave { get; set; }
        public virtual DbSet<Casamento> Casamento { get; set; }
        public virtual DbSet<Filho> Filho { get; set; }
        public virtual DbSet<Mae> Mae { get; set; }
        public virtual DbSet<Mutacao> Mutacao { get; set; }
        public virtual DbSet<MutacaoAve> MutacaoAve { get; set; }
        public virtual DbSet<Pai> Pai { get; set; }
        public virtual DbSet<Portador> Portador { get; set; }
        public virtual DbSet<PortadorAve> PortadorAve { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=webContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ave>(entity =>
            {
                entity.Property(e => e.codigo)
                   .IsRequired()
                   .HasMaxLength(10)
                   .IsUnicode(false);

                entity.Property(e => e.nome)
                    .IsRequired(required: false)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.cor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.dt_nascimento).IsRequired(required: false).HasColumnType("date");
            });

            modelBuilder.Entity<Casamento>(entity =>
            {
                entity.HasOne(d => d.id_femeaNavigation)
                    .WithMany(p => p.Noivas)
                    .HasForeignKey(d => d.id_femea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Casamento__id_no__7D0E9093");

                entity.HasOne(d => d.id_machoNavigation)
                    .WithMany(p => p.Noivos)
                    .HasForeignKey(d => d.id_macho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Casamento__id_no__7C1A6C5A");
            });

            modelBuilder.Entity<Filho>(entity =>
            {
                entity.HasOne(d => d.id_filhoNavigation)
                    .WithMany(p => p.GetFilhos)
                    .HasForeignKey(d => d.id_filho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filho__id_filho__367C1819");

                entity.HasOne(d => d.id_maeNavigation)
                    .WithMany(p => p.Filhos)
                    .HasForeignKey(d => d.id_mae)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filho__id_mae__3587F3E0");

                entity.HasOne(d => d.id_paiNavigation)
                    .WithMany(p => p.ObterFilhos)
                    .HasForeignKey(d => d.id_pai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filho__id_pai__3493CFA7");
            });

            modelBuilder.Entity<Mae>(entity =>
            {
                entity.HasOne(d => d.id_aveNavigation)
                    .WithMany(p => p.GetMaes)
                    .HasForeignKey(d => d.id_ave)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mae__id_ave__59063A47");

                entity.HasOne(d => d.id_maeNavigation)
                    .WithMany(p => p.Maes)
                    .HasForeignKey(d => d.id_mae)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mae__id_mae__59FA5E80");
            });

            modelBuilder.Entity<Mutacao>(entity =>
            {
                entity.Property(e => e.ds_mutacao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MutacaoAve>(entity =>
            {
                entity.HasOne(d => d.id_aveNavigation)
                    .WithMany(p => p.MutacaoAve)
                    .HasForeignKey(d => d.id_ave)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MutacaoAv__id_av__3BCADD1B");

                entity.HasOne(d => d.id_mutacaoNavigation)
                    .WithMany(p => p.MutacaoAve)
                    .HasForeignKey(d => d.id_mutacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MutacaoAv__id_mu__3AD6B8E2");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasOne(d => d.id_aveNavigation)
                    .WithMany(p => p.GetPais)
                    .HasForeignKey(d => d.id_ave)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pai__id_ave__5535A963");

                entity.HasOne(d => d.id_paiNavigation)
                    .WithMany(p => p.Pais)
                    .HasForeignKey(d => d.id_pai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pai__id_pai__5629CD9C");
            });

            modelBuilder.Entity<Portador>(entity =>
            {
                entity.Property(e => e.ds_portador)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PortadorAve>(entity =>
            {
                entity.HasOne(d => d.id_aveNavigation)
                    .WithMany(p => p.PortadorAve)
                    .HasForeignKey(d => d.id_ave)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PortadorA__id_av__4183B671");

                entity.HasOne(d => d.id_portadorNavigation)
                    .WithMany(p => p.PortadorAve)
                    .HasForeignKey(d => d.id_portador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PortadorA__id_po__408F9238");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}