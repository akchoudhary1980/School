using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class SectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueOn",
                table: "FeesStructureModels");

            migrationBuilder.DropColumn(
                name: "FeesAmount",
                table: "FeesStructureModels");

            migrationBuilder.DropColumn(
                name: "FeesHeadID",
                table: "FeesStructureModels");

            migrationBuilder.RenameColumn(
                name: "BillingCycle",
                table: "FeesStructureModels",
                newName: "Pictures");

            migrationBuilder.CreateTable(
                name: "FeesStructureTransModels",
                columns: table => new
                {
                    FeesStructureTransID = table.Column<int>(type: "int", nullable: false),
                    FeesHeadID = table.Column<int>(type: "int", nullable: false),
                    FeesAmount = table.Column<double>(type: "float", nullable: false),
                    BillingCycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionYearID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesStructureTransModels", x => x.FeesStructureTransID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeesStructureTransModels");

            migrationBuilder.RenameColumn(
                name: "Pictures",
                table: "FeesStructureModels",
                newName: "BillingCycle");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueOn",
                table: "FeesStructureModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "FeesAmount",
                table: "FeesStructureModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FeesHeadID",
                table: "FeesStructureModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
