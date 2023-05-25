using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.UnitTests.Stubs;

public class FarmRepositoryStub : IFarmRepository
{
    public static IReadOnlyCollection<Farm> Farms = new List<Farm>
        {
            new Farm { Id = 1, UBN = "3000000001", FarmType = FarmType.BreedingForMilk },
            new Farm { Id = 2, UBN = "3000000002", FarmType = FarmType.BreedingForMeat},
            new Farm { Id = 3, UBN = "3000000003", FarmType = FarmType.Milk},
            new Farm { Id = 4, UBN = "3000000004", FarmType = FarmType.Meat},
            new Farm { Id = 5, UBN = "3000000005", FarmType = FarmType.Slaughterhouse}, 
        }.AsReadOnly();

    public Task<IReadOnlyCollection<Farm>> ByUbns(string[] ubns)
    {
        var matchingFarms = Farms.Where(x => ubns.Any(ubn => ubn.Equals(x.UBN))).ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyCollection<Farm>>(matchingFarms);
    }

    public Task<IReadOnlyCollection<Farm>> ByFarmIds(int[] farmIds)
    {
        var matchingFarms = Farms.Where(x => farmIds.Any(id => id.Equals(x.Id))).ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyCollection<Farm>>(matchingFarms);
    }
}