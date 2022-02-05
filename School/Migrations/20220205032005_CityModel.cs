using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class CityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentFor",
                table: "PaymentModels");

            migrationBuilder.DropColumn(
                name: "PaymentGroup",
                table: "PaymentModels");

            migrationBuilder.AddColumn<int>(
                name: "FeesHeadID",
                table: "PaymentModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActivityTransTempModels",
                columns: table => new
                {
                    ActivityTransTempID = table.Column<int>(type: "int", nullable: false),
                    TokenActID = table.Column<int>(type: "int", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnyAward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionYearID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTransTempModels", x => x.ActivityTransTempID);
                });

            migrationBuilder.CreateTable(
                name: "EducationTransTempModels",
                columns: table => new
                {
                    EducationTransTempID = table.Column<int>(type: "int", nullable: false),
                    TokenEduID = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecievedMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionYearID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTransTempModels", x => x.EducationTransTempID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTransTempModels");

            migrationBuilder.DropTable(
                name: "EducationTransTempModels");

            migrationBuilder.DropColumn(
                name: "FeesHeadID",
                table: "PaymentModels");

            migrationBuilder.AddColumn<string>(
                name: "PaymentFor",
                table: "PaymentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGroup",
                table: "PaymentModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
