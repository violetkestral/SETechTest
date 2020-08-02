using Microsoft.EntityFrameworkCore.Migrations;

namespace GameManagement.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "PC" });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "PS4" });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Nintendo Switch" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
