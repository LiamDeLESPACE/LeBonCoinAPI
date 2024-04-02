﻿// <auto-generated />
using System;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240402084129_DbRemote")]
    partial class DbRemote
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("ProfilSequence");

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Adresse", b =>
                {
                    b.Property<int>("AdresseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("adr_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdresseId"));

                    b.Property<string>("CodeInsee")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("vil_codeinsee");

                    b.Property<int>("Numero")
                        .HasColumnType("integer")
                        .HasColumnName("adr_numero");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("adr_rue");

                    b.HasKey("AdresseId");

                    b.HasIndex("CodeInsee");

                    b.ToTable("t_e_adresse_adr");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Annonce", b =>
                {
                    b.Property<int>("AnnonceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnnonceId"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("ann_active");

                    b.Property<int>("AdresseId")
                        .HasColumnType("integer")
                        .HasColumnName("adr_id");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("ann_datepublication");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("ann_description");

                    b.Property<int>("DureeMinimumSejour")
                        .HasColumnType("integer")
                        .HasColumnName("ann_dureeminimumsejour");

                    b.Property<int>("Etoile")
                        .HasColumnType("integer")
                        .HasColumnName("ann_etoile");

                    b.Property<int>("NombreChambres")
                        .HasColumnType("integer")
                        .HasColumnName("ann_nombrechambres");

                    b.Property<int>("NombrePersonnesMax")
                        .HasColumnType("integer")
                        .HasColumnName("ann_nombrepersonnesmax");

                    b.Property<double>("PrixParNuit")
                        .HasColumnType("double precision")
                        .HasColumnName("ann_prixparnuit");

                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("ann_titre");

                    b.Property<int>("TypeLogementId")
                        .HasColumnType("integer")
                        .HasColumnName("tyl_id");

                    b.HasKey("AnnonceId");

                    b.HasIndex("AdresseId");

                    b.HasIndex("ProfilId");

                    b.HasIndex("TypeLogementId");

                    b.ToTable("t_e_annonce_ann", t =>
                        {
                            t.HasCheckConstraint("ck_ann_dureeminimumsejour", "ann_dureeminimumsejour > 0");

                            t.HasCheckConstraint("ck_ann_etoile", "ann_etoile > 0 AND ann_etoile <= 5");

                            t.HasCheckConstraint("ck_ann_nombrechambres", "ann_nombrechambres > 0");

                            t.HasCheckConstraint("ck_ann_nombrepersonnesmax", "ann_nombrepersonnesmax > 0");

                            t.HasCheckConstraint("ck_ann_prixparnuit", "ann_prixparnuit > 0");
                        });
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.CarteBancaire", b =>
                {
                    b.Property<int>("CarteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("cab_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarteId"));

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("cab_numero");

                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.HasKey("CarteId");

                    b.HasIndex("ProfilId");

                    b.ToTable("t_e_cartebancaire_cab");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Commentaire", b =>
                {
                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer")
                        .HasColumnName("res_id");

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cmt_contenu");

                    b.HasKey("ProfilId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("t_j_commentaire_cmt");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Departement", b =>
                {
                    b.Property<string>("DepartementCode")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("dep_code");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("dep_nom");

                    b.HasKey("DepartementCode");

                    b.ToTable("t_e_departement_dep");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Equipement", b =>
                {
                    b.Property<int>("EquipementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("equ_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EquipementId"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("equ_nom");

                    b.Property<int>("TypeEquipementId")
                        .HasColumnType("integer")
                        .HasColumnName("tye_id");

                    b.HasKey("EquipementId");

                    b.HasIndex("TypeEquipementId");

                    b.ToTable("t_e_equipement_equ");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Favoris", b =>
                {
                    b.Property<int>("AnnonceId")
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.HasKey("AnnonceId", "ProfilId");

                    b.HasIndex("ProfilId");

                    b.ToTable("t_j_favoris_fav");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pho_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhotoId"));

                    b.Property<int?>("AnnonceId")
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    b.Property<int?>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pho_url");

                    b.HasKey("PhotoId");

                    b.HasIndex("AnnonceId");

                    b.HasIndex("ProfilId");

                    b.ToTable("t_e_photo_pho");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.PossedeEquipement", b =>
                {
                    b.Property<int>("AnnonceId")
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    b.Property<int>("EquipementId")
                        .HasColumnType("integer")
                        .HasColumnName("equ_id");

                    b.HasKey("AnnonceId", "EquipementId");

                    b.HasIndex("EquipementId");

                    b.ToTable("t_j_possedeequipement_peq");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Profil", b =>
                {
                    b.Property<int>("ProfilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("prf_id")
                        .HasDefaultValueSql("nextval('\"ProfilSequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<int>("ProfilId"));

                    b.Property<string>("HashMotDePasse")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("prf_hashmdp");

                    b.Property<string>("Telephone")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("prf_telephone");

                    b.HasKey("ProfilId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Reglement", b =>
                {
                    b.Property<string>("ReglementId")
                        .HasColumnType("text")
                        .HasColumnName("rgl_id");

                    b.Property<int>("ReservationId")
                        .HasColumnType("integer")
                        .HasColumnName("res_id");

                    b.HasKey("ReglementId");

                    b.HasIndex("ReservationId");

                    b.ToTable("t_e_reglement_rgl");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("res_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("AnnonceId")
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    b.Property<DateTime>("DateArrivee")
                        .HasColumnType("date")
                        .HasColumnName("res_datearrivee");

                    b.Property<DateTime>("DateDepart")
                        .HasColumnType("date")
                        .HasColumnName("res_datedepart");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("res_nom");

                    b.Property<int>("NombreVoyageur")
                        .HasColumnType("integer")
                        .HasColumnName("res_nombrevoyageur");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("res_prenom");

                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("res_telephone");

                    b.HasKey("ReservationId");

                    b.HasIndex("AnnonceId");

                    b.HasIndex("ProfilId");

                    b.ToTable("t_e_reservation_res", t =>
                        {
                            t.HasCheckConstraint("ck_res_nombrevoyageur", "res_nombrevoyageur > 0");

                            t.HasCheckConstraint("ck_res_telephone", "res_telephone LIKE ('06%') or res_telephone LIKE ('07%')OR res_telephone IS NULL");
                        });
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.SecteurActivite", b =>
                {
                    b.Property<int>("SecteurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("sct_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SecteurId"));

                    b.Property<string>("NomSecteur")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("sct_nomsecteur");

                    b.HasKey("SecteurId");

                    b.ToTable("t_e_secteuractivite_sct");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Signale", b =>
                {
                    b.Property<int>("ProfilId")
                        .HasColumnType("integer")
                        .HasColumnName("prf_id");

                    b.Property<int>("AnnonceId")
                        .HasColumnType("integer")
                        .HasColumnName("ann_id");

                    b.HasKey("ProfilId", "AnnonceId");

                    b.HasIndex("AnnonceId");

                    b.ToTable("t_j_signale_sig");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.TypeEquipement", b =>
                {
                    b.Property<int>("TypeEquipementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tye_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TypeEquipementId"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("tye_nom");

                    b.HasKey("TypeEquipementId");

                    b.ToTable("t_e_typeequipement_tye");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.TypeLogement", b =>
                {
                    b.Property<int>("TypeLogementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tyl_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TypeLogementId"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("tyl_nom");

                    b.HasKey("TypeLogementId");

                    b.ToTable("t_e_typelogement_tyl");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Ville", b =>
                {
                    b.Property<string>("CodeInsee")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("vil_codeinsee");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("vil_codepostal");

                    b.Property<string>("DepartementCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("dep_code");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("vil_nom");

                    b.HasKey("CodeInsee");

                    b.HasIndex("DepartementCode");

                    b.ToTable("t_e_ville_vil");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Admin", b =>
                {
                    b.HasBaseType("LeBonCoinAPI.Models.EntityFramework.Profil");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("adm_email");

                    b.Property<string>("Service")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("adm_service");

                    b.ToTable("t_e_admin_adm", t =>
                        {
                            t.HasCheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'");
                        });
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Entreprise", b =>
                {
                    b.HasBaseType("LeBonCoinAPI.Models.EntityFramework.Profil");

                    b.Property<int>("AdresseId")
                        .HasColumnType("integer")
                        .HasColumnName("adr_id");

                    b.Property<string>("Nom")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("ent_nom");

                    b.Property<int>("SecteurId")
                        .HasColumnType("integer")
                        .HasColumnName("sct_id");

                    b.Property<string>("Siret")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("ent_siret");

                    b.HasIndex("AdresseId");

                    b.HasIndex("SecteurId");

                    b.ToTable("t_e_entreprise_ent");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Particulier", b =>
                {
                    b.HasBaseType("LeBonCoinAPI.Models.EntityFramework.Profil");

                    b.Property<int?>("AdresseId")
                        .HasColumnType("integer")
                        .HasColumnName("adr_id");

                    b.Property<string>("Civilite")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("prt_civilite");

                    b.Property<DateTime?>("DateNaissance")
                        .HasColumnType("date")
                        .HasColumnName("prt_datenaissance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("prt_email");

                    b.Property<string>("Nom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("prt_nom");

                    b.Property<string>("Prenom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("prt_prenom");

                    b.HasIndex("AdresseId");

                    b.ToTable("t_e_particulier_prt", t =>
                        {
                            t.HasCheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')");

                            t.HasCheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'");
                        });
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Adresse", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Ville", "VilleAdresse")
                        .WithMany("AdressesVille")
                        .HasForeignKey("CodeInsee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VilleAdresse");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Annonce", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Adresse", "AdresseAnnonce")
                        .WithMany("AnnoncesAdresse")
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilAnnonce")
                        .WithMany("AnnoncesProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.TypeLogement", "TypeLogementAnnonce")
                        .WithMany("AnnoncesTypeLogement")
                        .HasForeignKey("TypeLogementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdresseAnnonce");

                    b.Navigation("ProfilAnnonce");

                    b.Navigation("TypeLogementAnnonce");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.CarteBancaire", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilCarteBancaire")
                        .WithMany("CartesBancairesProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfilCarteBancaire");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Commentaire", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilCommentaire")
                        .WithMany("CommentairesProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Reservation", "ReservationCommentaire")
                        .WithMany("CommentairesReservation")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfilCommentaire");

                    b.Navigation("ReservationCommentaire");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Equipement", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.TypeEquipement", "TypeEquipementEquipement")
                        .WithMany("EquipementsTypeEquipement")
                        .HasForeignKey("TypeEquipementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeEquipementEquipement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Favoris", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Annonce", "AnnonceFavoris")
                        .WithMany("FavorisAnnonce")
                        .HasForeignKey("AnnonceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilFavoris")
                        .WithMany("FavorisProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnnonceFavoris");

                    b.Navigation("ProfilFavoris");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Photo", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Annonce", "AnnoncePhoto")
                        .WithMany("PhotosAnnonce")
                        .HasForeignKey("AnnonceId");

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilPhoto")
                        .WithMany("PhotosProfil")
                        .HasForeignKey("ProfilId");

                    b.Navigation("AnnoncePhoto");

                    b.Navigation("ProfilPhoto");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.PossedeEquipement", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Annonce", "AnnonceEquipementPossede")
                        .WithMany("EquipementsPossedesAnnonce")
                        .HasForeignKey("AnnonceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Equipement", "EquipementPossede")
                        .WithMany("EquipementsPossedesDesEquipement")
                        .HasForeignKey("EquipementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnnonceEquipementPossede");

                    b.Navigation("EquipementPossede");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Reglement", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Reservation", "ReservationReglement")
                        .WithMany("ReglementsReservation")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReservationReglement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Reservation", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Annonce", "AnnonceReservation")
                        .WithMany("ReservationsAnnonce")
                        .HasForeignKey("AnnonceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilReservation")
                        .WithMany("ReservationsProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnnonceReservation");

                    b.Navigation("ProfilReservation");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Signale", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Annonce", "AnnonceSignalement")
                        .WithMany("SignalementsAnnonce")
                        .HasForeignKey("AnnonceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Profil", "ProfilSignalement")
                        .WithMany("SignalementsProfil")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnnonceSignalement");

                    b.Navigation("ProfilSignalement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Ville", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Departement", "DepartementVille")
                        .WithMany("VillesDepartement")
                        .HasForeignKey("DepartementCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartementVille");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Entreprise", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Adresse", "AdresseEntreprise")
                        .WithMany("EntreprisesAdresse")
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.SecteurActivite", "SecteurActiviteEntreprise")
                        .WithMany("EntreprisesSecteurActivite")
                        .HasForeignKey("SecteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdresseEntreprise");

                    b.Navigation("SecteurActiviteEntreprise");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Particulier", b =>
                {
                    b.HasOne("LeBonCoinAPI.Models.EntityFramework.Adresse", "AdresseParticulier")
                        .WithMany("ParticuliersAdresse")
                        .HasForeignKey("AdresseId");

                    b.Navigation("AdresseParticulier");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Adresse", b =>
                {
                    b.Navigation("AnnoncesAdresse");

                    b.Navigation("EntreprisesAdresse");

                    b.Navigation("ParticuliersAdresse");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Annonce", b =>
                {
                    b.Navigation("EquipementsPossedesAnnonce");

                    b.Navigation("FavorisAnnonce");

                    b.Navigation("PhotosAnnonce");

                    b.Navigation("ReservationsAnnonce");

                    b.Navigation("SignalementsAnnonce");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Departement", b =>
                {
                    b.Navigation("VillesDepartement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Equipement", b =>
                {
                    b.Navigation("EquipementsPossedesDesEquipement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Profil", b =>
                {
                    b.Navigation("AnnoncesProfil");

                    b.Navigation("CartesBancairesProfil");

                    b.Navigation("CommentairesProfil");

                    b.Navigation("FavorisProfil");

                    b.Navigation("PhotosProfil");

                    b.Navigation("ReservationsProfil");

                    b.Navigation("SignalementsProfil");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Reservation", b =>
                {
                    b.Navigation("CommentairesReservation");

                    b.Navigation("ReglementsReservation");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.SecteurActivite", b =>
                {
                    b.Navigation("EntreprisesSecteurActivite");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.TypeEquipement", b =>
                {
                    b.Navigation("EquipementsTypeEquipement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.TypeLogement", b =>
                {
                    b.Navigation("AnnoncesTypeLogement");
                });

            modelBuilder.Entity("LeBonCoinAPI.Models.EntityFramework.Ville", b =>
                {
                    b.Navigation("AdressesVille");
                });
#pragma warning restore 612, 618
        }
    }
}