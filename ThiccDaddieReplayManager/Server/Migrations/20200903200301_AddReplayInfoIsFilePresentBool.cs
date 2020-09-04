using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiccDaddie.ReplayManager.Server.Migrations
{
    public partial class AddReplayInfoIsFilePresentBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFilePresent",
                table: "ReplayInfos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFilePresent",
                table: "ReplayInfos");
        }
    }
}
