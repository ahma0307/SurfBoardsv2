using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class RentPropertiesClassesNotIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_SurfBoardsv2UserId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "SurfBoardModelId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "SurfBoardModelName",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "SurfBoardsv2UserId",
                table: "Rents",
                newName: "BoardRenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_SurfBoardsv2UserId",
                table: "Rents",
                newName: "IX_Rents_BoardRenterId");

            migrationBuilder.AddColumn<Guid>(
                name: "RentedBoardId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentedBoardId",
                table: "Rents",
                column: "RentedBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents",
                column: "BoardRenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Boards_RentedBoardId",
                table: "Rents",
                column: "RentedBoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_AspNetUsers_BoardRenterId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Boards_RentedBoardId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_RentedBoardId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RentedBoardId",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "BoardRenterId",
                table: "Rents",
                newName: "SurfBoardsv2UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_BoardRenterId",
                table: "Rents",
                newName: "IX_Rents_SurfBoardsv2UserId");

            migrationBuilder.AddColumn<string>(
                name: "SurfBoardModelId",
                table: "Rents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SurfBoardModelName",
                table: "Rents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_AspNetUsers_SurfBoardsv2UserId",
                table: "Rents",
                column: "SurfBoardsv2UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
