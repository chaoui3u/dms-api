using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace meteoAPI.Migrations
{
    public partial class weatherClassDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRecords_Weather_WeatherId",
                table: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_WeatherId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "WeatherId",
                table: "WeatherRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WeatherId",
                table: "WeatherRecords",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_WeatherId",
                table: "WeatherRecords",
                column: "WeatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_Weather_WeatherId",
                table: "WeatherRecords",
                column: "WeatherId",
                principalTable: "Weather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
