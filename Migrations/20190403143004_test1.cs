using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyGroups",
                columns: table => new
                {
                    SurveyId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyGroups", x => new { x.SurveyId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_SurveyGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyGroups_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyGroups_GroupId",
                table: "SurveyGroups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyGroups");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
