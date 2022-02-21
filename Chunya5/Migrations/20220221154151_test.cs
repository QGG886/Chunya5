using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chunya5.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "a",
                table: "AssessmentPraces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "a",
                table: "AssessmentPraces");
        }
    }
}
