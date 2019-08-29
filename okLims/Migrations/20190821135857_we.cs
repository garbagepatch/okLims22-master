using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class we : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "RequestState",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "Laboratory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "FilterType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "FilterSize",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "ControllerType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestFK",
                table: "Request",
                column: "RequestFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_RequestState_RequestFK",
                table: "Request",
                column: "RequestFK",
                principalTable: "RequestState",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_RequestState_RequestFK",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_RequestFK",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "RequestState");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "Laboratory");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "FilterType");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "FilterSize");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "ControllerType");
        }
    }
}
