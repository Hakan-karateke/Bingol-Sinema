using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingolSinema.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToBiletandS3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bilets_RezervasyonID",
                table: "Bilets",
                column: "RezervasyonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bilets_Rezervasyons_RezervasyonID",
                table: "Bilets",
                column: "RezervasyonID",
                principalTable: "Rezervasyons",
                principalColumn: "RezervasyonID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bilets_Rezervasyons_RezervasyonID",
                table: "Bilets");

            migrationBuilder.DropIndex(
                name: "IX_Bilets_RezervasyonID",
                table: "Bilets");
        }
    }
}
