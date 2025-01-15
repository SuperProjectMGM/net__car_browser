using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace search.api.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddedPriceLongLatAndRetDescToRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firms");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReturnClientDescription",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ReturnLatitude",
                table: "Rentals",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ReturnLongtitude",
                table: "Rentals",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnClientDescription",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnLatitude",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnLongtitude",
                table: "Rentals");

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.Id);
                });
        }
    }
}
