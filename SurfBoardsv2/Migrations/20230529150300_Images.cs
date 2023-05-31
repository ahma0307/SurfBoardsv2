using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ImageFileName",
                table: "Boards",
                newName: "MainImageFileName");

            migrationBuilder.CreateTable(
                name: "BoardImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardImages_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardImages_BoardId",
                table: "BoardImages",
                column: "BoardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardImages");

            migrationBuilder.RenameColumn(
                name: "MainImageFileName",
                table: "Boards",
                newName: "ImageFileName");

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
    }
}
