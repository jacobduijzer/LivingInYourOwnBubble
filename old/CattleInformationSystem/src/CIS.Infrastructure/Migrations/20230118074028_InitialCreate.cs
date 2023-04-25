using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Calved = table.Column<bool>(type: "boolean", nullable: false),
                    ProductionTarget = table.Column<int>(type: "integer", nullable: false),
                    AgeInDays = table.Column<int>(type: "integer", nullable: false),
                    AgeInMonths = table.Column<int>(type: "integer", nullable: false),
                    AgeInYears = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LifeNumber = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DateCalved = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LifeNumberOfMother = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationNumber = table.Column<string>(type: "text", nullable: false),
                    ProductionTarget = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RawCowData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LifeNumber = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DateCalved = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LifeNumberOfMother = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawCowData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CowEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OccuredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CowId = table.Column<int>(type: "integer", nullable: false),
                    FarmLocationId = table.Column<int>(type: "integer", nullable: true),
                    AnimalCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CowEvent_AnimalCategories_AnimalCategoryId",
                        column: x => x.AnimalCategoryId,
                        principalTable: "AnimalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowEvent_Cows_CowId",
                        column: x => x.CowId,
                        principalTable: "Cows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowEvent_FarmLocations_FarmLocationId",
                        column: x => x.FarmLocationId,
                        principalTable: "FarmLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RawCowEventData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationNumber = table.Column<string>(type: "text", nullable: false),
                    OccuredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    RawCowDataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawCowEventData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawCowEventData_RawCowData_RawCowDataId",
                        column: x => x.RawCowDataId,
                        principalTable: "RawCowData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] { "Id", "AgeInDays", "AgeInMonths", "AgeInYears", "Calved", "Category", "Gender", "ProductionTarget" },
                values: new object[,]
                {
                    { 1, 0, 0, 0, true, 100, 0, 1 },
                    { 2, 0, 0, 0, false, 101, 0, 1 },
                    { 3, 0, 0, 0, false, 101, 1, 1 },
                    { 4, 0, 0, 0, false, 101, 0, 0 },
                    { 5, 0, 0, 1, false, 102, 0, 1 },
                    { 6, 0, 0, 1, false, 102, 0, 0 },
                    { 7, 0, 0, 1, false, 104, 1, 2 },
                    { 8, 14, 0, 0, false, 112, 1, 0 },
                    { 9, 14, 0, 0, false, 112, 0, 0 },
                    { 10, 0, 8, 0, false, 116, 1, 0 },
                    { 11, 0, 8, 0, false, 116, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "FarmLocations",
                columns: new[] { "Id", "LocationNumber", "ProductionTarget" },
                values: new object[,]
                {
                    { 1, "00000001", 2 },
                    { 2, "00000002", 1 },
                    { 3, "00000003", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CowEvent_AnimalCategoryId",
                table: "CowEvent",
                column: "AnimalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CowEvent_CowId",
                table: "CowEvent",
                column: "CowId");

            migrationBuilder.CreateIndex(
                name: "IX_CowEvent_FarmLocationId",
                table: "CowEvent",
                column: "FarmLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cows_LifeNumber",
                table: "Cows",
                column: "LifeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawCowData_LifeNumber",
                table: "RawCowData",
                column: "LifeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawCowEventData_RawCowDataId",
                table: "RawCowEventData",
                column: "RawCowDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CowEvent");

            migrationBuilder.DropTable(
                name: "RawCowEventData");

            migrationBuilder.DropTable(
                name: "AnimalCategories");

            migrationBuilder.DropTable(
                name: "Cows");

            migrationBuilder.DropTable(
                name: "FarmLocations");

            migrationBuilder.DropTable(
                name: "RawCowData");
        }
    }
}
