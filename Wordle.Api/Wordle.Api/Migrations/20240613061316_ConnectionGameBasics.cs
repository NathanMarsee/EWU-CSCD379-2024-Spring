using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionGameBasics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionGroupWordId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionsOfTheDayId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConnectionGroups",
                columns: table => new
                {
                    WordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionGroups", x => x.WordId);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionsOfTheDay",
                columns: table => new
                {
                    ConnectionsOfTheDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionsOfTheDay", x => x.ConnectionsOfTheDayId);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionGroupConnectionsOfTheDay",
                columns: table => new
                {
                    ConnectionsOfTheDaysConnectionsOfTheDayId = table.Column<int>(type: "int", nullable: false),
                    ConnectionsWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionGroupConnectionsOfTheDay", x => new { x.ConnectionsOfTheDaysConnectionsOfTheDayId, x.ConnectionsWordId });
                    table.ForeignKey(
                        name: "FK_ConnectionGroupConnectionsOfTheDay_ConnectionGroups_ConnectionsWordId",
                        column: x => x.ConnectionsWordId,
                        principalTable: "ConnectionGroups",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionGroupConnectionsOfTheDay_ConnectionsOfTheDay_ConnectionsOfTheDaysConnectionsOfTheDayId",
                        column: x => x.ConnectionsOfTheDaysConnectionsOfTheDayId,
                        principalTable: "ConnectionsOfTheDay",
                        principalColumn: "ConnectionsOfTheDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ConnectionGroupWordId",
                table: "Games",
                column: "ConnectionGroupWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ConnectionsOfTheDayId",
                table: "Games",
                column: "ConnectionsOfTheDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionGroupConnectionsOfTheDay_ConnectionsWordId",
                table: "ConnectionGroupConnectionsOfTheDay",
                column: "ConnectionsWordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ConnectionGroups_ConnectionGroupWordId",
                table: "Games",
                column: "ConnectionGroupWordId",
                principalTable: "ConnectionGroups",
                principalColumn: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ConnectionsOfTheDay_ConnectionsOfTheDayId",
                table: "Games",
                column: "ConnectionsOfTheDayId",
                principalTable: "ConnectionsOfTheDay",
                principalColumn: "ConnectionsOfTheDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ConnectionGroups_ConnectionGroupWordId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_ConnectionsOfTheDay_ConnectionsOfTheDayId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "ConnectionGroupConnectionsOfTheDay");

            migrationBuilder.DropTable(
                name: "ConnectionGroups");

            migrationBuilder.DropTable(
                name: "ConnectionsOfTheDay");

            migrationBuilder.DropIndex(
                name: "IX_Games_ConnectionGroupWordId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ConnectionsOfTheDayId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ConnectionGroupWordId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ConnectionsOfTheDayId",
                table: "Games");
        }
    }
}
