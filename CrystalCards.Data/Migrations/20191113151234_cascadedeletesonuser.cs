using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalCards.Data.Migrations
{
    public partial class cascadedeletesonuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "CustomRole",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomRole_UserId1",
                table: "CustomRole",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomRole_Users_UserId1",
                table: "CustomRole",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomRole_Users_UserId1",
                table: "CustomRole");

            migrationBuilder.DropIndex(
                name: "IX_CustomRole_UserId1",
                table: "CustomRole");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CustomRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
