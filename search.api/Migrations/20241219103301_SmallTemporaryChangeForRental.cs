using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace search.api.Migrations
{
    /// <inheritdoc />
    public partial class SmallTemporaryChangeForRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalFirmId",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalFirmId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
