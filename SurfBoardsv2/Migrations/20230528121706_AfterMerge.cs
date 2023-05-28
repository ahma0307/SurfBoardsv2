using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class AfterMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents");

            migrationBuilder.AlterColumn<string>(
                name: "BoardRenterId",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents",
                column: "BoardRenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents");

            migrationBuilder.AlterColumn<string>(
                name: "BoardRenterId",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents",
                column: "BoardRenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
