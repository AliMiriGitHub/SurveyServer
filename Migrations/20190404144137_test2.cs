using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Surveys",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurveyAnswerTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SurveyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswerTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswerTemplates_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_TypeId",
                table: "Surveys",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswerTemplates_SurveyId",
                table: "SurveyAnswerTemplates",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys",
                column: "TypeId",
                principalTable: "SurveyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyAnswerTemplates");

            migrationBuilder.DropTable(
                name: "SurveyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_TypeId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Surveys");
        }
    }
}
