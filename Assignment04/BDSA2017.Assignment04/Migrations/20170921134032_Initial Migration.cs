using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BDSA2017.Assignment04.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    carID = table.Column<int>(type: "int", nullable: false),
                    driver_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.carID);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    best_time = table.Column<int>(type: "int", nullable: true),
                    length_meter = table.Column<int>(type: "int", nullable: false),
                    max_cars = table.Column<int>(type: "int", nullable: false),
                    trackID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.trackID);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    raceID = table.Column<int>(type: "int", nullable: false),
                    actual_end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    actual_start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    number_of_laps = table.Column<int>(type: "int", nullable: false),
                    planned_end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    planned_start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    trackID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.raceID);
                    table.ForeignKey(
                        name: "FK_Race_Track",
                        column: x => x.trackID,
                        principalTable: "Track",
                        principalColumn: "trackID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarInRace",
                columns: table => new
                {
                    raceID = table.Column<int>(type: "int", nullable: false),
                    carID = table.Column<int>(type: "int", nullable: false),
                    best_lap = table.Column<int>(type: "int", nullable: true),
                    end_position = table.Column<int>(type: "int", nullable: true),
                    start_position = table.Column<int>(type: "int", nullable: false),
                    total_race_time = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInRace", x => new { x.raceID, x.carID });
                    table.ForeignKey(
                        name: "FK_CarInRace_Car",
                        column: x => x.carID,
                        principalTable: "Car",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarInRace_Race",
                        column: x => x.raceID,
                        principalTable: "Race",
                        principalColumn: "raceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarInRace_carID",
                table: "CarInRace",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_CarInRace",
                table: "CarInRace",
                column: "raceID");

            migrationBuilder.CreateIndex(
                name: "IX_Race_track_name",
                table: "Race",
                column: "trackID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarInRace");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Track");
        }
    }
}
