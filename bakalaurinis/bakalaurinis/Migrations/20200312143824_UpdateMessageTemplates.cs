using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateMessageTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "TitleTemplate",
                value: "Naujas kvietimas gautas");

            migrationBuilder.InsertData(
                table: "MessageTemplates",
                columns: new[] { "Id", "TextTemplate", "TitleTemplate" },
                values: new object[,]
                {
                    { 7, "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!", "Jūs atmetėte kvietimą" },
                    { 8, "Jūs priėmėte kvietimą vartotojo[user] pakvietimą į renginį[activity]!", "Jūs priėmėte kvietimą" },
                    { 9, "Vartotojas [user] pakvietė jus i veiką [activity]!", "Naujas kvietimas iššiūstas" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "TitleTemplate",
                value: "Naujas kvietimas į renginį");
        }
    }
}
