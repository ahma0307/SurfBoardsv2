using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class RowVersionNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Rents");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Rents",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Rents");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Rents",
                rowVersion: true,
                nullable: true);
        }
    }
}
