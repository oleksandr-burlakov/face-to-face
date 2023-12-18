using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F2F.DLL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetingParticipantinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipants_AspNetUsers_ParticipantId",
                table: "MeetingParticipants");

            migrationBuilder.DropIndex(
                name: "IX_MeetingParticipants_ParticipantId",
                table: "MeetingParticipants");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MeetingParticipants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MeetingParticipants");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipants_ParticipantId",
                table: "MeetingParticipants",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipants_AspNetUsers_ParticipantId",
                table: "MeetingParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
