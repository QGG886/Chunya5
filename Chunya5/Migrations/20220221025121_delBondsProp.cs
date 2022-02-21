using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chunya5.Migrations
{
    public partial class delBondsProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowDelete",
                table: "Bonds");

            migrationBuilder.DropColumn(
                name: "AllowEdit",
                table: "Bonds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowDelete",
                table: "Bonds",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowEdit",
                table: "Bonds",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
