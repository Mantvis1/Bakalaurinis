using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class TranslateTemplatesToEngish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "TextTemplate",
                value: "You got inivtation from [user] to work [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "TextTemplate",
                value: "[user] declined your invitation to work [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "TextTemplate",
                value: "[user] accepted your invitation to work [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "TextTemplate",
                value: "You have declined [user]'s invitation to work [work]!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "TextTemplate",
                value: "Vartotojas [user] pakvietė jus i veiką [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "TextTemplate",
                value: "Vartotojas [user] atmetė jūsų pakvietimą į renginį [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "TextTemplate",
                value: "Vartotojas [user] priėmė jūsų pakvietimą į renginį [work]!");

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "TextTemplate",
                value: "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [work]!");
        }
    }
}
