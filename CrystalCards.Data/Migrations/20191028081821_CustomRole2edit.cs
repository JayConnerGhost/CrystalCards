using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalCards.Data.Migrations
{
    public partial class CustomRole2edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomRole",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "CustomRole",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
