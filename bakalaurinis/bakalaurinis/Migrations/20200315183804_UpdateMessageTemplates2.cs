using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateMessageTemplates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "TextTemplate",
                value: "Jūs priėmėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "TextTemplate",
                value: "Jūs priėmėte kvietimą vartotojo[user] pakvietimą į renginį[activity]!");
        }
    }
}
