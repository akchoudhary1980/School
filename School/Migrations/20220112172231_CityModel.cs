using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class CityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTransModels",
                columns: table => new
                {
                    ActivityTransID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ActivityTransModels", x => x.ActivityTransID);
                });

            migrationBuilder.CreateTable(
                name: "EducationTransModels",
                columns: table => new
                {
                    EducationTransID = table.Column<int>(type: "int", nullable: false),
                    TokenEduID = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecievedMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionYearID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTransModels", x => x.EducationTransID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTransModels");

            migrationBuilder.DropTable(
                name: "EducationTransModels");
        }
    }
}
