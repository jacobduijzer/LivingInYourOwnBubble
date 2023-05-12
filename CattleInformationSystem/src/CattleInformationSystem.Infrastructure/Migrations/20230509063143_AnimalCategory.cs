using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnimalCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows");

            migrationBuilder.DropIndex(
                name: "IX_FarmCows_CowId",
                table: "FarmCows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows",
                columns: new[] { "CowId", "FarmId" });

            migrationBuilder.CreateTable(
                name: "AnimalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Calved = table.Column<bool>(type: "boolean", nullable: false),
                    FarmType = table.Column<int>(type: "integer", nullable: false),
                    AgeInDays = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FarmCows_FarmId",
                table: "FarmCows",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows");

            migrationBuilder.DropIndex(
                name: "IX_FarmCows_FarmId",
                table: "FarmCows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows",
                columns: new[] { "FarmId", "CowId" });

            migrationBuilder.CreateIndex(
                name: "IX_FarmCows_CowId",
                table: "FarmCows",
                column: "CowId");
        }
    }
}
