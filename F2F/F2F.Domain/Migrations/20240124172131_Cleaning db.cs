using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F2F.DLL.Migrations
{
    /// <inheritdoc />
    public partial class Cleaningdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingMessages");

            migrationBuilder.DropTable(
                name: "MeetingQuestionPoints");

            migrationBuilder.DropTable(
                name: "OneWayAnswers");

            migrationBuilder.DropTable(
                name: "SuspendedBehaviours");

            migrationBuilder.DropTable(
                name: "TestUserAnswers");

            migrationBuilder.DropTable(
                name: "OneWayAttempts");

            migrationBuilder.DropTable(
                name: "OneWayQuestions");

            migrationBuilder.DropTable(
                name: "TestAnswers");

            migrationBuilder.DropTable(
                name: "TestAttempts");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "AssessmentApplies");

            migrationBuilder.DropTable(
                name: "TestSections");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "OneWays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingMessages_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingMessages_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingQuestionPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TimeOfAnswer = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingQuestionPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingQuestionPoints_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingQuestionPoints_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneWays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneWays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneWays_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuspendedBehaviours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehiviourType = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuspendedBehaviours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuspendedBehaviours_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneWayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PreferableQuestionnaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_OneWays_OneWayId",
                        column: x => x.OneWayId,
                        principalTable: "OneWays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessments_Questionnaires_PreferableQuestionnaireId",
                        column: x => x.PreferableQuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OneWayQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneWayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfAttempts = table.Column<int>(type: "int", nullable: false),
                    TimeForAnswer = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneWayQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneWayQuestions_OneWays_OneWayId",
                        column: x => x.OneWayId,
                        principalTable: "OneWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedOneWayEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllowedTestEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentApplies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentApplies_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentApplies_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedToSeeAnswersBeforeReview = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfAllowedAttempts = table.Column<int>(type: "int", nullable: false),
                    NumberOfItemPerPage = table.Column<int>(type: "int", nullable: true),
                    ShowByType = table.Column<int>(type: "int", nullable: false),
                    TimeBoundaries = table.Column<TimeSpan>(type: "time", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tests_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneWayAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneWayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneWayAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneWayAttempts_AssessmentApplies_AssessmentApplyId",
                        column: x => x.AssessmentApplyId,
                        principalTable: "AssessmentApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OneWayAttempts_OneWays_OneWayId",
                        column: x => x.OneWayId,
                        principalTable: "OneWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentApplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAttempts_AssessmentApplies_AssessmentApplyId",
                        column: x => x.AssessmentApplyId,
                        principalTable: "AssessmentApplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAttempts_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSections_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneWayAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneWayAttemptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneWayQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneWayAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneWayAnswers_OneWayAttempts_OneWayAttemptId",
                        column: x => x.OneWayAttemptId,
                        principalTable: "OneWayAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OneWayAnswers_OneWayQuestions_OneWayQuestionId",
                        column: x => x.OneWayQuestionId,
                        principalTable: "OneWayQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPoints = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestions_TestSections_TestSectionId",
                        column: x => x.TestSectionId,
                        principalTable: "TestSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TestQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswers_TestQuestions_TestQuestionId",
                        column: x => x.TestQuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestUserAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TestAttemptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManualAnswer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestUserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestUserAnswers_TestAnswers_TestAnswerId",
                        column: x => x.TestAnswerId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestUserAnswers_TestAttempts_TestAttemptId",
                        column: x => x.TestAttemptId,
                        principalTable: "TestAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentApplies_AssessmentId",
                table: "AssessmentApplies",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentApplies_MeetingId",
                table: "AssessmentApplies",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentApplies_UserId",
                table: "AssessmentApplies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AuthorId",
                table: "Assessments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_OneWayId",
                table: "Assessments",
                column: "OneWayId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_PreferableQuestionnaireId",
                table: "Assessments",
                column: "PreferableQuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMessages_AuthorId",
                table: "MeetingMessages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMessages_MeetingId",
                table: "MeetingMessages",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingQuestionPoints_MeetingId",
                table: "MeetingQuestionPoints",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingQuestionPoints_QuestionId",
                table: "MeetingQuestionPoints",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWayAnswers_OneWayAttemptId",
                table: "OneWayAnswers",
                column: "OneWayAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWayAnswers_OneWayQuestionId",
                table: "OneWayAnswers",
                column: "OneWayQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWayAttempts_AssessmentApplyId",
                table: "OneWayAttempts",
                column: "AssessmentApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWayAttempts_OneWayId",
                table: "OneWayAttempts",
                column: "OneWayId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWayQuestions_OneWayId",
                table: "OneWayQuestions",
                column: "OneWayId");

            migrationBuilder.CreateIndex(
                name: "IX_OneWays_AuthorId",
                table: "OneWays",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_SuspendedBehaviours_MeetingId",
                table: "SuspendedBehaviours",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_QuestionId",
                table: "TestAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestQuestionId",
                table: "TestAnswers",
                column: "TestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_AssessmentApplyId",
                table: "TestAttempts",
                column: "AssessmentApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAttempts_TestId",
                table: "TestAttempts",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_TestSectionId",
                table: "TestQuestions",
                column: "TestSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_AssessmentId",
                table: "Tests",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_AuthorId",
                table: "Tests",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSections_TestId",
                table: "TestSections",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestUserAnswers_TestAnswerId",
                table: "TestUserAnswers",
                column: "TestAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestUserAnswers_TestAttemptId",
                table: "TestUserAnswers",
                column: "TestAttemptId");
        }
    }
}
