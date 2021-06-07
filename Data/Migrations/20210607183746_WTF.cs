using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektSpotkaniaGrupTematycznych.Data.Migrations
{
    public partial class WTF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Category_GroupCategoryId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "GroupCategoryId",
                table: "Group",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Category_GroupCategoryId",
                table: "Group",
                column: "GroupCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Category_GroupCategoryId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "GroupCategoryId",
                table: "Group",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Category_GroupCategoryId",
                table: "Group",
                column: "GroupCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
