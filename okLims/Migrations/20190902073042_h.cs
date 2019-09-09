using Microsoft.EntityFrameworkCore.Migrations;

namespace okLims.Migrations
{
    public partial class h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Events_Events_RequestEventId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Events_RequestEventId",
                table: "Event",
                newName: "IX_Event_RequestEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StateId",
                table: "Event",
                newName: "IX_Event_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_SizeID",
                table: "Event",
                newName: "IX_Event_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LaboratoryId",
                table: "Event",
                newName: "IX_Event_LaboratoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_FilterID",
                table: "Event",
                newName: "IX_Event_FilterID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ControllerID",
                table: "Event",
                newName: "IX_Event_ControllerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_ControllerType_ControllerID",
                table: "Event",
                column: "ControllerID",
                principalTable: "ControllerType",
                principalColumn: "ControllerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_FilterType_FilterID",
                table: "Event",
                column: "FilterID",
                principalTable: "FilterType",
                principalColumn: "FilterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Laboratory_LaboratoryId",
                table: "Event",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "LaboratoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_FilterSize_SizeID",
                table: "Event",
                column: "SizeID",
                principalTable: "FilterSize",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_RequestState_StateId",
                table: "Event",
                column: "StateId",
                principalTable: "RequestState",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Event_RequestEventId",
                table: "Event",
                column: "RequestEventId",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_ControllerType_ControllerID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_FilterType_FilterID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Laboratory_LaboratoryId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_FilterSize_SizeID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_RequestState_StateId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Event_RequestEventId",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Event_RequestEventId",
                table: "Events",
                newName: "IX_Events_RequestEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_StateId",
                table: "Events",
                newName: "IX_Events_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_SizeID",
                table: "Events",
                newName: "IX_Events_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_LaboratoryId",
                table: "Events",
                newName: "IX_Events_LaboratoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_FilterID",
                table: "Events",
                newName: "IX_Events_FilterID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ControllerID",
                table: "Events",
                newName: "IX_Events_ControllerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "EventId");

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
                name: "FK_Events_Events_RequestEventId",
                table: "Events",
                column: "RequestEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
