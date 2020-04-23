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
                keyValue: 1,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "You created new work [work]!", "Work created" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "You deleted work [work]!", "Work deleted" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "The system performed a new schedule generation!", "Schedule generation complete" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] pakvietė jus i veiką [work]!", "New invitation received" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] atmetė jūsų pakvietimą į renginį [work]!", "Invitation declined" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] priėmė jūsų pakvietimą į renginį [work]!", "Invitation accepted" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [work]!", "You have declined invitation" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "You have accepted [user]'s invitation to work [work]!", "You have accepted invitation" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs pakvietėtę [user] į veiką [work]!", "Invitation sent" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs sukūrėte nauja veiklą [activity]!", "Veiklos sukūrimas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs pašalinote veiklą [activity]!", "Veiklos šalinimas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Sistema atliko naują tvarkaraščio generavimą!", "Tvarkataščio generavimas atliktas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] pakvietė jus i veiką [activity]!", "Naujas kvietimas gautas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] atmetė jūsų pakvietimą į renginį [activity]!", "Kvietimas atmestas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Vartotojas [user] priėmė jūsų pakvietimą į renginį [activity]!", "Kvietimas priimtas" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!", "Jūs atmetėte kvietimą" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs priėmėte kvietimą vartotojo [user] pakvietimą į renginį [activity]!", "Jūs priėmėte kvietimą" });

            migrationBuilder.UpdateData(
                table: "MessageTemplates",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "TextTemplate", "TitleTemplate" },
                values: new object[] { "Jūs pakvietėtę [user] į veiką [activity]!", "Naujas kvietimas iššiūstas" });
        }
    }
}
