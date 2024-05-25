using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingolSinema.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToKullanici : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Cinsiyet",
                table: "Kullanicis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Soyad",
                table: "Kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Yas",
                table: "Kullanicis",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Kullanicis");

            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "Kullanicis");

            migrationBuilder.DropColumn(
                name: "Soyad",
                table: "Kullanicis");

            migrationBuilder.DropColumn(
                name: "Yas",
                table: "Kullanicis");
        }
    }
}
