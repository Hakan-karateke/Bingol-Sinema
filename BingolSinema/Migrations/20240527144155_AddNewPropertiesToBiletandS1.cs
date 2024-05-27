using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingolSinema.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToBiletandS1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeansFiyat",
                table: "Seanss",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FilmResimUrl",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Fiyat",
                table: "Bilets",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeansFiyat",
                table: "Seanss");

            migrationBuilder.DropColumn(
                name: "FilmResimUrl",
                table: "Films");

            migrationBuilder.AlterColumn<double>(
                name: "Fiyat",
                table: "Bilets",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
