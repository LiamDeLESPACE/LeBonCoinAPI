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

                entity.HasMany(d => d.FormulairesChatbot)
                    .WithOne(p => p.ActionFormulaire)
                    .HasForeignKey(d => d.FormulaireChatbotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_act_for");
            });

            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.AdresseId)
                    .HasName("adresse_adr_pkey");

                entity.HasKey(e => e.CodeInsee)
                 .HasName("codeinsee_adr_pkey");

                entity.Property(e => e.CodeInsee).IsFixedLength();
                entity.Property(e => e.Rue).HasMaxLength(100);
                entity.Property(e => e.Numero);
                entity.Property(e => e.Pays).HasMaxLength(50);

                entity.HasMany(d => d.Adresses)
                    .WithOne(p => p.Adr)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_adr_adresses");
            });

            modelBuilder.Entity<Annonce>(entity =>
            {
                entity.HasKey(e => e.AnnonceId)
                    .HasName("annonce_ann_pkey");

                entity.Property(e => e.Etoile)
                .HasCheckConstraint("chk_ann_etoile", @"^[0-5]{1}$");

                entity.HasMany(d => d.Fav)
                   .WithOne(p => p.AnnonceFavoris)
                   .HasForeignKey(d => d.ProfilId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_ann_annoncefav")

                entity.HasMany(d => d.AvisAnnonce)
                   .WithOne(p => p.AnnonceAvi)
                   .HasForeignKey(d => d.AnnonceId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_ann_annonceavis")

                entity.HasMany(d => d.ContientsAnnonce)
                   .WithOne(p => p.AnnonceContient)
                   .HasForeignKey(d => d.AnnonceId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_ann_contientannonce")

                    entity.HasMany(d => d.ReservationsAnnonce)
                   .WithOne(p => p.AnnonceReservation)
                   .HasForeignKey(d => d.ReservationId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_ann_reservationsannonce")

                 entity.HasMany(d => d.PhotosAnnonce)
                   .WithOne(p => p.PhotoAnnonce)
                   .HasForeignKey(d => d.PhotoId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_ann_photosannonce")


            });

            modelBuilder.Entity<AnnonceAvis>(entity =>
            {
                entity.HasKey(e => e.AvisId)
                    .HasName("avis_anv_pkey");

                entity.HasKey(e => e.AnnonceId)
                .HasName("annonce_anv_pkey");


              
            });

            modelBuilder.Entity<Avis>(entity =>
            {
                entity.HasKey(e => e.AvisId)
                    .HasName("avsi_avi_pkey");

                entity.HasKey(e => e.ProfilId)
                   .HasName("profil_avi_pkey");

                entity.Property(e => e.Titre).HasMaxLength(100);

                entity.Property(e => e.Note)
               .HasCheckConstraint("chk_avi_note", @"^[1-5]{1}$");

                entity.HasMany(d => d.AvisTypeProfilAvis)
                    .WithOne(p => p.AviAvisProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avi_avistypeprofilavis");

                entity.HasMany(d => d.AvisTypeAnnonceAvis)
                   .WithOne(p => p.AviAvisAnnonce)
                   .HasForeignKey(d => d.AnnonceId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_avi_avistypeannonceavis");
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
