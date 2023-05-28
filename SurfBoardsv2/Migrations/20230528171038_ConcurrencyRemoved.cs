using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class ConcurrencyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Boards_RentedBoardId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_BoardRenterId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_RentedBoardId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Rents");

            migrationBuilder.AlterColumn<Guid>(
                name: "BoardRenterId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurfBoardsv2UserId",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_BoardId",
                table: "Rents",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_SurfBoardsv2UserId",
                table: "Rents",
                column: "SurfBoardsv2UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_SurfBoardsv2UserId",
                table: "Rents",
                column: "SurfBoardsv2UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Boards_BoardId",
                table: "Rents",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_SurfBoardsv2UserId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Boards_BoardId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_BoardId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_SurfBoardsv2UserId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "SurfBoardsv2UserId",
                table: "Rents");

            migrationBuilder.AlterColumn<string>(
                name: "BoardRenterId",
                table: "Rents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Rents",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_BoardRenterId",
                table: "Rents",
                column: "BoardRenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentedBoardId",
                table: "Rents",
                column: "RentedBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents",
                column: "BoardRenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Boards_RentedBoardId",
                table: "Rents",
                column: "RentedBoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
