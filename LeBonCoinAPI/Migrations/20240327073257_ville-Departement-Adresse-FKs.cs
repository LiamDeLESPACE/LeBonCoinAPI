using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class villeDepartementAdresseFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_e_ville_vil_dep_code",
                table: "t_e_ville_vil",
                column: "dep_code");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_vil_codeinsee",
                table: "t_e_adresse_adr",
                column: "vil_codeinsee");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_vil_codeinsee",
                table: "t_e_adresse_adr",
                column: "vil_codeinsee",
                principalTable: "t_e_ville_vil",
                principalColumn: "vil_codeinsee",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_ville_vil_t_e_departement_dep_dep_code",
                table: "t_e_ville_vil",
                column: "dep_code",
                principalTable: "t_e_departement_dep",
                principalColumn: "dep_code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_adresse_adr_t_e_ville_vil_vil_codeinsee",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_ville_vil_t_e_departement_dep_dep_code",
                table: "t_e_ville_vil");

            migrationBuilder.DropIndex(
                name: "IX_t_e_ville_vil_dep_code",
                table: "t_e_ville_vil");

            migrationBuilder.DropIndex(
                name: "IX_t_e_adresse_adr_vil_codeinsee",
                table: "t_e_adresse_adr");
        }
    }
}
