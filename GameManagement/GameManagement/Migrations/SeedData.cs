using Microsoft.EntityFrameworkCore.Migrations;

namespace GameManagement.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Platforms",
                new[] {"Id", "Name"},
                new object[] {1, "PC"});

            migrationBuilder.InsertData(
                "Platforms",
                new[] {"Id", "Name"},
                new object[] {2, "PS4"});

            migrationBuilder.InsertData(
                "Platforms",
                new[] {"Id", "Name"},
                new object[] {3, "Nintendo Switch"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}