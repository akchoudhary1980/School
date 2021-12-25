using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class ClsssModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionModel",
                table: "SectionModel");

            migrationBuilder.RenameTable(
                name: "SectionModel",
                newName: "SectionModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionModels",
                table: "SectionModels",
                column: "SectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionModels",
                table: "SectionModels");

            migrationBuilder.RenameTable(
                name: "SectionModels",
                newName: "SectionModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionModel",
                table: "SectionModel",
                column: "SectionID");
        }
    }
}
