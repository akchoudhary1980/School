using Microsoft.EntityFrameworkCore.Migrations;

namespace School.Migrations
{
    public partial class UserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolAddress",
                table: "SchoolModels");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TeacherModels",
                newName: "PermanetAddress");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "StaffModels",
                newName: "PermanetAddress");

            migrationBuilder.RenameColumn(
                name: "SchoolWhatsApp",
                table: "SchoolModels",
                newName: "WhatsApp");

            migrationBuilder.RenameColumn(
                name: "SchoolWebsite",
                table: "SchoolModels",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "SchoolTelephone",
                table: "SchoolModels",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "SchoolState",
                table: "SchoolModels",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "SchoolOwner",
                table: "SchoolModels",
                newName: "OwnerName");

            migrationBuilder.RenameColumn(
                name: "SchoolMobile",
                table: "SchoolModels",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "SchoolLogo",
                table: "SchoolModels",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "SchoolEmail",
                table: "SchoolModels",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "SchoolCountry",
                table: "SchoolModels",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "SchoolClassFromTo",
                table: "SchoolModels",
                newName: "ClassFromTo");

            migrationBuilder.RenameColumn(
                name: "SchoolCity",
                table: "SchoolModels",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "SchoolBoard",
                table: "SchoolModels",
                newName: "Address");

            migrationBuilder.AlterColumn<bool>(
                name: "WrightRights",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "UserCreateRights",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SettingRights",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ReadRights",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentAddress",
                table: "TeacherModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "TeacherModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentAddress",
                table: "StaffModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "StaffModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardID",
                table: "SchoolModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolNoOfSwimmingPool",
                table: "SchoolModels",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAddress",
                table: "TeacherModels");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "TeacherModels");

            migrationBuilder.DropColumn(
                name: "CurrentAddress",
                table: "StaffModels");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "StaffModels");

            migrationBuilder.DropColumn(
                name: "BoardID",
                table: "SchoolModels");

            migrationBuilder.DropColumn(
                name: "SchoolNoOfSwimmingPool",
                table: "SchoolModels");

            migrationBuilder.RenameColumn(
                name: "PermanetAddress",
                table: "TeacherModels",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "PermanetAddress",
                table: "StaffModels",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                table: "SchoolModels",
                newName: "SchoolWhatsApp");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "SchoolModels",
                newName: "SchoolWebsite");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "SchoolModels",
                newName: "SchoolTelephone");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "SchoolModels",
                newName: "SchoolState");

            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "SchoolModels",
                newName: "SchoolOwner");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "SchoolModels",
                newName: "SchoolMobile");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "SchoolModels",
                newName: "SchoolLogo");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SchoolModels",
                newName: "SchoolEmail");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "SchoolModels",
                newName: "SchoolCountry");

            migrationBuilder.RenameColumn(
                name: "ClassFromTo",
                table: "SchoolModels",
                newName: "SchoolClassFromTo");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "SchoolModels",
                newName: "SchoolCity");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "SchoolModels",
                newName: "SchoolBoard");

            migrationBuilder.AlterColumn<string>(
                name: "WrightRights",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreateRights",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "SettingRights",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ReadRights",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "SchoolAddress",
                table: "SchoolModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
