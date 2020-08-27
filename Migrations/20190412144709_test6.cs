using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswerTemplates_Surveys_SurveyId",
                table: "SurveyAnswerTemplates");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswerTemplates_Surveys_SurveyId",
                table: "SurveyAnswerTemplates",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswerTemplates_Surveys_SurveyId",
                table: "SurveyAnswerTemplates");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswerTemplates_Surveys_SurveyId",
                table: "SurveyAnswerTemplates",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
