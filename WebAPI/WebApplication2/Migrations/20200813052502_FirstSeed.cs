using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class FirstSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[QuestionTypes] ([Name]) VALUES (N'Multiple Choice')
                INSERT INTO [dbo].[QuestionTypes] ([Name]) VALUES (N'True/False')");
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[ScoreRecordings] ([Name]) VALUES (N'Highest score')
                INSERT INTO [dbo].[ScoreRecordings] ([Name]) VALUES (N'Last score')");
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[FeedbackTypes] ([Name]) VALUES (N'No Feedback will be displayed to the student')
                INSERT INTO [dbo].[FeedbackTypes] ([Name]) VALUES (N'Feedback on submission')
                INSERT INTO [dbo].[FeedbackTypes] ([Name]) VALUES (N'Feedback will be displayed to the student on specific dates')");
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[FeedbackLevels] ([Name]) VALUES (N'Question-Level Feedback')
                INSERT INTO [dbo].[FeedbackLevels] ([Name]) VALUES (N'Selection-Level (A,B,C...) Feedback')
                INSERT INTO [dbo].[FeedbackLevels] ([Name]) VALUES (N'Both')");
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AdvancedFeedbacks] ([Name]) VALUES (N'Only Release Student''s Assessment Scores (questions not shown)')
                INSERT INTO [dbo].[AdvancedFeedbacks] ([Name]) VALUES (N'Student Response')
                INSERT INTO [dbo].[AdvancedFeedbacks] ([Name]) VALUES (N'Question-Level Feedback')
                INSERT INTO [dbo].[AdvancedFeedbacks] ([Name]) VALUES (N'Selection-Level Feedback')
                INSERT INTO [dbo].[AdvancedFeedbacks] ([Name]) VALUES (N'Student''s Question and Part Scores')");
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[SelectionSettings] ([Name]) VALUES (N'Single Correct')
                INSERT INTO [dbo].[SelectionSettings] ([Name]) VALUES (N'Multiple Correct, Single Selection')
                INSERT INTO [dbo].[SelectionSettings] ([Name]) VALUES (N'Multiple Correct, Multiple Selection')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
