using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Surveys",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys",
                column: "TypeId",
                principalTable: "SurveyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeId",
                table: "Surveys",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyTypes_TypeId",
                table: "Surveys",
                column: "TypeId",
                principalTable: "SurveyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
