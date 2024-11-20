using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace search.api.Migrations
{
    /// <inheritdoc />
    public partial class NewDB4test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UsersDetails");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "UsersDetails");

            migrationBuilder.RenameTable(
                name: "UsersDetails",
                newName: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserDetails",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "UsersDetails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsersDetails",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UsersDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "UsersDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersDetails",
                table: "UsersDetails",
                column: "Id");
        }
    }
}
