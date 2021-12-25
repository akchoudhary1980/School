using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class SectonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesginationModels",
                columns: table => new
                {
                    DesginationID = table.Column<int>(type: "int", nullable: false),
                    DesginationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesginationModels", x => x.DesginationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesginationModels");
        }
    }
}
