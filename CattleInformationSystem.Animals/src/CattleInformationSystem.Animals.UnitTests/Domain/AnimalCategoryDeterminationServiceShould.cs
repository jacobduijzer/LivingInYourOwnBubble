using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.UnitTests.Stubs;

namespace CattleInformationSystem.Animals.UnitTests.Domain;

public class AnimalCategoryDeterminationServiceShould
{
    [Fact]
    public void DetermineTheRightCategory()
    {
        AnimalCategoryDeterminationService service = new(new List<AnimalCategory>());
    }
}

