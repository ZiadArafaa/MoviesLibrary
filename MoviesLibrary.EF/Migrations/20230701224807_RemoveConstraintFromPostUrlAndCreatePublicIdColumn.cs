using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesLibrary.EF.Migrations
{
    public partial class RemoveConstraintFromPostUrlAndCreatePublicIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PosterUrl",
                schema: "Library",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                schema: "Library",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                schema: "Library",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "PosterUrl",
                schema: "Library",
                table: "Movies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
