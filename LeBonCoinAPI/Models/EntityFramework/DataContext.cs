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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; port=5432; Database=sae; uid=postgres; password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<Admin>(entity =>
            {

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.Property(b => b.ProfilId)
                .HasColumnName("prf_id");

                entity.Property(b => b.AdresseId)
                .HasColumnName("adr_id");

                entity.Property(b => b.HashMotDePasse)
                .HasColumnName("prf_hashmdp");

                entity.Property(b => b.Telephone)
                .HasColumnName("prf_telephone");

                //entity.HasCheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'");
            });*/

            modelBuilder.Entity<Admin>().ToTable(t => t.HasCheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'"));

            modelBuilder.Entity<Adresse>(/*entity =>
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

                entity.HasCheckConstraint("ck_adr_numero", "adr_numero between 0 and 1000");
            }*/);

            modelBuilder.Entity<Annonce>(/*entity =>
            {
                entity.HasKey(e => e.AnnonceId)
                    .HasName("pk_ann");

                entity.HasIndex(e => e.AnnonceId, "ann_pk")
                    .IsUnique();

                entity.HasIndex(e => e.TypeLogementId, "fk_ann_tyl");

                entity.HasIndex(e => e.ProfilId, "fk_ann_prf");

                entity.HasIndex(e => e.AdresseId, "fk_ann_adr");


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

                entity.HasCheckConstraint("ck_ann_active", "ann_active in ('FALSE', 'TRUE')");
                entity.HasCheckConstraint("ck_ann_etoile", "ann_etoile > 0 AND ann_etoile <= 5");
                entity.HasCheckConstraint("ck_ann_prixparnuit", "ann_prixparnuit > 0");
                entity.HasCheckConstraint("ck_ann_nombrechambres", "ann_nombrechambres > 0");

                entity.HasCheckConstraint("ck_ann_dureeminimumsejour", "ann_dureeminimumsejour > 0");
                entity.HasCheckConstraint("ck_ann_nombrepersonnesmax", "ann_nombrepersonnesmax > 0");
                entity.Property(e => e.DatePublication).HasDefaultValueSql("now()");
            }*/);

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.ReservationId });/*
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
                    .HasConstraintName("fk_cmt_prf");*/
            });

            modelBuilder.Entity<CarteBancaire>(/*entity =>
            {
                entity.HasKey(e => e.CarteId)
                    .HasName("pk_cab");

                entity.HasIndex(e => e.CarteId, "cab_pk")
                    .IsUnique();

                entity.HasIndex(e => e.ProfilId, "fk_cab_prf");

                entity.Property(e => e.Numero)
                    .IsFixedLength();

                entity.HasOne(d => d.ProfilCarteBancaire)
                    .WithMany(p => p.CartesBancairesProfil)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_cmt_prf");
            }*/);

            modelBuilder.Entity<Departement>(/*entity =>
            {
                entity.HasKey(e => e.DepartementCode)
                    .HasName("pk_dep");

                entity.HasIndex(e => e.DepartementCode, "dep_pk")
                    .IsUnique();

                entity.Property(e => e.DepartementCode)
                    .IsFixedLength();
            }*/);

            /*modelBuilder.Entity<Entreprise>(entity =>
            {
         
                entity.HasIndex(e => e.SecteurId, "fk_ent_sct");

                entity.Property(e => e.Siret)
                    .IsFixedLength();

                entity.Property(e => e.Telephone)
                    .IsFixedLength();
                entity.Property(b => b.ProfilId)
                   .HasColumnName("prf_id");

                entity.Property(b => b.AdresseId)
                    .HasColumnName("adr_id");

                entity.Property(b => b.HashMotDePasse)
                    .HasColumnName("prf_hashmdp");

                entity.Property(b => b.Telephone)
                    .HasColumnName("prf_telephone");

                entity.HasOne(d => d.SecteurActiviteEntreprise)
                   .WithMany(p => p.EntreprisesSecteurActivite)
                   .HasForeignKey(d => d.SecteurId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("fk_ent_sct");
            });*/

            modelBuilder.Entity<Entreprise>();

            modelBuilder.Entity<Equipement>(/*entity =>
            {
                entity.HasKey(e => e.EquipementId)
                    .HasName("pk_equ");

                entity.HasIndex(e => e.EquipementId, "equ_pk")
                    .IsUnique();

                entity.HasIndex(e => e.TypeEquipementId, "fk_equ_tye");

                entity.HasOne(d => d.TypeEquipementEquipement)
                   .WithMany(p => p.EquipementsTypeEquipement)
                   .HasForeignKey(d => d.TypeEquipementId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("fk_equ_tye");
            }*/);

            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.ProfilId });/*
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
                    .HasConstraintName("fk_fav_ann");*/
            });

            /*modelBuilder.Entity<Particulier>(entity =>
            {

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(b=>b.ProfilId)
                    .HasColumnName("prf_id");

                entity.Property(b => b.AdresseId)
                    .HasColumnName("adr_id");

                entity.Property(b => b.HashMotDePasse)
                    .HasColumnName("prf_hashmdp");

                entity.Property(b => b.Telephone)
                .HasColumnName("prf_telephone");
                entity.HasCheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'");
                entity.HasCheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')");

            });*/

            modelBuilder.Entity<Particulier>();


            modelBuilder.Entity<Photo>(/*entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("pk_pho");

                entity.HasIndex(e => e.ProfilId, "fk_pho_prf");

                entity.HasIndex(e => e.AnnonceId, "fk_pho_ann");

                entity.HasIndex(e => e.PhotoId, "pho_pk")
                    .IsUnique();

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

            }*/);

            modelBuilder.Entity<PossedeEquipement>(entity =>
            {
                entity.HasKey(e => new { e.AnnonceId, e.EquipementId });/*
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
                    .HasConstraintName("fk_peq_equ");*/
            });


            //modelBuilder.Entity<Profil>(entity =>
            //{

            //    entity.HasKey(e => e.ProfilId)
            //        .HasName("pk_prf");

            //    entity.HasIndex(e => e.AdresseId, "fk_prf_adr");

            //    entity.HasIndex(e => e.ProfilId, "prf_pk")
            //        .IsUnique();

            //    entity.Property(e => e.Telephone)
            //        .IsFixedLength();

            //    entity.Property(b => b.ProfilId)
            //        .HasColumnName("prf_id");

            //    entity.Property(b => b.AdresseId)
            //        .HasColumnName("adr_id");

            //    entity.Property(b => b.HashMotDePasse)
            //        .HasColumnName("prf_hashmdp");

            //    entity.Property(b => b.Telephone)
            //        .HasColumnName("prf_telephone");

            //    entity.Property("Discriminator");

            //    entity.HasOne(d => d.AdresseProfil)
            //        .WithMany(p => p.ProfilsAdresse)
            //        .HasForeignKey(d => d.AdresseId)
            //        .OnDelete(DeleteBehavior.Restrict)
            //        .HasConstraintName("fk_prf_adr");

            //    entity.HasCheckConstraint("ck_prf_telephone", "prf_telephone LIKE ('06%') or prf_telephone LIKE ('07%')OR prf_telephone IS NULL");

            //});

            modelBuilder.Entity<Profil>().UseTpcMappingStrategy();


            modelBuilder.Entity<Reglement>(/*entity =>
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
            }*/);

            modelBuilder.Entity<Reservation>(/*entity =>
            {
                entity.HasKey(e => e.ReservationId)
                    .HasName("pk_res");

                entity.HasIndex(e => e.AnnonceId, "fk_res_ann");

                entity.HasIndex(e => e.ReservationId, "res_pk")
                    .IsUnique();

                entity.HasIndex(e => e.ProfilId, "res_fk");

                entity.Property(e => e.Telephone)
                    .IsFixedLength();

                entity.HasOne(d => d.AnnonceReservation)
                    .WithMany(p => p.ReservationsAnnonce)
                    .HasForeignKey(d => d.AnnonceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_res_ann");

                entity.HasCheckConstraint("ck_res_nombrevoyageur", "res_nombrevoyageur > 0");
                entity.Property(e => e.DateArrivee).HasDefaultValueSql("now()");
                entity.Property(e => e.DateDepart).HasDefaultValueSql("now()+1");

                entity.HasCheckConstraint("ck_res_telephone", "res_telephone LIKE ('06%') or res_telephone LIKE ('07%')OR res_telephone IS NULL");

            }*/);

            modelBuilder.Entity<SecteurActivite>(/*entity =>
            {
                entity.HasKey(e => e.SecteurId)
                    .HasName("pk_sct");

                entity.HasIndex(e => e.SecteurId, "sct_pk")
                    .IsUnique();

            }*/);

            modelBuilder.Entity<Signale>(entity =>
            {
                entity.HasKey(e => new { e.ProfilId, e.AnnonceId });/*
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
                    .HasConstraintName("fk_sig_prf");*/
            });

            modelBuilder.Entity<TypeLogement>(/*entity =>
            {
                entity.HasKey(e => e.TypeLogementId)
                    .HasName("pk_tyl");

                entity.HasIndex(e => e.TypeLogementId, "tyl_pk")
                    .IsUnique();

            }*/);

            modelBuilder.Entity<TypeEquipement>(/*entity =>
            {
                entity.HasKey(e => e.TypeEquipementId)
                    .HasName("pk_tye");

                entity.HasIndex(e => e.TypeEquipementId, "tye_pk")
                    .IsUnique();


                entity.HasCheckConstraint("ck_tye_nom", "tye_nom in ('Equipements', 'Extérieur', 'Services et Accessibilité')");

            }*/);

            modelBuilder.Entity<Ville>(/*entity =>
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
            }*/);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
