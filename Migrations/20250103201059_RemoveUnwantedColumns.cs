using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    public partial class RemoveUnwantedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KatildigiEtkinlikler",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "KullaniciAdi",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TakipEttigiTopluluklar",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KatildigiEtkinlikler",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciAdi",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TakipEttigiTopluluklar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
