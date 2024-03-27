using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class EquipementsTypeLogementFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_j_possedeequipement_peq_equ_id",
                table: "t_j_possedeequipement_peq",
                column: "equ_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_equ_tye_id",
                table: "t_e_equipement_equ",
                column: "tye_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_tyl_id",
                table: "t_e_annonce_ann",
                column: "tyl_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_annonce_ann_t_e_typelogement_tyl_tyl_id",
                table: "t_e_annonce_ann",
                column: "tyl_id",
                principalTable: "t_e_typelogement_tyl",
                principalColumn: "tyl_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_equipement_equ_t_e_typeequipement_tye_tye_id",
                table: "t_e_equipement_equ",
                column: "tye_id",
                principalTable: "t_e_typeequipement_tye",
                principalColumn: "tye_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_possedeequipement_peq_t_e_annonce_ann_ann_id",
                table: "t_j_possedeequipement_peq",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_possedeequipement_peq_t_e_equipement_equ_equ_id",
                table: "t_j_possedeequipement_peq",
                column: "equ_id",
                principalTable: "t_e_equipement_equ",
                principalColumn: "equ_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_annonce_ann_t_e_typelogement_tyl_tyl_id",
                table: "t_e_annonce_ann");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_equipement_equ_t_e_typeequipement_tye_tye_id",
                table: "t_e_equipement_equ");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_possedeequipement_peq_t_e_annonce_ann_ann_id",
                table: "t_j_possedeequipement_peq");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_possedeequipement_peq_t_e_equipement_equ_equ_id",
                table: "t_j_possedeequipement_peq");

            migrationBuilder.DropIndex(
                name: "IX_t_j_possedeequipement_peq_equ_id",
                table: "t_j_possedeequipement_peq");

            migrationBuilder.DropIndex(
                name: "IX_t_e_equipement_equ_tye_id",
                table: "t_e_equipement_equ");

            migrationBuilder.DropIndex(
                name: "IX_t_e_annonce_ann_tyl_id",
                table: "t_e_annonce_ann");
        }
    }
}
