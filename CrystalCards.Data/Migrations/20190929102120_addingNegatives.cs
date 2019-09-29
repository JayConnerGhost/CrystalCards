using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalCards.Data.Migrations
{
    public partial class addingNegatives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId1",
                table: "NPPoint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NPPoint_CardId1",
                table: "NPPoint",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NPPoint_Cards_CardId1",
                table: "NPPoint",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NPPoint_Cards_CardId1",
                table: "NPPoint");

            migrationBuilder.DropIndex(
                name: "IX_NPPoint_CardId1",
                table: "NPPoint");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "NPPoint");
        }
    }
}
