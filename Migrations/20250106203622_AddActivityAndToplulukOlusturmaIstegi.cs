using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityAndToplulukOlusturmaIstegi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ToplulukOlusturmaIstekleri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToplulukAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaskanAdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KanitBelgesiYolu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BasvuruTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToplulukOlusturmaIstekleri", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ToplulukOlusturmaIstekleri");
        }
    }
}
