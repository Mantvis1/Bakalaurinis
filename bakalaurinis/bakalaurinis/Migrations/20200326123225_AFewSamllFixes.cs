using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class AFewSamllFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Works");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvitationsConfirmed",
                table: "Works",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "TextTemplate",
                value: "Jūs pakvietėtę [user] į veiką [activity]!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvitationsConfirmed",
                table: "Works");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Works",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "TextTemplate",
                value: "Vartotojas [user] pakvietė jus i veiką [activity]!");
        }
    }
}
