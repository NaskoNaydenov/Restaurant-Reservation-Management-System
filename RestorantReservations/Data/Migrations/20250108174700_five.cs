using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestorantReservations.Data.Migrations
{
    public partial class five : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Reservation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "id");
        }
    }
}
