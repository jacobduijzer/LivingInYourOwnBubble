using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Infrastructure;

public class FarmSeeder
{
    private readonly DatabaseContext _databaseContext;

    public FarmSeeder(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Seed()
    {
        if (_databaseContext.Farms.Any())
            return;

        await _databaseContext.Farms.AddRangeAsync(new List<Farm>()
        {
            new Farm { UBN = "20000000001", FarmType = FarmType.BreedingForMilk },
            new Farm { UBN = "20000000002", FarmType = FarmType.BreedingForMilk },
            new Farm { UBN = "20000000003", FarmType = FarmType.BreedingForMeat },
            new Farm { UBN = "20000000004", FarmType = FarmType.BreedingForMeat },
            new Farm { UBN = "20000000005", FarmType = FarmType.Milk },
            new Farm { UBN = "20000000006", FarmType = FarmType.Milk},
            new Farm { UBN = "20000000007", FarmType = FarmType.Meat},
            new Farm { UBN = "20000000008", FarmType = FarmType.Meat},
            new Farm { UBN = "20000000009", FarmType = FarmType.Slaughterhouse},
            new Farm { UBN = "20000000010", FarmType = FarmType.Slaughterhouse},
        });
        await _databaseContext.SaveChangesAsync();
    }
}