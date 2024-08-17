using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    /// <inheritdoc />
    public partial class modificandochaveestrangeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Gerentes_GerenteId",
                table: "Cinemas");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Gerentes_GerenteId",
                table: "Cinemas",
                column: "GerenteId",
                principalTable: "Gerentes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Gerentes_GerenteId",
                table: "Cinemas");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Gerentes_GerenteId",
                table: "Cinemas",
                column: "GerenteId",
                principalTable: "Gerentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
