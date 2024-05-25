using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BingolSinema.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "KayitTarihi",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Soyadi",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Yas",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adi",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "KayitTarihi",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Soyadi",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Yas",
                table: "Admins");
        }
    }
}
