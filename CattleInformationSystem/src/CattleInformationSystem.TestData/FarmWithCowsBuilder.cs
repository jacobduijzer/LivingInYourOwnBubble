using CattleInformationSystem.Domain;

namespace CattleInformationSystem.TestData;

public class FarmWithCowsBuilder
{
    private FarmType _farmType;

    public FarmWithCowsBuilder WithFarmType(FarmType farmType)
    {
        _farmType = farmType;
        return this;
    }

    public Farm Generate(int numberOfCows)
    {
        return default;
    }
}