using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfBoardsv2.Migrations
{
    /// <inheritdoc />
    public partial class MainImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageFileName",
                table: "Boards");

            migrationBuilder.AddColumn<string>(
                name: "MainImageFilePath",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "MainImageId",
                table: "Boards",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageFilePath",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "MainImageId",
                table: "Boards");

            migrationBuilder.AddColumn<string>(
                name: "MainImageFileName",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
