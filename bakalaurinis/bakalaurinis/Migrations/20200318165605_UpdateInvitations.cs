using Microsoft.EntityFrameworkCore.Migrations;

namespace bakalaurinis.Migrations
{
    public partial class UpdateInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invitations_WorkId",
                table: "Invitations",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Works_WorkId",
                table: "Invitations",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Works_WorkId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_WorkId",
                table: "Invitations");
        }
    }
}
