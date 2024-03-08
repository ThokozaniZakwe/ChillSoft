using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chillisoft.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetingItemToMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeetingItemId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "MeetingItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingItems_MeetingId",
                table: "MeetingItems",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingItems_Meetings_MeetingId",
                table: "MeetingItems",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingItems_Meetings_MeetingId",
                table: "MeetingItems");

            migrationBuilder.DropIndex(
                name: "IX_MeetingItems_MeetingId",
                table: "MeetingItems");

            migrationBuilder.DropColumn(
                name: "MeetingItemId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "MeetingItems");
        }
    }
}
