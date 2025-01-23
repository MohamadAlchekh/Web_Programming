using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToplulukKatilimCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Katilimlar_Topluluklar_Topluluk",
                table: "Katilimlar");

            migrationBuilder.AddForeignKey(
                name: "FK_Katilimlar_Topluluklar_Topluluk",
                table: "Katilimlar",
                column: "Topluluk",
                principalTable: "Topluluklar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Katilimlar_Topluluklar_Topluluk",
                table: "Katilimlar");

            migrationBuilder.AddForeignKey(
                name: "FK_Katilimlar_Topluluklar_Topluluk",
                table: "Katilimlar",
                column: "Topluluk",
                principalTable: "Topluluklar",
                principalColumn: "ID");
        }
    }
}
