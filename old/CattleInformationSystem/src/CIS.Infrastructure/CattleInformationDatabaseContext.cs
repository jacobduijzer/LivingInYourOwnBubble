using CIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIS.Infrastructure;

public class CattleInformationDatabaseContext : DbContext
{
    public CattleInformationDatabaseContext(DbContextOptions<CattleInformationDatabaseContext> options) : base(options)
    {
    }

    public DbSet<Cow> Cows => Set<Cow>();
    public DbSet<RawCowData> RawCowData => Set<RawCowData>();
    public DbSet<AnimalCategory> AnimalCategories => Set<AnimalCategory>();
    public DbSet<FarmLocation> FarmLocations => Set<FarmLocation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<AnimalCategory> animalCategoryData = new()
        {
            new AnimalCategory(1, Gender.Female, true, ProductionTarget.Milk, 0, 0, 0, 100),
            new AnimalCategory(2, Gender.Female, false, ProductionTarget.Milk, 0, 0, 0, 101),
            new AnimalCategory(3, Gender.Male, false, ProductionTarget.Milk, 0, 0, 0, 101),
            new AnimalCategory(4, Gender.Female, false, ProductionTarget.Consumption, 0, 0, 0, 101),
            new AnimalCategory(5, Gender.Female, false, ProductionTarget.Milk, 0, 0, 1, 102),
            new AnimalCategory(6, Gender.Female, false, ProductionTarget.Consumption, 0, 0, 1, 102),
            //
            new AnimalCategory(7, Gender.Male, false, ProductionTarget.Breeding, 0, 0, 1, 104),
            new AnimalCategory(8, Gender.Male, false, ProductionTarget.Consumption, 14, 0, 0, 112),
            new AnimalCategory(9, Gender.Female, false, ProductionTarget.Consumption, 14, 0, 0, 112),
            new AnimalCategory(10, Gender.Male, false, ProductionTarget.Consumption, 0, 8, 0, 116),
            new AnimalCategory(11, Gender.Female, false, ProductionTarget.Consumption, 0, 8, 0, 116),
        };

       // List<AnimalCategory> animalCategoryData = new()
       // {
       //     new()
       //     {
       //         Id = 1,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Milk,
       //         Category = 10
       //     },
       //     new()
       //     {
       //         Id = 2,
       //         Gender = Gender.Female,
       //         AgeInDays = 14,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Milk,
       //         Category = 20
       //     },
       //     new()
       //     {
       //         Id = 3,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 2,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Milk,
       //         Category = 30
       //     },
       //     new()
       //     {
       //         Id = 4,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 0,
       //         AgeInYears = 2,
       //         ProductionTarget = ProductionTarget.Milk,
       //         Category = 40
       //     },

       //     // Breeding
       //     new()
       //     {
       //         Id = 5,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 100
       //     },
       //     new()
       //     {
       //         Id = 6,
       //         Gender = Gender.Female,
       //         AgeInDays = 14,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 120
       //     },
       //     new()
       //     {
       //         Id = 7,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 2,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 130
       //     },
       //     new()
       //     {
       //         Id = 8,
       //         Gender = Gender.Female,
       //         AgeInDays = 0,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 100
       //     },
       //     new()
       //     {
       //         Id = 9,
       //         Gender = Gender.Male,
       //         AgeInDays = 14,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 120
       //     },
       //     new()
       //     {
       //         Id = 10,
       //         Gender = Gender.Male,
       //         AgeInDays = 0,
       //         AgeInMonths = 2,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Breeding,
       //         Category = 130
       //     },

       //     // Consumption
       //     new()
       //     {
       //         Id = 11,
       //         Gender = Gender.Male,
       //         AgeInDays = 0,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Consumption,
       //         Category = 200
       //     },
       //     new()
       //     {
       //         Id = 12,
       //         Gender = Gender.Male,
       //         AgeInDays = 14,
       //         AgeInMonths = 0,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Consumption,
       //         Category = 220
       //     },
       //     new()
       //     {
       //         Id = 13,
       //         Gender = Gender.Male,
       //         AgeInDays = 0,
       //         AgeInMonths = 2,
       //         AgeInYears = 0,
       //         ProductionTarget = ProductionTarget.Consumption,
       //         Category = 230
       //     }
       // };

        modelBuilder.Entity<AnimalCategory>()
            .HasData(animalCategoryData);

        List<FarmLocation> farmLocations = new()
        {
            new()
            {
                Id = 1,
                LocationNumber = "00000001",
                ProductionTarget = ProductionTarget.Breeding
            },
            new()
            {
                Id = 2,
                LocationNumber = "00000002",
                ProductionTarget = ProductionTarget.Milk
            },
            new()
            {
                Id = 3,
                LocationNumber = "00000003",
                ProductionTarget = ProductionTarget.Milk
            }
        };

        modelBuilder.Entity<FarmLocation>()
            .HasData(farmLocations);
    }
}