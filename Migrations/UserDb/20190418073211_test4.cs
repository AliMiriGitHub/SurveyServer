using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations.UserDb
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId");
        }
    }
}
