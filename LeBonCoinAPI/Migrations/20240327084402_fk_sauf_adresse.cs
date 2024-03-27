using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class fksaufadresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho");

            migrationBuilder.AlterColumn<int>(
                name: "prf_id",
                table: "t_e_photo_pho",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ann_id",
                table: "t_e_photo_pho",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_particulier_prt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_entreprise_ent",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_admin_adm",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_signale_sig_ann_id",
                table: "t_j_signale_sig",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_favoris_fav_prf_id",
                table: "t_j_favoris_fav",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_commentaire_cmt_res_id",
                table: "t_j_commentaire_cmt",
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
                name: "IX_t_e_reglement_rgl_res_id",
                table: "t_e_reglement_rgl",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pho_prf_id",
                table: "t_e_photo_pho",
                column: "prf_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_prf_id",
                table: "t_e_annonce_ann",
                column: "prf_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_reglement_rgl_t_e_reservation_res_res_id",
                table: "t_e_reglement_rgl",
                column: "res_id",
                principalTable: "t_e_reservation_res",
                principalColumn: "res_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_reservation_res_t_e_annonce_ann_ann_id",
                table: "t_e_reservation_res",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_commentaire_cmt_t_e_reservation_res_res_id",
                table: "t_j_commentaire_cmt",
                column: "res_id",
                principalTable: "t_e_reservation_res",
                principalColumn: "res_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_favoris_fav_t_e_annonce_ann_ann_id",
                table: "t_j_favoris_fav",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_signale_sig_t_e_annonce_ann_ann_id",
                table: "t_j_signale_sig",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_reglement_rgl_t_e_reservation_res_res_id",
                table: "t_e_reglement_rgl");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_reservation_res_t_e_annonce_ann_ann_id",
                table: "t_e_reservation_res");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_commentaire_cmt_t_e_reservation_res_res_id",
                table: "t_j_commentaire_cmt");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_favoris_fav_t_e_annonce_ann_ann_id",
                table: "t_j_favoris_fav");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_signale_sig_t_e_annonce_ann_ann_id",
                table: "t_j_signale_sig");

            migrationBuilder.DropIndex(
                name: "IX_t_j_signale_sig_ann_id",
                table: "t_j_signale_sig");

            migrationBuilder.DropIndex(
                name: "IX_t_j_favoris_fav_prf_id",
                table: "t_j_favoris_fav");

            migrationBuilder.DropIndex(
                name: "IX_t_j_commentaire_cmt_res_id",
                table: "t_j_commentaire_cmt");

            migrationBuilder.DropIndex(
                name: "IX_t_e_reservation_res_ann_id",
                table: "t_e_reservation_res");

            migrationBuilder.DropIndex(
                name: "IX_t_e_reservation_res_prf_id",
                table: "t_e_reservation_res");

            migrationBuilder.DropIndex(
                name: "IX_t_e_reglement_rgl_res_id",
                table: "t_e_reglement_rgl");

            migrationBuilder.DropIndex(
                name: "IX_t_e_photo_pho_prf_id",
                table: "t_e_photo_pho");

            migrationBuilder.DropIndex(
                name: "IX_t_e_annonce_ann_prf_id",
                table: "t_e_annonce_ann");

            migrationBuilder.AlterColumn<int>(
                name: "prf_id",
                table: "t_e_photo_pho",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ann_id",
                table: "t_e_photo_pho",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_particulier_prt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_entreprise_ent",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "adr_id",
                table: "t_e_admin_adm",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
