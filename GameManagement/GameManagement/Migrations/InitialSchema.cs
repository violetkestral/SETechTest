using Microsoft.EntityFrameworkCore.Migrations;

namespace GameManagement.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Games",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Games", x => x.Id); });

            migrationBuilder.CreateTable(
                "Platforms",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Platforms", x => x.Id); });

            migrationBuilder.CreateTable(
                "GamePlatforms",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => x.Id);
                    table.ForeignKey(
                        "FK_GamePlatforms_Games_GameId",
                        x => x.GameId,
                        "Games",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GamePlatforms_Platforms_PlatformId",
                        x => x.PlatformId,
                        "Platforms",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GamePlatforms_GameId",
                "GamePlatforms",
                "GameId");

            migrationBuilder.CreateIndex(
                "IX_GamePlatforms_PlatformId",
                "GamePlatforms",
                "PlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GamePlatforms");

            migrationBuilder.DropTable(
                "Games");

            migrationBuilder.DropTable(
                "Platforms");
        }
    }
}