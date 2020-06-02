using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateActivityPriorityToWorkPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityPriority",
                table: "Works");

            migrationBuilder.AddColumn<int>(
                name: "WorkPriority",
                table: "Works",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkPriority",
                table: "Works");

            migrationBuilder.AddColumn<int>(
                name: "ActivityPriority",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
