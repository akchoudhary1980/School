using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class SubjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "TeacherModels");

            migrationBuilder.RenameColumn(
                name: "TeacherDesignation",
                table: "TeacherModels",
                newName: "ScanDocuments");

            migrationBuilder.RenameColumn(
                name: "Subjects",
                table: "TeacherModels",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "StaffModels",
                newName: "ScanDocuments");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "StaffModels",
                newName: "Picture");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "TeacherModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesginationID",
                table: "TeacherModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "StaffModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesginationID",
                table: "StaffModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QualificationModels",
                columns: table => new
                {
                    QualificationID = table.Column<int>(type: "int", nullable: false),
                    QualificationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationModels", x => x.QualificationID);
                });

            migrationBuilder.CreateTable(
                name: "QualificationTransModels",
                columns: table => new
                {
                    QualificationTransID = table.Column<int>(type: "int", nullable: false),
                    QualificationFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationID = table.Column<int>(type: "int", nullable: false),
                    PassingYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardID = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percent = table.Column<double>(type: "float", nullable: false),
                    RecieptMark = table.Column<double>(type: "float", nullable: false),
                    TotalMark = table.Column<double>(type: "float", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    StaffID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationTransModels", x => x.QualificationTransID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectModels",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectModels", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "SubJectTransModels",
                columns: table => new
                {
                    SubJectTransID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubJectTransModels", x => x.SubJectTransID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualificationModels");

            migrationBuilder.DropTable(
                name: "QualificationTransModels");

            migrationBuilder.DropTable(
                name: "SubjectModels");

            migrationBuilder.DropTable(
                name: "SubJectTransModels");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "TeacherModels");

            migrationBuilder.DropColumn(
                name: "DesginationID",
                table: "TeacherModels");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "StaffModels");

            migrationBuilder.DropColumn(
                name: "DesginationID",
                table: "StaffModels");

            migrationBuilder.RenameColumn(
                name: "ScanDocuments",
                table: "TeacherModels",
                newName: "TeacherDesignation");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "TeacherModels",
                newName: "Subjects");

            migrationBuilder.RenameColumn(
                name: "ScanDocuments",
                table: "StaffModels",
                newName: "Qualification");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "StaffModels",
                newName: "Designation");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "TeacherModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
