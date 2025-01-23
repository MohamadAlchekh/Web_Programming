using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEtkinlikKatilimCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID",
                table: "EtkinlikKatilimlar");

            migrationBuilder.DropForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID1",
                table: "EtkinlikKatilimlar");

            migrationBuilder.DropIndex(
                name: "IX_EtkinlikKatilimlar_EtkinlikID1",
                table: "EtkinlikKatilimlar");

            migrationBuilder.DropColumn(
                name: "EtkinlikID1",
                table: "EtkinlikKatilimlar");

            migrationBuilder.AddForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID",
                principalTable: "Etkinlikler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID",
                table: "EtkinlikKatilimlar");

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
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID",
                principalTable: "Etkinlikler",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID1",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID1",
                principalTable: "Etkinlikler",
                principalColumn: "ID");
        }
    }
}
