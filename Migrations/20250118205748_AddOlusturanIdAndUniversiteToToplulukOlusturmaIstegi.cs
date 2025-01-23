using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddOlusturanIdAndUniversiteToToplulukOlusturmaIstegi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OlusturanId",
                table: "ToplulukOlusturmaIstekleri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Universite",
                table: "ToplulukOlusturmaIstekleri",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EtkinlikID1",
                table: "EtkinlikKatilimlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikKatilimlar_EtkinlikID1",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID1");

            migrationBuilder.AddForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID1",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID1",
                principalTable: "Etkinlikler",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID1",
                table: "EtkinlikKatilimlar");

            migrationBuilder.DropIndex(
                name: "IX_EtkinlikKatilimlar_EtkinlikID1",
                table: "EtkinlikKatilimlar");

            migrationBuilder.DropColumn(
                name: "OlusturanId",
                table: "ToplulukOlusturmaIstekleri");

            migrationBuilder.DropColumn(
                name: "Universite",
                table: "ToplulukOlusturmaIstekleri");

            migrationBuilder.DropColumn(
                name: "EtkinlikID1",
                table: "EtkinlikKatilimlar");
        }
    }
}
