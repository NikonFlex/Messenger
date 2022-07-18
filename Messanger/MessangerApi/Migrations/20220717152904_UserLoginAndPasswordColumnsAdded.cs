using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessangerApi.Migrations
{
    public partial class UserLoginAndPasswordColumnsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Users",
                newName: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "MobileNumber");
        }
    }
}
