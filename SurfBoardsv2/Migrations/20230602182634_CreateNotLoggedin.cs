using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotLoggedin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardRenterFirstName",
                table: "Rents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoardRenterLastName",
                table: "Rents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardRenterPhoneNumber",
                table: "Rents",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardRenterFirstName",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "BoardRenterLastName",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "BoardRenterPhoneNumber",
                table: "Rents");
        }
    }
}
