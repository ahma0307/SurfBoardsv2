using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class Merge1stJune : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "BoardImages",
                newName: "Extension");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_BoardId",
                table: "Rents",
                column: "BoardId");

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
                name: "FK_Rents_Boards_BoardId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_BoardId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "BoardImages",
                newName: "FilePath");
        }
    }
}
