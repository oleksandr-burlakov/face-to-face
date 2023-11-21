using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F2F.DLL.Migrations
{
    /// <inheritdoc />
    public partial class Meetingaddpossiblesinglepreferablequestionnaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreferableQuestionnaireId",
                table: "Meetings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PreferableQuestionnaireId",
                table: "Meetings",
                column: "PreferableQuestionnaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Questionnaires_PreferableQuestionnaireId",
                table: "Meetings",
                column: "PreferableQuestionnaireId",
                principalTable: "Questionnaires",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Questionnaires_PreferableQuestionnaireId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_PreferableQuestionnaireId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "PreferableQuestionnaireId",
                table: "Meetings");
        }
    }
}
