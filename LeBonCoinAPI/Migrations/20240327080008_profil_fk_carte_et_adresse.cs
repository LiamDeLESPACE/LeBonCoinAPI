using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeBonCoinAPI.Migrations
{
    /// <inheritdoc />
    public partial class profilfkcarteetadresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebancaire_cab_prf_id",
                table: "t_e_cartebancaire_cab",
                column: "prf_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_e_cartebancaire_cab_prf_id",
                table: "t_e_cartebancaire_cab");
        }
    }
}
