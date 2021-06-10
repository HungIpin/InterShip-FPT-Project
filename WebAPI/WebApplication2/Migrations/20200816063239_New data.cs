using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class Newdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvancedFeedbacksInExams_AdvancedFeedbacks_AdvancedFeedbackId",
                table: "AvancedFeedbacksInExams");

            migrationBuilder.DropForeignKey(
                name: "FK_AvancedFeedbacksInExams_Exams_ExamId",
                table: "AvancedFeedbacksInExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvancedFeedbacksInExams",
                table: "AvancedFeedbacksInExams");

            migrationBuilder.RenameTable(
                name: "AvancedFeedbacksInExams",
                newName: "AdvancedFeedbacksInExams");

            migrationBuilder.RenameIndex(
                name: "IX_AvancedFeedbacksInExams_AdvancedFeedbackId",
                table: "AdvancedFeedbacksInExams",
                newName: "IX_AdvancedFeedbacksInExams_AdvancedFeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvancedFeedbacksInExams",
                table: "AdvancedFeedbacksInExams",
                columns: new[] { "ExamId", "AdvancedFeedbackId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AdvancedFeedbacksInExams_AdvancedFeedbacks_AdvancedFeedbackId",
                table: "AdvancedFeedbacksInExams",
                column: "AdvancedFeedbackId",
                principalTable: "AdvancedFeedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvancedFeedbacksInExams_Exams_ExamId",
                table: "AdvancedFeedbacksInExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvancedFeedbacksInExams_AdvancedFeedbacks_AdvancedFeedbackId",
                table: "AdvancedFeedbacksInExams");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvancedFeedbacksInExams_Exams_ExamId",
                table: "AdvancedFeedbacksInExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvancedFeedbacksInExams",
                table: "AdvancedFeedbacksInExams");

            migrationBuilder.RenameTable(
                name: "AdvancedFeedbacksInExams",
                newName: "AvancedFeedbacksInExams");

            migrationBuilder.RenameIndex(
                name: "IX_AdvancedFeedbacksInExams_AdvancedFeedbackId",
                table: "AvancedFeedbacksInExams",
                newName: "IX_AvancedFeedbacksInExams_AdvancedFeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvancedFeedbacksInExams",
                table: "AvancedFeedbacksInExams",
                columns: new[] { "ExamId", "AdvancedFeedbackId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AvancedFeedbacksInExams_AdvancedFeedbacks_AdvancedFeedbackId",
                table: "AvancedFeedbacksInExams",
                column: "AdvancedFeedbackId",
                principalTable: "AdvancedFeedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AvancedFeedbacksInExams_Exams_ExamId",
                table: "AvancedFeedbacksInExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
