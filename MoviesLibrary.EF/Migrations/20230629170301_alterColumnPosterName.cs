using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesLibrary.EF.Migrations
{
    public partial class alterColumnPosterName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterName",
                schema: "Library",
                table: "Movies",
                newName: "PosterUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterUrl",
                schema: "Library",
                table: "Movies",
                newName: "PosterName");
        }
    }
}
