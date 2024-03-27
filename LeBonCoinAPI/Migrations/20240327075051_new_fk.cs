using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class newfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_e_photo_pho_ann_id",
                table: "t_e_photo_pho",
                column: "ann_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_sct_id",
                table: "t_e_entreprise_ent",
                column: "sct_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_annonce_ann_adr_id",
                table: "t_e_annonce_ann",
                column: "adr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_annonce_ann_t_e_adresse_adr_adr_id",
                table: "t_e_annonce_ann",
                column: "adr_id",
                principalTable: "t_e_adresse_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_secteuractivite_sct_sct_id",
                table: "t_e_entreprise_ent",
                column: "sct_id",
                principalTable: "t_e_secteuractivite_sct",
                principalColumn: "sct_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho",
                column: "ann_id",
                principalTable: "t_e_annonce_ann",
                principalColumn: "ann_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_annonce_ann_t_e_adresse_adr_adr_id",
                table: "t_e_annonce_ann");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_secteuractivite_sct_sct_id",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_photo_pho_t_e_annonce_ann_ann_id",
                table: "t_e_photo_pho");

            migrationBuilder.DropIndex(
                name: "IX_t_e_photo_pho_ann_id",
                table: "t_e_photo_pho");

            migrationBuilder.DropIndex(
                name: "IX_t_e_entreprise_ent_sct_id",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropIndex(
                name: "IX_t_e_annonce_ann_adr_id",
                table: "t_e_annonce_ann");
        }
    }
}
