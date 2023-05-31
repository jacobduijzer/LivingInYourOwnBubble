using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.UnitTests.Stubs;

public class CategoryRepositoryStub : ICategoryRepository
{
    public Task<IReadOnlyCollection<AnimalCategory>> All()
    {
        var categories = new List<AnimalCategory>()
        {
            new AnimalCategory { Category = 101, Calved = false, Gender = Gender.Female, FarmType = FarmType.BreedingForMilk, AgeInDays = 0},
            new AnimalCategory { Category = 101, Calved = false, Gender = Gender.Female, FarmType = FarmType.Milk, AgeInDays = 0},
        };
        return Task.FromResult<IReadOnlyCollection<AnimalCategory>>(categories.AsReadOnly());
    }
}