using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesLibrary.EF.Migrations
{
    public partial class GenereMoviesRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "GenereId",
                schema: "Library",
                table: "Movies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenereId",
                schema: "Library",
                table: "Movies",
                column: "GenereId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Generes_GenereId",
                schema: "Library",
                table: "Movies",
                column: "GenereId",
                principalSchema: "Library",
                principalTable: "Generes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Generes_GenereId",
                schema: "Library",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenereId",
                schema: "Library",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenereId",
                schema: "Library",
                table: "Movies");
        }
    }
}
