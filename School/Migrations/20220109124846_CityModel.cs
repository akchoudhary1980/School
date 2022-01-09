using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class CityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeesStructureID",
                table: "FeesStructureTransTempModels",
                newName: "TokenID");

            migrationBuilder.RenameColumn(
                name: "FeesStructureID",
                table: "FeesStructureTransModels",
                newName: "TokenID");

            migrationBuilder.AddColumn<int>(
                name: "TokenID",
                table: "FeesStructureModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenID",
                table: "FeesStructureModels");

            migrationBuilder.RenameColumn(
                name: "TokenID",
                table: "FeesStructureTransTempModels",
                newName: "FeesStructureID");

            migrationBuilder.RenameColumn(
                name: "TokenID",
                table: "FeesStructureTransModels",
                newName: "FeesStructureID");
        }
    }
}
