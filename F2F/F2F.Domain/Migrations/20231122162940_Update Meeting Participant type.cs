using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F2F.DLL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetingParticipanttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "MeetingParticipants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ParticipantId",
                table: "MeetingParticipants",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
