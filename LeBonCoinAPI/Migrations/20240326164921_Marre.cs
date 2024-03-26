using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class Marre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ProfilSequence");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    admservice = table.Column<string>(name: "adm_service", type: "character varying(50)", maxLength: 50, nullable: false),
                    admemail = table.Column<string>(name: "adm_email", type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.prfid);
                });

            migrationBuilder.CreateTable(
                name: "Adresse",
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
                    table.PrimaryKey("PK_Adresse", x => x.adrid);
                });

            migrationBuilder.CreateTable(
                name: "Annonce",
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
                    table.PrimaryKey("PK_Annonce", x => x.annid);
                });

            migrationBuilder.CreateTable(
                name: "CarteBancaire",
                columns: table => new
                {
                    cabid = table.Column<int>(name: "cab_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    cabnumero = table.Column<string>(name: "cab_numero", type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteBancaire", x => x.cabid);
                });

            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    depcode = table.Column<string>(name: "dep_code", type: "character varying(3)", maxLength: 3, nullable: false),
                    depnom = table.Column<string>(name: "dep_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.depcode);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    sctid = table.Column<int>(name: "sct_id", type: "integer", nullable: false),
                    entsiret = table.Column<string>(name: "ent_siret", type: "character varying(14)", maxLength: 14, nullable: false),
                    entnom = table.Column<string>(name: "ent_nom", type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.prfid);
                });

            migrationBuilder.CreateTable(
                name: "Equipement",
                columns: table => new
                {
                    equid = table.Column<int>(name: "equ_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyeid = table.Column<int>(name: "tye_id", type: "integer", nullable: false),
                    equnom = table.Column<string>(name: "equ_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipement", x => x.equid);
                });

            migrationBuilder.CreateTable(
                name: "Particulier",
                columns: table => new
                {
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false, defaultValueSql: "nextval('\"ProfilSequence\"')"),
                    adrid = table.Column<int>(name: "adr_id", type: "integer", nullable: false),
                    prfhashmdp = table.Column<string>(name: "prf_hashmdp", type: "text", nullable: false),
                    prftelephone = table.Column<string>(name: "prf_telephone", type: "character varying(10)", maxLength: 10, nullable: true),
                    prtemail = table.Column<string>(name: "prt_email", type: "character varying(100)", maxLength: 100, nullable: false),
                    prtcivilite = table.Column<string>(name: "prt_civilite", type: "character varying(1)", maxLength: 1, nullable: true),
                    prtnom = table.Column<string>(name: "prt_nom", type: "character varying(50)", maxLength: 50, nullable: true),
                    prtprenom = table.Column<string>(name: "prt_prenom", type: "character varying(50)", maxLength: 50, nullable: true),
                    prtdatenaissance = table.Column<DateTime>(name: "prt_datenaissance", type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Particulier", x => x.prfid);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    phoid = table.Column<int>(name: "pho_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false),
                    phourl = table.Column<string>(name: "pho_url", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.phoid);
                });

            migrationBuilder.CreateTable(
                name: "Reglement",
                columns: table => new
                {
                    rglid = table.Column<string>(name: "rgl_id", type: "text", nullable: false),
                    resid = table.Column<int>(name: "res_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reglement", x => x.rglid);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    resid = table.Column<int>(name: "res_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    annid = table.Column<int>(name: "ann_id", type: "integer", nullable: false),
                    prfid = table.Column<int>(name: "prf_id", type: "integer", nullable: false),
                    resdatearrivee = table.Column<DateTime>(name: "res_datearrivee", type: "timestamp with time zone", nullable: false),
                    resdatedepart = table.Column<DateTime>(name: "res_datedepart", type: "timestamp with time zone", nullable: false),
                    resnombrevoyageur = table.Column<int>(name: "res_nombrevoyageur", type: "integer", nullable: false),
                    resnom = table.Column<string>(name: "res_nom", type: "character varying(50)", maxLength: 50, nullable: false),
                    resprenom = table.Column<string>(name: "res_prenom", type: "character varying(50)", maxLength: 50, nullable: false),
                    restelephone = table.Column<string>(name: "res_telephone", type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.resid);
                });

            migrationBuilder.CreateTable(
                name: "SecteurActivite",
                columns: table => new
                {
                    sctid = table.Column<int>(name: "sct_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sctnomsecteur = table.Column<string>(name: "sct_nomsecteur", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecteurActivite", x => x.sctid);
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
                });

            migrationBuilder.CreateTable(
                name: "TypeEquipement",
                columns: table => new
                {
                    tyeid = table.Column<int>(name: "tye_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tyenom = table.Column<string>(name: "tye_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEquipement", x => x.tyeid);
                });

            migrationBuilder.CreateTable(
                name: "TypeLogement",
                columns: table => new
                {
                    tylid = table.Column<int>(name: "tyl_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tylnom = table.Column<string>(name: "tyl_nom", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLogement", x => x.tylid);
                });

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    vilcodeinsee = table.Column<string>(name: "vil_codeinsee", type: "character varying(5)", maxLength: 5, nullable: false),
                    depcode = table.Column<string>(name: "dep_code", type: "character varying(3)", maxLength: 3, nullable: false),
                    vilnom = table.Column<string>(name: "vil_nom", type: "character varying(100)", maxLength: 100, nullable: false),
                    vilcodepostal = table.Column<string>(name: "vil_codepostal", type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.vilcodeinsee);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Annonce");

            migrationBuilder.DropTable(
                name: "CarteBancaire");

            migrationBuilder.DropTable(
                name: "Departement");

            migrationBuilder.DropTable(
                name: "Entreprise");

            migrationBuilder.DropTable(
                name: "Equipement");

            migrationBuilder.DropTable(
                name: "Particulier");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Reglement");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "SecteurActivite");

            migrationBuilder.DropTable(
                name: "t_j_commentaire_cmt");

            migrationBuilder.DropTable(
                name: "t_j_favoris_fav");

            migrationBuilder.DropTable(
                name: "t_j_possedeequipement_peq");

            migrationBuilder.DropTable(
                name: "t_j_signale_sig");

            migrationBuilder.DropTable(
                name: "TypeEquipement");

            migrationBuilder.DropTable(
                name: "TypeLogement");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropSequence(
                name: "ProfilSequence");
        }
    }
}
