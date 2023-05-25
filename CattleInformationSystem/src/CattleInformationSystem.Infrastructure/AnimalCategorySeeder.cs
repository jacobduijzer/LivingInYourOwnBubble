using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Infrastructure;

public class AnimalCategorySeeder
{
    private readonly DatabaseContext _databaseContext;

    public AnimalCategorySeeder(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Seed()
    {
        if (_databaseContext.AnimalCategories.Any())
            return;
        
        await _databaseContext.AnimalCategories.AddRangeAsync( 
            new List<AnimalCategory>
            {
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = true,
                    FarmType = FarmType.Milk,
                    AgeInDays = 0,
                    Category = 100
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = true,
                    FarmType = FarmType.BreedingForMeat,
                    AgeInDays = 0,
                    Category = 100
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = true,
                    FarmType = FarmType.BreedingForMilk,
                    AgeInDays = 0,
                    Category = 100
                },
                new AnimalCategory
                {
                    Gender = Gender.Male,
                    Calved = false,
                    FarmType = FarmType.Milk,
                    AgeInDays = 0,
                    Category = 101
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = false,
                    FarmType = FarmType.Milk,
                    AgeInDays = 0,
                    Category = 101
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = false,
                    FarmType = FarmType.Milk,
                    AgeInDays = 365,
                    Category = 102
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = false,
                    FarmType = FarmType.BreedingForMeat,
                    AgeInDays = 365,
                    Category = 102
                },
                new AnimalCategory
                {
                    Gender = Gender.Female,
                    Calved = false,
                    FarmType = FarmType.BreedingForMilk,
                    AgeInDays = 365,
                    Category = 102
                }
            }
        );
        await _databaseContext.SaveChangesAsync();
    }
}