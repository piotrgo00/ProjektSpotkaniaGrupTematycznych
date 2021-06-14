using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektSpotkaniaGrupTematycznych.Data.Migrations
{
    public partial class TicketUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OwnerId",
                table: "Ticket",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_OwnerId",
                table: "Ticket",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_OwnerId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_OwnerId",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
