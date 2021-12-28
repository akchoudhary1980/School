using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class FeesHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "UserModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "TeacherModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "SubJectTransModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "SubjectModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "StaffModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "SessionYearModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "SessionYearModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "SectionModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "QualificationTransModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "QualificationModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "DesginationModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "ClassModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearID",
                table: "BoardModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeesHeadModels",
                columns: table => new
                {
                    FeesHeadID = table.Column<int>(type: "int", nullable: false),
                    FeesHeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeesHeadType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionYearID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesHeadModels", x => x.FeesHeadID);
                });

            migrationBuilder.CreateTable(
                name: "FeesStructureModels",
                columns: table => new
                {
                    FeesStructureID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    FeesHeadID = table.Column<int>(type: "int", nullable: false),
                    FeesAmount = table.Column<double>(type: "float", nullable: false),
                    BillingCycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionYearID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesStructureModels", x => x.FeesStructureID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeesHeadModels");

            migrationBuilder.DropTable(
                name: "FeesStructureModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "TeacherModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "SubJectTransModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "SubjectModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "StaffModels");

            migrationBuilder.DropColumn(
                name: "End",
                table: "SessionYearModels");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "SessionYearModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "SectionModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "QualificationTransModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "QualificationModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "DesginationModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "ClassModels");

            migrationBuilder.DropColumn(
                name: "SessionYearID",
                table: "BoardModels");
        }
    }
}
