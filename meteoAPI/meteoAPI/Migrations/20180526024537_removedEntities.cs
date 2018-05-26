using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace meteoAPI.Migrations
{
    public partial class removedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WeatherRecords_Clouds_CloudsId",
            //    table: "WeatherRecords");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WeatherRecords_Rain_RainId",
            //    table: "WeatherRecords");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WeatherRecords_Snow_SnowId",
            //    table: "WeatherRecords");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WeatherRecords_Sun_SunId",
            //    table: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "Clouds");

            migrationBuilder.DropTable(
                name: "Rain");

            migrationBuilder.DropTable(
                name: "Snow");

            migrationBuilder.DropTable(
                name: "Sun");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_CloudsId",
                table: "WeatherRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_RainId",
                table: "WeatherRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_SnowId",
                table: "WeatherRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_SunId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "CloudsId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "RainId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "SnowId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "SunId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "GrndLevel",
                table: "MainData");

            migrationBuilder.DropColumn(
                name: "SeaLevel",
                table: "MainData");

            migrationBuilder.DropColumn(
                name: "TempMax",
                table: "MainData");

            migrationBuilder.DropColumn(
                name: "TempMin",
                table: "MainData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CloudsId",
                table: "WeatherRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RainId",
                table: "WeatherRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SnowId",
                table: "WeatherRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SunId",
                table: "WeatherRecords",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GrndLevel",
                table: "MainData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SeaLevel",
                table: "MainData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempMax",
                table: "MainData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempMin",
                table: "MainData",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Clouds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    All = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clouds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rain",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Volume = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Snow",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Volume = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sun",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SunRise = table.Column<DateTimeOffset>(nullable: false),
                    SunSet = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sun", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_CloudsId",
                table: "WeatherRecords",
                column: "CloudsId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_RainId",
                table: "WeatherRecords",
                column: "RainId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_SnowId",
                table: "WeatherRecords",
                column: "SnowId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_SunId",
                table: "WeatherRecords",
                column: "SunId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_Clouds_CloudsId",
                table: "WeatherRecords",
                column: "CloudsId",
                principalTable: "Clouds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_Rain_RainId",
                table: "WeatherRecords",
                column: "RainId",
                principalTable: "Rain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_Snow_SnowId",
                table: "WeatherRecords",
                column: "SnowId",
                principalTable: "Snow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_Sun_SunId",
                table: "WeatherRecords",
                column: "SunId",
                principalTable: "Sun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
