using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    AchievedPoints = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TakenTimes = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    Difficulty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoreRecordings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreRecordings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionPools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    ParentPoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionPools_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionPools_QuestionPools_ParentPoolId",
                        column: x => x.ParentPoolId,
                        principalTable: "QuestionPools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    DoB = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountAchievements",
                columns: table => new
                {
                    AchievementId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAchievements", x => new { x.AccountId, x.AchievementId });
                    table.ForeignKey(
                        name: "FK_AccountAchievements_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCertifications",
                columns: table => new
                {
                    CertificationId = table.Column<string>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    AchievedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCertifications", x => new { x.AccountId, x.CertificationId });
                    table.ForeignKey(
                        name: "FK_AccountCertifications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountCertifications_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NumOfQuestions = table.Column<int>(nullable: false),
                    PassScore = table.Column<float>(nullable: false),
                    Duration = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    CertificationId = table.Column<string>(nullable: false),
                    FeedbackTypeId = table.Column<int>(nullable: false),
                    FeedbackLevelId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    ScoreRecordingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_FeedbackLevels_FeedbackLevelId",
                        column: x => x.FeedbackLevelId,
                        principalTable: "FeedbackLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_FeedbackTypes_FeedbackTypeId",
                        column: x => x.FeedbackTypeId,
                        principalTable: "FeedbackTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_ScoreRecordings_ScoreRecordingId",
                        column: x => x.ScoreRecordingId,
                        principalTable: "ScoreRecordings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillinCertifications",
                columns: table => new
                {
                    CertificationId = table.Column<string>(nullable: false),
                    SkillId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillinCertifications", x => new { x.SkillId, x.CertificationId });
                    table.ForeignKey(
                        name: "FK_SkillinCertifications_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillinCertifications_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCVs",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    Attachment = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCVs", x => new { x.AccountId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_AccountCVs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountCVs_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(nullable: true),
                    QuestionTypeId = table.Column<int>(nullable: false),
                    SelectionSettingId = table.Column<int>(nullable: false),
                    QuestionPoolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionPools_QuestionPoolId",
                        column: x => x.QuestionPoolId,
                        principalTable: "QuestionPools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_SelectionSettings_SelectionSettingId",
                        column: x => x.SelectionSettingId,
                        principalTable: "SelectionSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvancedFeedbacksInExams",
                columns: table => new
                {
                    AdvancedFeedbackId = table.Column<int>(nullable: false),
                    ExamId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvancedFeedbacksInExams", x => new { x.ExamId, x.AdvancedFeedbackId });
                    table.ForeignKey(
                        name: "FK_AvancedFeedbacksInExams_AdvancedFeedbacks_AdvancedFeedbackId",
                        column: x => x.AdvancedFeedbackId,
                        principalTable: "AdvancedFeedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvancedFeedbacksInExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SequenceNo = table.Column<int>(nullable: false),
                    QuestionPoolId = table.Column<int>(nullable: false),
                    NumOfQuestions = table.Column<int>(nullable: false),
                    QuestionPoints = table.Column<float>(nullable: false),
                    DeductedPoints = table.Column<float>(nullable: false),
                    IsShuffle = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamParts_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamParts_QuestionPools_QuestionPoolId",
                        column: x => x.QuestionPoolId,
                        principalTable: "QuestionPools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamScores",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    ExamId = table.Column<string>(nullable: false),
                    Points = table.Column<double>(nullable: false),
                    OpenedTime = table.Column<DateTime>(nullable: false),
                    ClosedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamScores", x => new { x.ExamId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_ExamScores_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamScores_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<string>(nullable: false),
                    AvailableDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    DisplayPoint = table.Column<bool>(nullable: false),
                    NumOfSubmissions = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    FbDisplayAfter = table.Column<DateTime>(nullable: false),
                    FbDisplayBefore = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSettings_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAttachment_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Choice = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionChoices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointValue = table.Column<float>(nullable: false),
                    DisplayPoint = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CorrectFb = table.Column<string>(nullable: true),
                    InCorrectFb = table.Column<string>(nullable: true),
                    DeductedPoints = table.Column<float>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionSetting_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsInParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: false),
                    ExamPartId = table.Column<int>(nullable: false),
                    SequenceNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsInParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsInParts_ExamParts_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ExamParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionsInParts_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    QuestionsInPartId = table.Column<int>(nullable: false),
                    Points = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamHistories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamHistories_QuestionsInParts_QuestionsInPartId",
                        column: x => x.QuestionsInPartId,
                        principalTable: "QuestionsInParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamHistoryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamHistoryId = table.Column<int>(nullable: false),
                    Choice = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamHistoryDetails_ExamHistories_ExamHistoryId",
                        column: x => x.ExamHistoryId,
                        principalTable: "ExamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAchievements_AchievementId",
                table: "AccountAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCertifications_CertificationId",
                table: "AccountCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCVs_TemplateId",
                table: "AccountCVs",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AvancedFeedbacksInExams_AdvancedFeedbackId",
                table: "AvancedFeedbacksInExams",
                column: "AdvancedFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamHistories_AccountId",
                table: "ExamHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamHistories_QuestionsInPartId",
                table: "ExamHistories",
                column: "QuestionsInPartId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamHistoryDetails_ExamHistoryId",
                table: "ExamHistoryDetails",
                column: "ExamHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamParts_ExamId",
                table: "ExamParts",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamParts_QuestionPoolId",
                table: "ExamParts",
                column: "QuestionPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AccountId",
                table: "Exams",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CertificationId",
                table: "Exams",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_FeedbackLevelId",
                table: "Exams",
                column: "FeedbackLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_FeedbackTypeId",
                table: "Exams",
                column: "FeedbackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ScoreRecordingId",
                table: "Exams",
                column: "ScoreRecordingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamScores_AccountId",
                table: "ExamScores",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSettings_ExamId",
                table: "ExamSettings",
                column: "ExamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttachment_QuestionId",
                table: "QuestionAttachment",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_QuestionId",
                table: "QuestionChoices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPools_AccountId",
                table: "QuestionPools",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPools_ParentPoolId",
                table: "QuestionPools",
                column: "ParentPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionPoolId",
                table: "Questions",
                column: "QuestionPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SelectionSettingId",
                table: "Questions",
                column: "SelectionSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSetting_QuestionId",
                table: "QuestionSetting",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsInParts_QuestionId",
                table: "QuestionsInParts",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillinCertifications_CertificationId",
                table: "SkillinCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAchievements");

            migrationBuilder.DropTable(
                name: "AccountCertifications");

            migrationBuilder.DropTable(
                name: "AccountCVs");

            migrationBuilder.DropTable(
                name: "AvancedFeedbacksInExams");

            migrationBuilder.DropTable(
                name: "ExamHistoryDetails");

            migrationBuilder.DropTable(
                name: "ExamScores");

            migrationBuilder.DropTable(
                name: "ExamSettings");

            migrationBuilder.DropTable(
                name: "QuestionAttachment");

            migrationBuilder.DropTable(
                name: "QuestionChoices");

            migrationBuilder.DropTable(
                name: "QuestionSetting");

            migrationBuilder.DropTable(
                name: "SkillinCertifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "AdvancedFeedbacks");

            migrationBuilder.DropTable(
                name: "ExamHistories");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "QuestionsInParts");

            migrationBuilder.DropTable(
                name: "ExamParts");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "QuestionPools");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "SelectionSettings");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "FeedbackLevels");

            migrationBuilder.DropTable(
                name: "FeedbackTypes");

            migrationBuilder.DropTable(
                name: "ScoreRecordings");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
