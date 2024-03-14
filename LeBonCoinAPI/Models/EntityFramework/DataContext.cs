using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LeBonCoinAPI.Models.EntityFramework
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("DBContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Modèle builder
            
            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("action_act_pkey");

                entity.Property(e => e.Libelle).HasMaxLength(50);

                entity.HasMany(d => d.FormulairesChatbotAction)
                    .WithOne(p => p.ActionFormulaire)
                    .HasForeignKey(d => d.FormulaireChatbotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_act_for");
            });

            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.AdresseId)
                    .HasName("adresse_adr_pkey");

                entity.Property(e => e.CodeInsee).IsFixedLength();
                entity.Property(e => e.Rue).HasMaxLength(100);
                entity.Property(e => e.Numero);
                entity.Property(e => e.Pays).HasMaxLength(50);

                entity.HasOne(d => d.VilleAdresse)
                    .WithMany(p => p.AdressesVille)
                    .HasForeignKey(d => d.CodeInsee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_adr_vil");
            });

            modelBuilder.Entity<Annonce>(entity =>
            {
                entity.HasKey(e => e.AnnonceId)
                    .HasName("annonce_ann_pkey");

                entity.Property(e => e.ProprietaireId);
                entity.Property(e => e.CalendrierId);
                entity.Property(e => e.TypelogementId);
                entity.Property(e => e.CapacitevoyageurId);
                entity.Property(e => e.AdresseId);
                entity.Property(e => e.Titre).HasMaxLength(100);
                entity.Property(e => e.DureeMinimumSejour);
                entity.Property(e => e.EstActive);
                entity.Property(e => e.DatePublication);
                entity.Property(e => e.Description);
                entity.Property(e => e.Etoile);

                entity.HasOne(d => d.VilleAdresse)
                    .WithMany(p => p.AdressesVille)
                    .HasForeignKey(d => d.CodeInsee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_adr_vil");
            });

            /*
            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.HasKey(e => e.EtudiantId)
                    .HasName("t_e_etudiant_etu_pkey");

                entity.Property(e => e.Ine).IsFixedLength();

                entity.Property(e => e.DateInscription).HasDefaultValueSql("CURRENT_DATE");

                entity.HasOne(d => d.FormationEtudiant)
                    .WithMany(p => p.EtudiantsFormation)
                    .HasForeignKey(d => d.FormationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_etu_for");
            });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
