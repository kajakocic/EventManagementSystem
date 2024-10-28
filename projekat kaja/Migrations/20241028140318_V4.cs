using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekat_kaja.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVENT_LOKACIJE_LocationEventID",
                table: "EVENT");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRACIJA_EVENT_EventsUsersID",
                table: "REGISTRACIJA");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRACIJA_USER_UsersEventsID",
                table: "REGISTRACIJA");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_EVENT_EventReviewID",
                table: "REVIEW");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_USER_UserReviewID",
                table: "REVIEW");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOKACIJE",
                table: "LOKACIJE");

            migrationBuilder.DropColumn(
                name: "Vreme",
                table: "EVENT");

            migrationBuilder.RenameTable(
                name: "LOKACIJE",
                newName: "LOKACIJA");

            migrationBuilder.RenameColumn(
                name: "UserReviewID",
                table: "REVIEW",
                newName: "UserEventID");

            migrationBuilder.RenameColumn(
                name: "EventReviewID",
                table: "REVIEW",
                newName: "EventUserID");

            migrationBuilder.RenameIndex(
                name: "IX_REVIEW_UserReviewID",
                table: "REVIEW",
                newName: "IX_REVIEW_UserEventID");

            migrationBuilder.RenameIndex(
                name: "IX_REVIEW_EventReviewID",
                table: "REVIEW",
                newName: "IX_REVIEW_EventUserID");

            migrationBuilder.RenameColumn(
                name: "UsersEventsID",
                table: "REGISTRACIJA",
                newName: "UserEventID");

            migrationBuilder.RenameColumn(
                name: "EventsUsersID",
                table: "REGISTRACIJA",
                newName: "EventUserID");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRACIJA_UsersEventsID",
                table: "REGISTRACIJA",
                newName: "IX_REGISTRACIJA_UserEventID");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRACIJA_EventsUsersID",
                table: "REGISTRACIJA",
                newName: "IX_REGISTRACIJA_EventUserID");

            migrationBuilder.RenameColumn(
                name: "LocationEventID",
                table: "EVENT",
                newName: "LokacijaEventID");

            migrationBuilder.RenameIndex(
                name: "IX_EVENT_LocationEventID",
                table: "EVENT",
                newName: "IX_EVENT_LokacijaEventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOKACIJA",
                table: "LOKACIJA",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EVENT_LOKACIJA_LokacijaEventID",
                table: "EVENT",
                column: "LokacijaEventID",
                principalTable: "LOKACIJA",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRACIJA_EVENT_EventUserID",
                table: "REGISTRACIJA",
                column: "EventUserID",
                principalTable: "EVENT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRACIJA_USER_UserEventID",
                table: "REGISTRACIJA",
                column: "UserEventID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_EVENT_EventUserID",
                table: "REVIEW",
                column: "EventUserID",
                principalTable: "EVENT",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_USER_UserEventID",
                table: "REVIEW",
                column: "UserEventID",
                principalTable: "USER",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVENT_LOKACIJA_LokacijaEventID",
                table: "EVENT");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRACIJA_EVENT_EventUserID",
                table: "REGISTRACIJA");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRACIJA_USER_UserEventID",
                table: "REGISTRACIJA");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_EVENT_EventUserID",
                table: "REVIEW");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_USER_UserEventID",
                table: "REVIEW");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOKACIJA",
                table: "LOKACIJA");

            migrationBuilder.RenameTable(
                name: "LOKACIJA",
                newName: "LOKACIJE");

            migrationBuilder.RenameColumn(
                name: "UserEventID",
                table: "REVIEW",
                newName: "UserReviewID");

            migrationBuilder.RenameColumn(
                name: "EventUserID",
                table: "REVIEW",
                newName: "EventReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_REVIEW_UserEventID",
                table: "REVIEW",
                newName: "IX_REVIEW_UserReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_REVIEW_EventUserID",
                table: "REVIEW",
                newName: "IX_REVIEW_EventReviewID");

            migrationBuilder.RenameColumn(
                name: "UserEventID",
                table: "REGISTRACIJA",
                newName: "UsersEventsID");

            migrationBuilder.RenameColumn(
                name: "EventUserID",
                table: "REGISTRACIJA",
                newName: "EventsUsersID");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRACIJA_UserEventID",
                table: "REGISTRACIJA",
                newName: "IX_REGISTRACIJA_UsersEventsID");

            migrationBuilder.RenameIndex(
                name: "IX_REGISTRACIJA_EventUserID",
                table: "REGISTRACIJA",
                newName: "IX_REGISTRACIJA_EventsUsersID");

            migrationBuilder.RenameColumn(
                name: "LokacijaEventID",
                table: "EVENT",
                newName: "LocationEventID");

            migrationBuilder.RenameIndex(
                name: "IX_EVENT_LokacijaEventID",
                table: "EVENT",
                newName: "IX_EVENT_LocationEventID");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Vreme",
                table: "EVENT",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOKACIJE",
                table: "LOKACIJE",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EVENT_LOKACIJE_LocationEventID",
                table: "EVENT",
                column: "LocationEventID",
                principalTable: "LOKACIJE",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRACIJA_EVENT_EventsUsersID",
                table: "REGISTRACIJA",
                column: "EventsUsersID",
                principalTable: "EVENT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRACIJA_USER_UsersEventsID",
                table: "REGISTRACIJA",
                column: "UsersEventsID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_EVENT_EventReviewID",
                table: "REVIEW",
                column: "EventReviewID",
                principalTable: "EVENT",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_USER_UserReviewID",
                table: "REVIEW",
                column: "UserReviewID",
                principalTable: "USER",
                principalColumn: "ID");
        }
    }
}
