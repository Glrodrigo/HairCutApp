using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairCut.Tools.Repository.Migrations
{
    public partial class UpdateResetCodeUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordCreateDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordCreateDate",
                table: "Users");
        }
    }
}
