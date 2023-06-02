using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class removeRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Boards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Rents",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Boards",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
