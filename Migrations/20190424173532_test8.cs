using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations
{
    public partial class test8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Surveys",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Surveys",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
