using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class BoardModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardModels",
                columns: table => new
                {
                    BoardID = table.Column<int>(type: "int", nullable: false),
                    BoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardModels", x => x.BoardID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardModels");
        }
    }
}
