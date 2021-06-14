using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektSpotkaniaGrupTematycznych.Data.Migrations
{
    public partial class OnlineLink2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OnlineLink",
                table: "Meeting",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OnlineLink",
                table: "Meeting",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
