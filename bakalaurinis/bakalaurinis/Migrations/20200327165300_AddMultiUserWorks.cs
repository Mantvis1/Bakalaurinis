using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class AddMultiUserWorks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WillBeParticipant",
                table: "Works",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WillBeParticipant",
                table: "Works");
        }
    }
}
