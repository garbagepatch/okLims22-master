using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class jj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Request");

            migrationBuilder.CreateTable(
                name: "RequestState",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestState", x => x.StateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_StateId",
                table: "Request",
                column: "StateId");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_RequestState_StateId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "RequestState");

            migrationBuilder.DropIndex(
                name: "IX_Request_StateId",
                table: "Request");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Request",
                nullable: false,
                defaultValue: 0);

          
        }
    }
}
