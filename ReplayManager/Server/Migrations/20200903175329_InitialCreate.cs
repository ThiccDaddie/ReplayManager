using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiccDaddie.ReplayManager.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReplayInfos",
                columns: table => new
                {
                    ReplayInfoId = table.Column<string>(nullable: false),
                    PlayerVehicle = table.Column<string>(nullable: true),
                    ClientVersionFromExe = table.Column<string>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    PlayerID = table.Column<int>(nullable: false),
                    ServerName = table.Column<string>(nullable: true),
                    MapDisplayName = table.Column<string>(nullable: true),
                    DateTime = table.Column<string>(nullable: true),
                    BattleType = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayInfos", x => x.ReplayInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplayInfos");
        }
    }
}
