using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class DbRemote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ProfilSequence");

            migrationBuilder.CreateTable(
                name: "t_e_admin_adm",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    admservice = table.Column<string>(name: "adm_service", type: "character varying(50)", maxLength: 50, nullable: true),
                    admemail = table.Column<string>(name: "adm_email", type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_admin_adm", x => x.prfid);
                    table.CheckConstraint("ck_adm_email", "adm_email like '%_@__%.__%'");
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_cab",
                columns: table => new
                {
                    cabid = table.Column<int>(name: "cab_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    cabnumero = table.Column<string>(name: "cab_numero", type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_cartebancaire_cab", x => x.cabid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_departement_dep",
                columns: table => new
                {
                    depcode = table.Column<string>(name: "dep_code", type: "character varying(3)", maxLength: 3, nullable: false),
                    depnom = table.Column<string>(name: "dep_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_departement_dep", x => x.depcode);
                });

            migrationBuilder.CreateTable(
                name: "t_e_secteuractivite_sct",
                columns: table => new
                {
                    sctid = table.Column<int>(name: "sct_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sctnomsecteur = table.Column<string>(name: "sct_nomsecteur", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_secteuractivite_sct", x => x.sctid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeequipement_tye",
                columns: table => new
                {
                    tyeid = table.Column<int>(name: "tye_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyenom = table.Column<string>(name: "tye_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_typeequipement_tye", x => x.tyeid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typelogement_tyl",
                columns: table => new
                {
                    tylid = table.Column<int>(name: "tyl_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tylnom = table.Column<string>(name: "tyl_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_typelogement_tyl", x => x.tylid);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                columns: table => new
                {
                    vilcodeinsee = table.Column<string>(name: "vil_codeinsee", type: "character varying(5)", maxLength: 5, nullable: false),
                    depcode = table.Column<string>(name: "dep_code", type: "character varying(3)", maxLength: 3, nullable: false),
                    vilnom = table.Column<string>(name: "vil_nom", type: "character varying(100)", maxLength: 100, nullable: false),
                    vilcodepostal = table.Column<string>(name: "vil_codepostal", type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_ville_vil", x => x.vilcodeinsee);
                    table.ForeignKey(
                        name: "FK_t_e_ville_vil_t_e_departement_dep_dep_code",
                        column: x => x.depcode,
                        principalTable: "t_e_departement_dep",
                        principalColumn: "dep_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipement_equ",
                columns: table => new
                {
                    equid = table.Column<int>(name: "equ_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyeid = table.Column<int>(name: "tye_id", type: "integer", nullable: false),
                    equnom = table.Column<string>(name: "equ_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_equipement_equ", x => x.equid);
                    table.ForeignKey(
                        name: "FK_t_e_equipement_equ_t_e_typeequipement_tye_tye_id",
                        column: x => x.tyeid,
                        principalTable: "t_e_typeequipement_tye",
                        principalColumn: "tye_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vilcodeinsee = table.Column<string>(name: "vil_codeinsee", type: "character varying(5)", maxLength: 5, nullable: false),
                    adrrue = table.Column<string>(name: "adr_rue", type: "character varying(100)", maxLength: 100, nullable: false),
                    adrnumero = table.Column<int>(name: "adr_numero", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_adresse_adr", x => x.adrid);
                    table.ForeignKey(
                        name: "FK_t_e_adresse_adr_t_e_ville_vil_vil_codeinsee",
                        column: x => x.vilcodeinsee,
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_codeinsee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_annonce_ann",
                columns: table => new
                {
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false),
                    tylid = table.Column<int>(name: "tyl_id", type: "integer", nullable: false),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    anntitre = table.Column<string>(name: "ann_titre", type: "character varying(100)", maxLength: 100, nullable: false),
                    anndureeminimumsejour = table.Column<int>(name: "ann_dureeminimumsejour", type: "integer", nullable: false),
                    annactive = table.Column<bool>(name: "ann_active", type: "boolean", nullable: false),
                    anndatepublication = table.Column<DateTime>(name: "ann_datepublication", type: "date", nullable: false),
                    anndescription = table.Column<string>(name: "ann_description", type: "text", nullable: true),
                    annetoile = table.Column<int>(name: "ann_etoile", type: "integer", nullable: false),
                    annnombrepersonnesmax = table.Column<int>(name: "ann_nombrepersonnesmax", type: "integer", nullable: false),
                    annprixparnuit = table.Column<double>(name: "ann_prixparnuit", type: "double precision", nullable: false),
                    annnombrechambres = table.Column<int>(name: "ann_nombrechambres", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_annonce_ann", x => x.annid);
                    table.CheckConstraint("ck_ann_dureeminimumsejour", "ann_dureeminimumsejour > 0");
                    table.CheckConstraint("ck_ann_etoile", "ann_etoile > 0 AND ann_etoile <= 5");
                    table.CheckConstraint("ck_ann_nombrechambres", "ann_nombrechambres > 0");
                    table.CheckConstraint("ck_ann_nombrepersonnesmax", "ann_nombrepersonnesmax > 0");
                    table.CheckConstraint("ck_ann_prixparnuit", "ann_prixparnuit > 0");
                    table.ForeignKey(
                        name: "FK_t_e_annonce_ann_t_e_adresse_adr_adr_id",
                        column: x => x.adrid,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_annonce_ann_t_e_typelogement_tyl_tyl_id",
                        column: x => x.tylid,
                        principalTable: "t_e_typelogement_tyl",
                        principalColumn: "tyl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entreprise_ent",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    sctid = table.Column<int>(name: "sct_id", type: "integer", nullable: false),
                    entsiret = table.Column<string>(name: "ent_siret", type: "character varying(14)", maxLength: 14, nullable: false),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false),
                    entnom = table.Column<string>(name: "ent_nom", type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entreprise_ent", x => x.prfid);
                    table.ForeignKey(
                        name: "FK_t_e_entreprise_ent_t_e_adresse_adr_adr_id",
                        column: x => x.adrid,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_entreprise_ent_t_e_secteuractivite_sct_sct_id",
                        column: x => x.sctid,
                        principalTable: "t_e_secteuractivite_sct",
                        principalColumn: "sct_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_particulier_prt",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    prtemail = table.Column<string>(name: "prt_email", type: "character varying(100)", maxLength: 100, nullable: false),
                    prtcivilite = table.Column<string>(name: "prt_civilite", type: "character varying(1)", maxLength: 1, nullable: true),
                    prtnom = table.Column<string>(name: "prt_nom", type: "character varying(50)", maxLength: 50, nullable: true),
                    prtprenom = table.Column<string>(name: "prt_prenom", type: "character varying(50)", maxLength: 50, nullable: true),
                    prtdatenaissance = table.Column<DateTime>(name: "prt_datenaissance", type: "date", nullable: true),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_particulier_prt", x => x.prfid);
                    table.CheckConstraint("ck_prt_civilite", "prt_civilite in ('H', 'F')");
                    table.CheckConstraint("ck_prt_email", "prt_email like '%_@__%.__%'");
                    table.ForeignKey(
                        name: "FK_t_e_particulier_prt_t_e_adresse_adr_adr_id",
                        column: x => x.adrid,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pho",
                columns: table => new
                {
                    phoid = table.Column<int>(name: "pho_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: true),
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: true),
                    phourl = table.Column<string>(name: "pho_url", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_photo_pho", x => x.phoid);
                    table.ForeignKey(
                        name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                        column: x => x.annid,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_res",
                columns: table => new
                {
                    resid = table.Column<int>(name: "res_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    resdatearrivee = table.Column<DateTime>(name: "res_datearrivee", type: "date", nullable: false),
                    resdatedepart = table.Column<DateTime>(name: "res_datedepart", type: "date", nullable: false),
                    resnombrevoyageur = table.Column<int>(name: "res_nombrevoyageur", type: "integer", nullable: false),
                    resnom = table.Column<string>(name: "res_nom", type: "character varying(50)", maxLength: 50, nullable: false),
                    resprenom = table.Column<string>(name: "res_prenom", type: "character varying(50)", maxLength: 50, nullable: false),
                    restelephone = table.Column<string>(name: "res_telephone", type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_reservation_res", x => x.resid);
                    table.CheckConstraint("ck_res_nombrevoyageur", "res_nombrevoyageur > 0");
                    table.CheckConstraint("ck_res_telephone", "res_telephone LIKE ('06%') or res_telephone LIKE ('07%')OR res_telephone IS NULL");
                    table.ForeignKey(
                        name: "FK_t_e_reservation_res_t_e_annonce_ann_ann_id",
                        column: x => x.annid,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_favoris_fav",
                columns: table => new
                {
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_favoris_fav", x => new { x.annid, x.prfid });
                    table.ForeignKey(
                        name: "FK_t_j_favoris_fav_t_e_annonce_ann_ann_id",
                        column: x => x.annid,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_possedeequipement_peq",
                columns: table => new
                {
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false),
                    equid = table.Column<int>(name: "equ_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_possedeequipement_peq", x => new { x.annid, x.equid });
                    table.ForeignKey(
                        name: "FK_t_j_possedeequipement_peq_t_e_annonce_ann_ann_id",
                        column: x => x.annid,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_possedeequipement_peq_t_e_equipement_equ_equ_id",
                        column: x => x.equid,
                        principalTable: "t_e_equipement_equ",
                        principalColumn: "equ_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_signale_sig",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_signale_sig", x => new { x.prfid, x.annid });
                    table.ForeignKey(
                        name: "FK_t_j_signale_sig_t_e_annonce_ann_ann_id",
                        column: x => x.annid,
                        principalTable: "t_e_annonce_ann",
                        principalColumn: "ann_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reglement_rgl",
                columns: table => new
                {
                    rglid = table.Column<string>(name: "rgl_id", type: "text", nullable: false),
                    resid = table.Column<int>(name: "res_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_reglement_rgl", x => x.rglid);
                    table.ForeignKey(
                        name: "FK_t_e_reglement_rgl_t_e_reservation_res_res_id",
                        column: x => x.resid,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_commentaire_cmt",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    resid = table.Column<int>(name: "res_id", type: "integer", nullable: false),
                    cmtcontenu = table.Column<string>(name: "cmt_contenu", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_commentaire_cmt", x => new { x.prfid, x.resid });
                    table.ForeignKey(
                        name: "FK_t_j_commentaire_cmt_t_e_reservation_res_res_id",
                        column: x => x.resid,
                        principalTable: "t_e_reservation_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_vil_codeinsee",
                table: "t_e_adresse_adr",
                column: "vil_codeinsee");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_adr_id",
                table: "t_e_annonce_ann",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_prf_id",
                table: "t_e_annonce_ann",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_tyl_id",
                table: "t_e_annonce_ann",
                column: "tyl_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebancaire_cab_prf_id",
                table: "t_e_cartebancaire_cab",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_adr_id",
                table: "t_e_entreprise_ent",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_sct_id",
                table: "t_e_entreprise_ent",
                column: "sct_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_equ_tye_id",
                table: "t_e_equipement_equ",
                column: "tye_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_particulier_prt_adr_id",
                table: "t_e_particulier_prt",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pho_ann_id",
                table: "t_e_photo_pho",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pho_prf_id",
                table: "t_e_photo_pho",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reglement_rgl_res_id",
                table: "t_e_reglement_rgl",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_res_ann_id",
                table: "t_e_reservation_res",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_res_prf_id",
                table: "t_e_reservation_res",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_ville_vil_dep_code",
                table: "t_e_ville_vil",
                column: "dep_code");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_commentaire_cmt_res_id",
                table: "t_j_commentaire_cmt",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favoris_fav_prf_id",
                table: "t_j_favoris_fav",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_possedeequipement_peq_equ_id",
                table: "t_j_possedeequipement_peq",
                column: "equ_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_signale_sig_ann_id",
                table: "t_j_signale_sig",
                column: "ann_id");
        }

        /// <inheritdoc />
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
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_typelogement_tyl");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil");

            migrationBuilder.DropTable(
                name: "t_e_departement_dep");

            migrationBuilder.DropSequence(
                name: "ProfilSequence");
        }
    }
}
