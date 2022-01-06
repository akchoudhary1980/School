using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class StateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeesStructureTransTempModels",
                columns: table => new
                {
                    FeesStructureTransTempID = table.Column<int>(type: "int", nullable: false),
                    Tokon = table.Column<int>(type: "int", nullable: false),
                    FeesHeadID = table.Column<int>(type: "int", nullable: false),
                    FeesHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeesAmount = table.Column<double>(type: "float", nullable: false),
                    BillingCycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    SessionYearID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesStructureTransTempModels", x => x.FeesStructureTransTempID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeesStructureTransTempModels");
        }
    }
}
