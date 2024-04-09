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

        public virtual DbSet<Admin> Admins { get; set; }  
        public virtual DbSet<Adresse> Adresses { get; set; }  
        public virtual DbSet<Annonce> Annonces { get; set; }  
        public virtual DbSet<Commentaire> Commentaires { get; set; }  
        public virtual DbSet<CarteBancaire> CarteBancaires { get; set; }  
        public virtual DbSet<Departement> Departements { get; set; }  
        public virtual DbSet<Entreprise> Entreprises { get; set; }  
        public virtual DbSet<Equipement> Equipements { get; set; }  
        public virtual DbSet<Favoris> lesFavoris { get; set; }  
        public virtual DbSet<Particulier> Particuliers { get; set; }  
        public virtual DbSet<Photo> Photos { get; set; }  
        public virtual DbSet<PossedeEquipement> PossedeEquipements { get; set; }  
        public virtual DbSet<Profil> Profils { get; set; }  
        public virtual DbSet<Reglement> Reglements { get; set; }  
        public virtual DbSet<Reservation> Reservations { get; set; }  
        public virtual DbSet<SecteurActivite> SecteurActivites { get; set; }  
        public virtual DbSet<Signale> Signales { get; set; }  
        public virtual DbSet<TypeLogement> TypeLogements { get; set; }  
        public virtual DbSet<TypeEquipement> TypeEquipements { get; set; }  
        public virtual DbSet<Ville> Villes { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; port=5432; Database=sae; uid=postgres; password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            modelBuilder.Entity<Admin>().ToTable(t => t.HasCheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'"));

            modelBuilder.Entity<Adresse>();

            modelBuilder.Entity<Annonce>()
            .ToTable(t => t.HasCheckConstraint("ck_ann_etoile", "ann_etoile > 0 AND ann_etoile <= 5"))
            .ToTable(t => t.HasCheckConstraint("ck_ann_prixparnuit", "ann_prixparnuit > 0"))
            .ToTable(t => t.HasCheckConstraint("ck_ann_nombrechambres", "ann_nombrechambres > 0"))
            .ToTable(t => t.HasCheckConstraint("ck_ann_dureeminimumsejour", "ann_dureeminimumsejour > 0"))
            .ToTable(t => t.HasCheckConstraint("ck_ann_nombrepersonnesmax", "ann_nombrepersonnesmax > 0"));

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.ReservationId });
            });

            modelBuilder.Entity<CarteBancaire>();

            modelBuilder.Entity<Departement>();

            modelBuilder.Entity<Entreprise>();

            modelBuilder.Entity<Equipement>();

            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.ProfilId });
            });

            modelBuilder.Entity<Particulier>()
                .ToTable(t=> t.HasCheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'"))
                .ToTable(t => t.HasCheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')"));


            modelBuilder.Entity<Photo>();

            modelBuilder.Entity<PossedeEquipement>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.EquipementId });
            });

            modelBuilder.Entity<Profil>().UseTpcMappingStrategy();

            modelBuilder.Entity<Reglement>();

            modelBuilder.Entity<Reservation>()
                .ToTable(t => t.HasCheckConstraint("ck_res_nombrevoyageur", "res_nombrevoyageur > 0"))
                .ToTable(t => t.HasCheckConstraint("ck_res_telephone", "res_telephone LIKE ('06%') or res_telephone LIKE ('07%')OR res_telephone IS NULL"));

            modelBuilder.Entity<SecteurActivite>();

            modelBuilder.Entity<Signale>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.AnnonceId });
            });

            modelBuilder.Entity<TypeLogement>();

            modelBuilder.Entity<TypeEquipement>();

            modelBuilder.Entity<Ville>();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
