using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityPriority",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activities",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishUntil",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ScheduleStatus = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "ActivityPriority",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FinishUntil",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Activities");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
