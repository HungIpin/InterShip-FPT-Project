using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class AddIdToExamScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamScores");

            migrationBuilder.CreateTable(
                name: "ExamScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    ExamId = table.Column<string>(nullable: false),
                    Points = table.Column<double>(nullable: false),
                    OpenedTime = table.Column<DateTime>(nullable: false),
                    ClosedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamScores", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_ExamScores_AccountId",
                table: "ExamScores",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamScores_ExamId",
                table: "ExamScores",
                column: "ExamId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ExamScores");

        }
    }
}
