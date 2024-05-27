using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingolSinema.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToBiletandS2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyons_KullaniciID",
                table: "Rezervasyons",
                column: "KullaniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervasyons_Kullanicis_KullaniciID",
                table: "Rezervasyons",
                column: "KullaniciID",
                principalTable: "Kullanicis",
                principalColumn: "KullaniciID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervasyons_Kullanicis_KullaniciID",
                table: "Rezervasyons");

            migrationBuilder.DropIndex(
                name: "IX_Rezervasyons_KullaniciID",
                table: "Rezervasyons");
        }
    }
}
