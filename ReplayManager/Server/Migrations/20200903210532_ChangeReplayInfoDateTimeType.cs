using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThiccDaddie.ReplayManager.Server.Migrations
{
    public partial class ChangeReplayInfoDateTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ReplayInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                table: "ReplayInfos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
