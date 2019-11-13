using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalCards.Data.Migrations
{
    public partial class settingupcascadedeleteoncard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionPoint_Cards_CardId",
                table: "ActionPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_Link_Cards_CardId",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_NPPoint_Cards_CardId",
                table: "NPPoint");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionPoint_Cards_CardId",
                table: "ActionPoint",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Cards_CardId",
                table: "Link",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPPoint_Cards_CardId",
                table: "NPPoint",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionPoint_Cards_CardId",
                table: "ActionPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_Link_Cards_CardId",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_NPPoint_Cards_CardId",
                table: "NPPoint");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionPoint_Cards_CardId",
                table: "ActionPoint",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Cards_CardId",
                table: "Link",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPPoint_Cards_CardId",
                table: "NPPoint",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
