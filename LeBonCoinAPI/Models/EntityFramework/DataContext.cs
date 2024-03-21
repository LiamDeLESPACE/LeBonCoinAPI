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
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.ProfilId)
                    .HasName("pk_adm");                

                entity.HasIndex(e => e.ProfilId, "adm_pk")
                    .IsUnique();

                entity.Property(e => e.ProfilId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.AdresseId)
                    .HasName("pk_adr");                

                entity.HasIndex(e => e.AdresseId, "adr_pk")
                    .IsUnique();

                entity.HasIndex(e => e.CodeInsee, "fk_adr_vil");

                entity.Property(e => e.AdresseId)
                    .ValueGeneratedNever();                    

                entity.Property(e => e.CodeInsee)    
                    .IsFixedLength();

                entity.HasOne(d => d.VilleAdresse)
                    .WithMany(p => p.AdressesVille)
                    .HasForeignKey(d => d.CodeInsee)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_adr_vil");
            });

            modelBuilder.Entity<Annonce>(entity =>
            {
                entity.HasKey(e => e.AnnonceId)
                    .HasName("pk_ann");

                entity.HasIndex(e => e.AnnonceId, "ann_pk")
                    .IsUnique();

                entity.HasIndex(e => e.TypeLogementId, "fk_ann_tyl");

                entity.HasIndex(e => e.ProfilId, "fk_ann_prf");

                entity.HasIndex(e => e.AdresseId, "fk_ann_adr");

                entity.Property(e => e.AnnonceId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.AdresseAnnonce)
                    .WithMany(p => p.AnnoncesAdresse)
                    .HasForeignKey(d => d.AdresseId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ann_adr");

                entity.HasOne(d => d.TypeLogementAnnonce)
                    .WithMany(p => p.AnnoncesTypeLogement)
                    .HasForeignKey(d => d.TypeLogementId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ann_tyl");

                entity.HasOne(d => d.ProfilAnnonce)
                    .WithMany(p => p.AnnoncesProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_ann_prf");
            });

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.ReservationId })
                    .HasName("pk_cmt");

                entity.HasIndex(e => e.ReservationId, "fk_cmt_res")
                    .IsUnique();
                
                entity.HasIndex(e => e.ProfilId, "fk_cmt_prf")
                    .IsUnique();

                entity.HasOne(d => d.ReservationCommentaire)
                    .WithMany(p => p.CommentairesReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cmt_res");

                entity.HasOne(d => d.ProfilCommentaire)
                    .WithMany(p => p.CommentairesProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cmt_prf");
            });

            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.HasKey(e => e.CarteId)
                    .HasName("pk_cab");

                entity.HasIndex(e => e.CarteId, "cab_pk")
                    .IsUnique();

                entity.HasIndex(e => e.ProfilId, "fk_cab_prf");

                entity.Property(e => e.CarteId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Numero)
                    .IsFixedLength();

                entity.HasOne(d => d.ProfilCarteBancaire)
                    .WithMany(p => p.CartesBancairesProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cmt_prf");
            });

            modelBuilder.Entity<Departement>(entity =>
            {
                entity.HasKey(e => e.DepartementCode)
                    .HasName("pk_dep");

                entity.HasIndex(e => e.DepartementCode, "dep_pk")
                    .IsUnique();
            });

            modelBuilder.Entity<Entreprise>(entity =>
            {
                entity.HasKey(e => e.ProfilId)
                    .HasName("pk_ent");

                entity.HasIndex(e => e.ProfilId, "ent_pk")
                    .IsUnique();

                entity.HasIndex(e => e.SecteurId, "fk_ent_sct");

                entity.Property(e => e.ProfilId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Siret)
                    .IsFixedLength();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.HasOne(d => d.SecteurActiviteEntreprise)
                   .WithMany(p => p.EntreprisesSecteurActivite)
                   .HasForeignKey(d => d.SecteurId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("fk_ent_sct");
            });

            modelBuilder.Entity<Equipement>(entity =>
            {
                entity.HasKey(e => e.EquipementId)
                    .HasName("pk_equ");

                entity.HasIndex(e => e.EquipementId, "equ_pk")
                    .IsUnique();

                entity.HasIndex(e => e.TypeEquipementId, "fk_equ_tye");

                entity.Property(e => e.EquipementId)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.TypeEquipementEquipement)
                   .WithMany(p => p.EquipementsTypeEquipement)
                   .HasForeignKey(d => d.TypeEquipementId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("fk_equ_tye");
            });

            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.ProfilId })
                    .HasName("pk_fav");

                entity.HasIndex(e => e.ProfilId, "fk_fav_prf")
                    .IsUnique();

                entity.HasIndex(e => e.AnnonceId, "fk_fav_ann")
                    .IsUnique();

                entity.HasOne(d => d.ProfilFavoris)
                    .WithMany(p => p.FavorisProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_fav_prf");

                entity.HasOne(d => d.AnnonceFavoris)
                    .WithMany(p => p.FavorisAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_fav_ann");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
