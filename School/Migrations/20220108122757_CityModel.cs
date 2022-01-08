using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class CityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeesHead",
                table: "FeesStructureTransModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeesHead",
                table: "FeesStructureTransModels");
        }
    }
}
