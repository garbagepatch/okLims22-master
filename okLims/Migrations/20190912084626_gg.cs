using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestLine",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "RequestLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "RequestLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "End",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "RequestLine");
        }
    }
}
