using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestorantReservations.Data.Migrations
{
    /// <inheritdoc />
    public partial class NameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Table",
                newName: "capacity");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Table",
                newName: "available");

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "Reservation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Table",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "available",
                table: "Table",
                newName: "Available");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reservation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
