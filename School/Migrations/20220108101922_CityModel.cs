using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class CityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Token",
                table: "FeesStructureModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "FeesStructureModels");
        }
    }
}
