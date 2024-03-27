using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class renamefull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ville",
                table: "Ville");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeLogement",
                table: "TypeLogement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeEquipement",
                table: "TypeEquipement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecteurActivite",
                table: "SecteurActivite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reglement",
                table: "Reglement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Particulier",
                table: "Particulier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipement",
                table: "Equipement");

            migrationBuilder.RenameTable(
                name: "Ville",
                newName: "t_e_ville_vil");

            migrationBuilder.RenameTable(
                name: "TypeLogement",
                newName: "t_e_typelogement_tyl");

            migrationBuilder.RenameTable(
                name: "TypeEquipement",
                newName: "t_e_typeequipement_tye");

            migrationBuilder.RenameTable(
                name: "SecteurActivite",
                newName: "t_e_secteuractivite_sct");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "t_e_reservation_res");

            migrationBuilder.RenameTable(
                name: "Reglement",
                newName: "t_e_reglement_rgl");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "t_e_photo_pho");

            migrationBuilder.RenameTable(
                name: "Particulier",
                newName: "t_e_particulier_prt");

            migrationBuilder.RenameTable(
                name: "Equipement",
                newName: "t_e_equipement_equ");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_ville_vil",
                table: "t_e_ville_vil",
                column: "vil_codeinsee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typelogement_tyl",
                table: "t_e_typelogement_tyl",
                column: "tyl_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_typeequipement_tye",
                table: "t_e_typeequipement_tye",
                column: "tye_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_secteuractivite_sct",
                table: "t_e_secteuractivite_sct",
                column: "sct_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_reservation_res",
                table: "t_e_reservation_res",
                column: "res_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_reglement_rgl",
                table: "t_e_reglement_rgl",
                column: "rgl_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_photo_pho",
                table: "t_e_photo_pho",
                column: "pho_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_particulier_prt",
                table: "t_e_particulier_prt",
                column: "prf_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_e_equipement_equ",
                table: "t_e_equipement_equ",
                column: "equ_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_ville_vil",
                table: "t_e_ville_vil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typelogement_tyl",
                table: "t_e_typelogement_tyl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_typeequipement_tye",
                table: "t_e_typeequipement_tye");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_secteuractivite_sct",
                table: "t_e_secteuractivite_sct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_reservation_res",
                table: "t_e_reservation_res");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_reglement_rgl",
                table: "t_e_reglement_rgl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_photo_pho",
                table: "t_e_photo_pho");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_particulier_prt",
                table: "t_e_particulier_prt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_e_equipement_equ",
                table: "t_e_equipement_equ");

            migrationBuilder.RenameTable(
                name: "t_e_ville_vil",
                newName: "Ville");

            migrationBuilder.RenameTable(
                name: "t_e_typelogement_tyl",
                newName: "TypeLogement");

            migrationBuilder.RenameTable(
                name: "t_e_typeequipement_tye",
                newName: "TypeEquipement");

            migrationBuilder.RenameTable(
                name: "t_e_secteuractivite_sct",
                newName: "SecteurActivite");

            migrationBuilder.RenameTable(
                name: "t_e_reservation_res",
                newName: "Reservation");

            migrationBuilder.RenameTable(
                name: "t_e_reglement_rgl",
                newName: "Reglement");

            migrationBuilder.RenameTable(
                name: "t_e_photo_pho",
                newName: "Photo");

            migrationBuilder.RenameTable(
                name: "t_e_particulier_prt",
                newName: "Particulier");

            migrationBuilder.RenameTable(
                name: "t_e_equipement_equ",
                newName: "Equipement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ville",
                table: "Ville",
                column: "vil_codeinsee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeLogement",
                table: "TypeLogement",
                column: "tyl_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeEquipement",
                table: "TypeEquipement",
                column: "tye_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecteurActivite",
                table: "SecteurActivite",
                column: "sct_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "res_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reglement",
                table: "Reglement",
                column: "rgl_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "pho_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Particulier",
                table: "Particulier",
                column: "prf_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipement",
                table: "Equipement",
                column: "equ_id");
        }
    }
}
