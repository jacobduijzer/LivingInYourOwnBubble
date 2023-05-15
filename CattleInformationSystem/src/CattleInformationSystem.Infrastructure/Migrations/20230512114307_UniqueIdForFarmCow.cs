using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIdForFarmCow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FarmCows",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FarmCows_CowId",
                table: "FarmCows",
                column: "CowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows");

            migrationBuilder.DropIndex(
                name: "IX_FarmCows_CowId",
                table: "FarmCows");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FarmCows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmCows",
                table: "FarmCows",
                columns: new[] { "CowId", "FarmId" });
        }
    }
}
