using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    ReplayInfoId = table.Column<string>(type: "TEXT", nullable: false),
                    IsFilePresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlayerVehicle = table.Column<string>(type: "TEXT", nullable: true),
                    ClientVersionFromExe = table.Column<string>(type: "TEXT", nullable: true),
                    RegionCode = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: true),
                    MapDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BattleType = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerName = table.Column<string>(type: "TEXT", nullable: true),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replays", x => x.ReplayInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replays");
        }
    }
}
