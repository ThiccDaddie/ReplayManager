using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReplayManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Replays",
                columns: table => new
                {
                    Directory = table.Column<string>(type: "TEXT", nullable: false),
                    RelativeFilePath = table.Column<string>(type: "TEXT", nullable: false),
                    IsFilePresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlayerVehicle = table.Column<string>(type: "TEXT", nullable: false),
                    ClientVersionFromXml = table.Column<string>(type: "TEXT", nullable: false),
                    RegionCode = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: false),
                    MapDisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    BattleType = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replays", x => new { x.Directory, x.RelativeFilePath });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replays");
        }
    }
}
