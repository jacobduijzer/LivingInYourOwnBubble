using CattleInformationSystem.Animals.Domain;

namespace CattleInformationSystem.Animals.UnitTests.Stubs;

public class CategoryRepositoryStub : ICategoryRepository
{
    public Task<IReadOnlyCollection<AnimalCategory>> All()
    {
        var categories = new List<AnimalCategory>()
        {
// TODO: fill
        };
        return Task.FromResult<IReadOnlyCollection<AnimalCategory>>(categories.AsReadOnly());
    }
}