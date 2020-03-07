using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class CreateMessageTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleTemplate = table.Column<string>(nullable: true),
                    TextTemplate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MessageTemplates",
                columns: new[] { "Id", "TextTemplate", "TitleTemplate" },
                values: new object[,]
                {
                    { 1, "Jūs sukūrėte nauja veiklą [activity]!", "Veiklos sukūrimas" },
                    { 2, "Jūs pašalinote veiklą [activity]!", "Veiklos šalinimas" },
                    { 3, "Sistema atliko naują tvarkaraščio generavimą!", "Tvarkataščio generavimas atliktas" },
                    { 4, "Vartotojas [user] pakvietė jus i veiką [activity]!", "Naujas kvietimas į renginį" },
                    { 5, "Vartotojas [user] atmetė jūsų pakvietimą į renginį [activity]!", "Kvietimas atmestas" },
                    { 6, "Vartotojas [user] priėmė jūsų pakvietimą į renginį [activity]!", "Kvietimas priimtas" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Messages");
        }
    }
}
