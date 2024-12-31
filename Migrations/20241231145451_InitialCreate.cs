using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    İsimSoyisim = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SifreHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KatildigiEtkinlikler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakipEttigiTopluluklar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Topluluklar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Universite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UyeSayisi = table.Column<int>(type: "int", nullable: false),
                    Olusturan = table.Column<int>(type: "int", nullable: false),
                    ResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Onayli = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topluluklar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Topluluklar_Users_Olusturan",
                        column: x => x.Olusturan,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lokasyon = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Topluluk = table.Column<int>(type: "int", nullable: false),
                    KatilimSayisi = table.Column<int>(type: "int", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    ResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Topluluklar_Topluluk",
                        column: x => x.Topluluk,
                        principalTable: "Topluluklar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Katilimlar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kullanici = table.Column<int>(type: "int", nullable: false),
                    Topluluk = table.Column<int>(type: "int", nullable: false),
                    KatilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katilimlar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Katilimlar_Topluluklar_Topluluk",
                        column: x => x.Topluluk,
                        principalTable: "Topluluklar",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Katilimlar_Users_Kullanici",
                        column: x => x.Kullanici,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_Topluluk",
                table: "Etkinlikler",
                column: "Topluluk");

            migrationBuilder.CreateIndex(
                name: "IX_Katilimlar_Kullanici",
                table: "Katilimlar",
                column: "Kullanici");

            migrationBuilder.CreateIndex(
                name: "IX_Katilimlar_Topluluk",
                table: "Katilimlar",
                column: "Topluluk");

            migrationBuilder.CreateIndex(
                name: "IX_Topluluklar_Olusturan",
                table: "Topluluklar",
                column: "Olusturan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "Katilimlar");

            migrationBuilder.DropTable(
                name: "Topluluklar");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
