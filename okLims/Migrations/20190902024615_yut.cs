using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class yut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Request_EventId",
                table: "RequestLine");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "RequestState");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "Laboratory");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "FilterType");

            migrationBuilder.DropColumn(
                name: "RequestFK",
                table: "FilterSize");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "RequestLine",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestLine_EventId",
                table: "RequestLine",
                newName: "IX_RequestLine_EventId");

            migrationBuilder.AddColumn<int>(
                name: "RequestEventId",
                table: "RequestLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ControllerID",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilterID",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequesterEmail",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeID",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestEventId",
                table: "RequestLine",
                column: "RequestEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ControllerID",
                table: "Events",
                column: "ControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_FilterID",
                table: "Events",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LaboratoryId",
                table: "Events",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SizeID",
                table: "Events",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StateId",
                table: "Events",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ControllerType_ControllerID",
                table: "Events",
                column: "ControllerID",
                principalTable: "ControllerType",
                principalColumn: "ControllerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FilterType_FilterID",
                table: "Events",
                column: "FilterID",
                principalTable: "FilterType",
                principalColumn: "FilterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Laboratory_LaboratoryId",
                table: "Events",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FilterSize_SizeID",
                table: "Events",
                column: "SizeID",
                principalTable: "FilterSize",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_RequestState_StateId",
                table: "Events",
                column: "StateId",
                principalTable: "RequestState",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Events_EventId",
                table: "RequestLine",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Events_RequestEventId",
                table: "RequestLine",
                column: "RequestEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ControllerType_ControllerID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_FilterType_FilterID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Laboratory_LaboratoryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_FilterSize_SizeID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_RequestState_StateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Events_EventId",
                table: "RequestLine");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Events_RequestEventId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestEventId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_Events_ControllerID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_FilterID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LaboratoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_SizeID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestEventId",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ControllerID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FilterID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequesterEmail",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SizeID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "RequestLine",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestLine_EventId",
                table: "RequestLine",
                newName: "IX_RequestLine_EventId");

            migrationBuilder.AddColumn<int>(
                name: "RequestFK",
                table: "RequestState",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerID = table.Column<int>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    FilterID = table.Column<int>(nullable: false),
                    LaboratoryId = table.Column<int>(nullable: false),
                    RequestFK = table.Column<int>(nullable: true),
                    RequesterEmail = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Request_ControllerType_ControllerID",
                        column: x => x.ControllerID,
                        principalTable: "ControllerType",
                        principalColumn: "ControllerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_FilterType_FilterID",
                        column: x => x.FilterID,
                        principalTable: "FilterType",
                        principalColumn: "FilterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Laboratory_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratory",
                        principalColumn: "LaboratoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_RequestState_RequestFK",
                        column: x => x.RequestFK,
                        principalTable: "RequestState",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_FilterSize_SizeID",
                        column: x => x.SizeID,
                        principalTable: "FilterSize",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_RequestState_StateId",
                        column: x => x.StateId,
                        principalTable: "RequestState",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_ControllerID",
                table: "Request",
                column: "ControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_FilterID",
                table: "Request",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_LaboratoryId",
                table: "Request",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestFK",
                table: "Request",
                column: "RequestFK");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SizeID",
                table: "Request",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_StateId",
                table: "Request",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Request_EventId",
                table: "RequestLine",
                column: "EventId",
                principalTable: "Request",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
