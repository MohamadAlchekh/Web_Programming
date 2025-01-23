using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddEtkinlikKatilim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EtkinlikKatilimlar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    EtkinlikID = table.Column<int>(type: "int", nullable: false),
                    KatilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtkinlikKatilimlar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EtkinlikKatilimlar_Etkinlikler_EtkinlikID",
                        column: x => x.EtkinlikID,
                        principalTable: "Etkinlikler",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EtkinlikKatilimlar_Users_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikKatilimlar_EtkinlikID",
                table: "EtkinlikKatilimlar",
                column: "EtkinlikID");

            migrationBuilder.CreateIndex(
                name: "IX_EtkinlikKatilimlar_KullaniciID",
                table: "EtkinlikKatilimlar",
                column: "KullaniciID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtkinlikKatilimlar");
        }
    }
}
