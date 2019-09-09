using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class rw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLine");

            migrationBuilder.AddColumn<int>(
                name: "RequestLine_ControllerID",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestLine_FilterID",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestLine_LaboratoryId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestEventId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestLineId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestLine_RequesterEmail",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestLine_SizeID",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RequestEventId",
                table: "Events",
                column: "RequestEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_RequestEventId",
                table: "Events",
                column: "RequestEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_RequestEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RequestEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLine_ControllerID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLine_FilterID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLine_LaboratoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLineId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLine_RequesterEmail",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestLine_SizeID",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "RequestLine",
                columns: table => new
                {
                    RequestLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    End = table.Column<string>(nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    FilterID = table.Column<int>(nullable: false),
                    LaboratoryId = table.Column<int>(nullable: false),
                    RequestEventId = table.Column<int>(nullable: true),
                    RequesterEmail = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: false),
                    Start = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLine", x => x.RequestLineId);
                    table.ForeignKey(
                        name: "FK_RequestLine_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestLine_Events_RequestEventId",
                        column: x => x.RequestEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_EventId",
                table: "RequestLine",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestEventId",
                table: "RequestLine",
                column: "RequestEventId");
        }
    }
}
