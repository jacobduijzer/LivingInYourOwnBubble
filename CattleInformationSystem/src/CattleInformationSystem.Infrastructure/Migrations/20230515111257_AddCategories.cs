using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, true, (int)FarmType.Milk, 0, 100});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, true, (int)FarmType.BreedingForMeat, 0, 100});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, true, (int)FarmType.BreedingForMilk, 0, 100});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.BreedingForMilk, 0, 101});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Male, false, (int)FarmType.BreedingForMilk, 0, 101});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Male, false, (int)FarmType.Milk, 0, 101});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.Milk, 0, 101});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.Meat, 0, 101});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.Milk, 365, 102});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.BreedingForMeat, 365, 102});
            
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] {"Gender", "Calved", "FarmType", "AgeInDays", "Category"},
                values: new object[] {(int)Gender.Female, false, (int)FarmType.BreedingForMilk, 365, 102});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
