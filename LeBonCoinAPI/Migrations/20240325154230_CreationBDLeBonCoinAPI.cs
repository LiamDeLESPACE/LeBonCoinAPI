using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    public partial class CreationBDLeBonCoinAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_departement_dep",
                columns: table => new
                {
                    dep_code = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    dep_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dep", x => x.dep_code);
                });

            migrationBuilder.CreateTable(
                name: "t_e_secteuractivite_sct",
                columns: table => new
                {
                    sct_id = table.Column<int>(type: "integer", nullable: false),
                    sct_nomsecteur = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sct", x => x.sct_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeequipement_tye",
                columns: table => new
                {
                    tye_id = table.Column<int>(type: "integer", nullable: false),
                    tye_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tye", x => x.tye_id);
                    table.CheckConstraint("ck_tye_nom", "tye_nom in ('Equipements', 'Extérieur', 'Services et Accessibilité')");
                });

            migrationBuilder.CreateTable(
                name: "t_e_typelogement_tyl",
                columns: table => new
                {
                    tyl_id = table.Column<int>(type: "integer", nullable: false),
                    tyl_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tyl", x => x.tyl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                columns: table => new
                {
                    vil_codeinsee = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false),
                    dep_code = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    vil_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vil_codepostal = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vil", x => x.vil_codeinsee);
                    table.ForeignKey(
                        name: "fk_vil_dep",
                        column: x => x.dep_code,
                        principalTable: "t_e_departement_dep",
                        principalColumn: "dep_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipement_equ",
                columns: table => new
                {
                    equ_id = table.Column<int>(type: "integer", nullable: false),
                    tye_id = table.Column<int>(type: "integer", nullable: false),
                    equ_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equ", x => x.equ_id);
                    table.ForeignKey(
                        name: "fk_equ_tye",
                        column: x => x.tye_id,
                        principalTable: "t_e_typeequipement_tye",
                        principalColumn: "tye_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    vil_codeinsee = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false),
                    adr_rue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    adr_numero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adr", x => x.adr_id);
                    table.CheckConstraint("ck_adr_adressenum", "adr_adressenum between 0 and 1000");
                    table.ForeignKey(
                        name: "fk_adr_vil",
                        column: x => x.vil_codeinsee,
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_codeinsee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_profil_prf",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    prf_hashmdp = table.Column<string>(type: "text", nullable: false),
                    prf_telephone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => x.prf_id);
                    table.CheckConstraint("ck_prf_telephone", "prf_telephone LIKE ('06%') or prf_telephone LIKE ('07%')OR prf_telephone IS NULL");
                    table.CheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'");
                    table.CheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')");
                    table.CheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'");
                    table.ForeignKey(
                        name: "fk_prf_adr",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_admin_adm",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    adm_service = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adm_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => x.prf_id);
                    table.CheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'");
                    table.ForeignKey(
                        name: "FK_t_e_admin_adm_t_e_profil_prf_prf_id",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_annonce_ann",
                columns: table => new
                {
                    ann_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false),
                    tyl_id = table.Column<int>(type: "integer", nullable: false),
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    ann_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ann_dureeminimumsejour = table.Column<int>(type: "integer", nullable: false),
                    ann_active = table.Column<bool>(type: "boolean", nullable: false),
                    ann_datepublication = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ann_description = table.Column<string>(type: "text", nullable: false),
                    ann_etoile = table.Column<int>(type: "integer", nullable: false),
                    ann_nombrepersonnesmax = table.Column<int>(type: "integer", nullable: false),
                    ann_prixparnuit = table.Column<double>(type: "double precision", nullable: false),
                    ann_nombrechambres = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ann", x => x.ann_id);
                    table.CheckConstraint("ck_ann_active", "ann_active in ('FALSE', 'TRUE')");
                    table.CheckConstraint("ck_ann_dureeminimumsejour", "ann_dureeminimumsejour > 0");
                    table.CheckConstraint("ck_ann_etoile", "ann_etoile > 0 AND ann_etoile <= 5");
                    table.CheckConstraint("ck_ann_nombrechambres", "ann_nombrechambres > 0");
                    table.CheckConstraint("ck_ann_nombrepersonnesmax", "ann_nombrepersonnesmax > 0");
                    table.CheckConstraint("ck_ann_prixparnuit", "ann_prixparnuit > 0");
                    table.ForeignKey(
                        name: "fk_ann_adr",
                        column: x => x.adr_id,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ann_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ann_tyl",
                        column: x => x.tyl_id,
                        principalTable: "t_e_typelogement_tyl",
                        principalColumn: "tyl_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_cab",
                columns: table => new
                {
                    cab_id = table.Column<int>(type: "integer", nullable: false),
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    cab_numero = table.Column<string>(type: "character(16)", fixedLength: true, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cab", x => x.cab_id);
                    table.ForeignKey(
                        name: "fk_cmt_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entreprise_ent",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    sct_id = table.Column<int>(type: "integer", nullable: false),
                    ent_siret = table.Column<string>(type: "character(14)", fixedLength: true, maxLength: 14, nullable: false),
                    ent_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => x.prf_id);
                    table.ForeignKey(
                        name: "fk_ent_sct",
                        column: x => x.sct_id,
                        principalTable: "t_e_secteuractivite_sct",
                        principalColumn: "sct_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_e_entreprise_ent_t_e_profil_prf_prf_id",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_particulier_prt",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    prt_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    prt_civilite = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    prt_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prt_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prt_datenaissance = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prf", x => x.prf_id);
                    table.CheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')");
                    table.CheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'");
                    table.ForeignKey(
                        name: "FK_t_e_particulier_prt_t_e_profil_prf_prf_id",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pho",
                columns: table => new
                {
                    pho_id = table.Column<int>(type: "integer", nullable: false),
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    ann_id = table.Column<int>(type: "integer", nullable: false),
                    pho_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pho", x => x.pho_id);
                    table.ForeignKey(
                        name: "fk_pho_ann",
                        column: x => x.ann_id,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pho_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_res",
                columns: table => new
                {
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    ann_id = table.Column<int>(type: "integer", nullable: false),
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    res_datearrivee = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    res_datedepart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()+1"),
                    res_nombrevoyageur = table.Column<int>(type: "integer", nullable: false),
                    res_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    res_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    res_telephone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_res", x => x.res_id);
                    table.CheckConstraint("ck_res_nombrevoyageur", "res_nombrevoyageur > 0");
                    table.CheckConstraint("ck_res_telephone", "res_telephone LIKE ('06%') or res_telephone LIKE ('07%')OR res_telephone IS NULL");
                    table.ForeignKey(
                        name: "fk_res_ann",
                        column: x => x.ann_id,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_e_reservation_res_t_e_profil_prf_prf_id",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_favoris_fav",
                columns: table => new
                {
                    ann_id = table.Column<int>(type: "integer", nullable: false),
                    prf_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fav", x => new { x.ann_id, x.prf_id });
                    table.ForeignKey(
                        name: "fk_fav_ann",
                        column: x => x.ann_id,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fav_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_possedeequipement_peq",
                columns: table => new
                {
                    ann_id = table.Column<int>(type: "integer", nullable: false),
                    equ_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_peq", x => new { x.ann_id, x.equ_id });
                    table.ForeignKey(
                        name: "fk_peq_ann",
                        column: x => x.ann_id,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_peq_equ",
                        column: x => x.equ_id,
                        principalTable: "t_e_equipement_equ",
                        principalColumn: "equ_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_signale_sig",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    ann_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sig", x => new { x.prf_id, x.ann_id });
                    table.ForeignKey(
                        name: "fk_sig_ann",
                        column: x => x.ann_id,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sig_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reglement_rgl",
                columns: table => new
                {
                    rgl_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rgl", x => x.rgl_id);
                    table.ForeignKey(
                        name: "fk_rgl_res",
                        column: x => x.res_id,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_commentaire_cmt",
                columns: table => new
                {
                    prf_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    cmt_contenu = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmt", x => new { x.prf_id, x.res_id });
                    table.ForeignKey(
                        name: "fk_cmt_prf",
                        column: x => x.prf_id,
                        principalTable: "t_e_profil_prf",
                        principalColumn: "prf_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmt_res",
                        column: x => x.res_id,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "prf_pk",
                table: "t_e_admin_adm",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "adr_pk",
                table: "t_e_adresse_adr",
                column: "adr_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_adr_vil",
                table: "t_e_adresse_adr",
                column: "vil_codeinsee");

            migrationBuilder.CreateIndex(
                name: "ann_pk",
                table: "t_e_annonce_ann",
                column: "ann_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_ann_adr",
                table: "t_e_annonce_ann",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "fk_ann_prf",
                table: "t_e_annonce_ann",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "fk_ann_tyl",
                table: "t_e_annonce_ann",
                column: "tyl_id");

            migrationBuilder.CreateIndex(
                name: "cab_pk",
                table: "t_e_cartebancaire_cab",
                column: "cab_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_cab_prf",
                table: "t_e_cartebancaire_cab",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "dep_pk",
                table: "t_e_departement_dep",
                column: "dep_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_ent_sct",
                table: "t_e_entreprise_ent",
                column: "sct_id");

            migrationBuilder.CreateIndex(
                name: "prf_pk",
                table: "t_e_entreprise_ent",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "equ_pk",
                table: "t_e_equipement_equ",
                column: "equ_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_equ_tye",
                table: "t_e_equipement_equ",
                column: "tye_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_particulier_prt_prt_email",
                table: "t_e_particulier_prt",
                column: "prt_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "prf_pk",
                table: "t_e_particulier_prt",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_pho_ann",
                table: "t_e_photo_pho",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "fk_pho_prf",
                table: "t_e_photo_pho",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "pho_pk",
                table: "t_e_photo_pho",
                column: "pho_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_prf_adr",
                table: "t_e_profil_prf",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "prf_pk",
                table: "t_e_profil_prf",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_rgl_res",
                table: "t_e_reglement_rgl",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "reglements_pk",
                table: "t_e_reglement_rgl",
                column: "rgl_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_res_ann",
                table: "t_e_reservation_res",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "res_fk",
                table: "t_e_reservation_res",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "res_pk",
                table: "t_e_reservation_res",
                column: "res_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "sct_pk",
                table: "t_e_secteuractivite_sct",
                column: "sct_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tye_pk",
                table: "t_e_typeequipement_tye",
                column: "tye_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tyl_pk",
                table: "t_e_typelogement_tyl",
                column: "tyl_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_vil_dep",
                table: "t_e_ville_vil",
                column: "dep_code");

            migrationBuilder.CreateIndex(
                name: "vil_pk",
                table: "t_e_ville_vil",
                column: "vil_codeinsee",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_cmt_prf",
                table: "t_j_commentaire_cmt",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_cmt_res",
                table: "t_j_commentaire_cmt",
                column: "res_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_fav_ann",
                table: "t_j_favoris_fav",
                column: "ann_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_fav_prf",
                table: "t_j_favoris_fav",
                column: "prf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_peq_ann",
                table: "t_j_possedeequipement_peq",
                column: "ann_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_peq_equ",
                table: "t_j_possedeequipement_peq",
                column: "equ_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_sig_ann",
                table: "t_j_signale_sig",
                column: "ann_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_sig_prf",
                table: "t_j_signale_sig",
                column: "prf_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_admin_adm");

            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_cab");

            migrationBuilder.DropTable(
                name: "t_e_entreprise_ent");

            migrationBuilder.DropTable(
                name: "t_e_particulier_prt");

            migrationBuilder.DropTable(
                name: "t_e_photo_pho");

            migrationBuilder.DropTable(
                name: "t_e_reglement_rgl");

            migrationBuilder.DropTable(
                name: "t_j_commentaire_cmt");

            migrationBuilder.DropTable(
                name: "t_j_favoris_fav");

            migrationBuilder.DropTable(
                name: "t_j_possedeequipement_peq");

            migrationBuilder.DropTable(
                name: "t_j_signale_sig");

            migrationBuilder.DropTable(
                name: "t_e_secteuractivite_sct");

            migrationBuilder.DropTable(
                name: "t_e_reservation_res");

            migrationBuilder.DropTable(
                name: "t_e_equipement_equ");

            migrationBuilder.DropTable(
                name: "t_e_annonce_ann");

            migrationBuilder.DropTable(
                name: "t_e_typeequipement_tye");

            migrationBuilder.DropTable(
                name: "t_e_profil_prf");

            migrationBuilder.DropTable(
                name: "t_e_typelogement_tyl");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil");

            migrationBuilder.DropTable(
                name: "t_e_departement_dep");
        }
    }
}
