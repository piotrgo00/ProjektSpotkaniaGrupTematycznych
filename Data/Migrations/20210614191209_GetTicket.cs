using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektSpotkaniaGrupTematycznych.Data.Migrations
{
    public partial class GetTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Ticket",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Ticket");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
