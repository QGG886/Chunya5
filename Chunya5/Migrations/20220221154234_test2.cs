using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chunya5.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "a",
                table: "AssessmentPraces");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "a",
                table: "AssessmentPraces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
