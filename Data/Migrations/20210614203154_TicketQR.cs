using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektSpotkaniaGrupTematycznych.Data.Migrations
{
    public partial class TicketQR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QR",
                table: "Ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QR",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
