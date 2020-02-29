using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateActivitiesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Activities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Activities",
                type: "datetime2",
                nullable: true);
        }
    }
}
