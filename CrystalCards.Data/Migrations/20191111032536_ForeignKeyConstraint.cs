using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalCards.Data.Migrations
{
    public partial class ForeignKeyConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Projects_UserProjectId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "UserProjectId",
                table: "Cards",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_UserProjectId",
                table: "Cards",
                newName: "IX_Cards_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Projects_ProjectId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Cards",
                newName: "UserProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_ProjectId",
                table: "Cards",
                newName: "IX_Cards_UserProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Projects_UserProjectId",
                table: "Cards",
                column: "UserProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
