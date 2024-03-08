using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chillisoft.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentOnMeetingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "MeetingItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "MeetingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "MeetingItems");

            migrationBuilder.AddColumn<int>(
                name: "MeetingItemId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "MeetingItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
