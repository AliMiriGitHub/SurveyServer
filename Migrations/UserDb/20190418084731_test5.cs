using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveySystem.Migrations.UserDb
{
    public partial class test5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
