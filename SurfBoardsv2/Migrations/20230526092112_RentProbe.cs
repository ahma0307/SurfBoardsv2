using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class RentProbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_UserId",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rent",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SurfBoardsv2UserId",
                table: "Rent",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rent_SurfBoardsv2UserId",
                table: "Rent",
                column: "SurfBoardsv2UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_SurfBoardsv2UserId",
                table: "Rent",
                column: "SurfBoardsv2UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_AspNetUsers_SurfBoardsv2UserId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_SurfBoardsv2UserId",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "SurfBoardsv2UserId",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rent",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_UserId",
                table: "Rent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_AspNetUsers_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
