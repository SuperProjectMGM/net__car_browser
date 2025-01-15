using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace search.api.Migrations
{
    /// <inheritdoc />
    public partial class RentalsAddedProviderIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarProviderIdentifier",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarProviderIdentifier",
                table: "Rentals");
        }
    }
}
