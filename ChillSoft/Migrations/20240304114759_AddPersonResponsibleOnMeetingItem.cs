using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChillSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonResponsibleOnMeetingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonResponsible",
                table: "MeetingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonResponsible",
                table: "MeetingItems");
        }
    }
}
