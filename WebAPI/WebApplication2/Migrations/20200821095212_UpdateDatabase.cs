using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsInParts_ExamParts_QuestionId",
                table: "QuestionsInParts");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionPoolId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionPoolId",
                table: "ExamParts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsInParts_ExamPartId",
                table: "QuestionsInParts",
                column: "ExamPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsInParts_ExamParts_ExamPartId",
                table: "QuestionsInParts",
                column: "ExamPartId",
                principalTable: "ExamParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsInParts_ExamParts_ExamPartId",
                table: "QuestionsInParts");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsInParts_ExamPartId",
                table: "QuestionsInParts");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionPoolId",
                table: "Questions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionPoolId",
                table: "ExamParts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsInParts_ExamParts_QuestionId",
                table: "QuestionsInParts",
                column: "QuestionId",
                principalTable: "ExamParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
