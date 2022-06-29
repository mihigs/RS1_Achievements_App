using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_api.Data.Migrations
{
    public partial class achievements_achieved_by : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AchievementId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AchievementId",
                table: "AspNetUsers",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Achievement_AchievementId",
                table: "AspNetUsers",
                column: "AchievementId",
                principalTable: "Achievement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Achievement_AchievementId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AchievementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "AspNetUsers");
        }
    }
}
