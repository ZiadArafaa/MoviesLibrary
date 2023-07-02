using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesLibrary.EF.Migrations
{
    public partial class AlterPublishingDAte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishingYear",
                schema: "Library",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishingDate",
                schema: "Library",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishingDate",
                schema: "Library",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "PublishingYear",
                schema: "Library",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
