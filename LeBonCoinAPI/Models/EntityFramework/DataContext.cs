using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.Intrinsics.X86;

namespace LeBonCoinAPI.Models.EntityFramework
{
    public partial class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) { }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Adresse> Adresses { get; set; } = null!;
        public virtual DbSet<Annonce> Annonces { get; set; } = null!;
        public virtual DbSet<Commentaire> Commentaires { get; set; } = null!;
        public virtual DbSet<CarteBancaire> CarteBancaires { get; set; } = null!;
        public virtual DbSet<Departement> Departements { get; set; } = null!;
        public virtual DbSet<Entreprise> Entreprises { get; set; } = null!;
        public virtual DbSet<Equipement> Equipements { get; set; } = null!;
        public virtual DbSet<Favoris> lesFavoris { get; set; } = null!;
        public virtual DbSet<Particulier> Particuliers { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<PossedeEquipement> PossedeEquipements { get; set; } = null!;
        public virtual DbSet<Profil> Profils { get; set; } = null!;
        public virtual DbSet<Reglement> Reglements { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<SecteurActivite> SecteurActivites { get; set; } = null!;
        public virtual DbSet<Signale> Signales { get; set; } = null!;
        public virtual DbSet<TypeLogement> TypeLogements { get; set; } = null!;
        public virtual DbSet<TypeEquipement> TypeEquipements { get; set; } = null!;
        public virtual DbSet<Ville> Villes { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=FilmRatingsDB; uid=postgres; password=postgres;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Particulier>(entity =>
            {
                entity.HasKey(e => e.ProfilId)
                    .HasName("pk_prt");

                entity.HasIndex(e => e.ProfilId, "prt_pk")
                    .IsUnique();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.Property(e => e.ProfilId)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("pk_pho");

                entity.HasIndex(e => e.ProfilId, "fk_pho_prf");

                entity.HasIndex(e => e.AnnoncePhoto, "fk_pho_ann");

                entity.HasIndex(e => e.PhotoId, "pho_pk")
                    .IsUnique();

                entity.Property(e => e.PhotoId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.ProfilPhoto)
                    .WithMany(p => p.PhotosProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pho_prf");

                entity.HasOne(d => d.AnnoncePhoto)
                    .WithMany(p => p.PhotosAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pho_ann");

            });

            modelBuilder.Entity<PossedeEquipement>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.EquipementId })
                    .HasName("pk_peq");

                entity.HasIndex(e => e.EquipementId, "fk_peq_equ").IsUnique();

                entity.HasIndex(e => e.AnnonceId, "fk_peq_ann").IsUnique();

                entity.HasOne(d => d.AnnonceEquipementPossede)
                    .WithMany(p => p.EquipementsPossedesAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_peq_ann");

                entity.HasOne(d => d.EquipementPossede)
                    .WithMany(p => p.EquipementsPossedesDesEquipement)
                    .HasForeignKey(d => d.EquipementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_peq_equ");
            });

            modelBuilder.Entity<Profil>(entity =>
            {
                entity.HasKey(e => e.ProfilId)
                    .HasName("pk_prf");

                entity.HasIndex(e => e.AdresseId, "fk_prf_adr");

                entity.HasIndex(e => e.ProfilId, "prf_pk")
                    .IsUnique();

                entity.Property(e => e.ProfilId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.HasOne(d => d.AdresseProfil)
                    .WithMany(p => p.ProfilsAdresse)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_prf_adr");
            });

            modelBuilder.Entity<Reglement>(entity =>
            {
                entity.HasKey(e => e.ReglementId)
                    .HasName("pk_rgl");

                entity.HasIndex(e => e.ReservationId, "fk_rgl_res");

                entity.HasIndex(e => e.ReglementId, "reglements_pk")
                    .IsUnique();

                entity.Property(e => e.ReglementId)
                    .ValueGeneratedNever();
                
                entity.HasOne(d => d.ReservationReglement)
                    .WithMany(p => p.ReglementsReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_rgl_res");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReservationId)
                    .HasName("pk_res");

                entity.HasIndex(e => e.AnnonceId, "fk_res_ann");

                entity.HasIndex(e => e.ReservationId, "res_pk")
                    .IsUnique();

                entity.HasIndex(e => e.ProfilId, "res_fk");

                entity.Property(e => e.ReservationId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.HasOne(d => d.AnnonceReservation)
                    .WithMany(p => p.ReservationsAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_res_ann");
            });

            modelBuilder.Entity<SecteurActivite>(entity =>
            {
                entity.HasKey(e => e.SecteurId)
                    .HasName("pk_sct");

                entity.HasIndex(e => e.SecteurId, "sct_pk")
                    .IsUnique();

                entity.Property(e => e.SecteurId)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Signale>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.AnnonceId })
                    .HasName("pk_sig");

                entity.HasIndex(e => e.AnnonceId, "fk_sig_ann")
                .IsUnique();

                entity.HasIndex(e => e.ProfilId, "fk_sig_prf")
                .IsUnique();

                entity.HasOne(d => d.AnnonceSignalement)
                    .WithMany(p => p.SignalementsAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sig_ann");

                entity.HasOne(d => d.ProfilSignalement)
                    .WithMany(p => p.SignalementsProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_sig_prf");
            });

            modelBuilder.Entity<TypeLogement>(entity =>
            {
                entity.HasKey(e => e.TypeLogementId)
                    .HasName("pk_tyl");

                entity.HasIndex(e => e.TypeLogementId, "tyl_pk")
                    .IsUnique();

                entity.Property(e => e.TypeLogementId)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<TypeEquipement>(entity =>
            {
                entity.HasKey(e => e.TypeEquipementId)
                    .HasName("pk_tye");

                entity.HasIndex(e => e.TypeEquipementId, "tye_pk")
                    .IsUnique();

                entity.Property(e => e.TypeEquipementId)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.CodeInsee)
                    .HasName("pk_vil");

                entity.HasIndex(e => e.DepartementCode, "fk_vil_dep");

                entity.HasIndex(e => e.CodeInsee, "vil_pk")
                    .IsUnique();

                entity.Property(e => e.CodeInsee)
                    .IsFixedLength();

                entity.Property(e => e.DepartementCode)
                    .IsFixedLength();

                entity.Property(e => e.CodePostal)
                    .IsFixedLength();

                entity.HasOne(d => d.DepartementVille)
                    .WithMany(p => p.VillesDepartement)
                    .HasForeignKey(d => d.DepartementCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_vil_dep");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
