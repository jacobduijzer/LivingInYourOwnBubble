using CattleInformationSystem.Domain;

namespace CattleInformationSystem.Application.Transitions;

public class TransitionFactory
{
    private readonly IFarmCowRepository _farmCows;
    private readonly List<Farm> _farms;

    public TransitionFactory(
        IFarmCowRepository farmCows,
        List<Farm> farms)
    {
        _farmCows = farmCows;
        _farms = farms;
    }

    public ICowTransition Create(FarmType farmType) =>
        farmType switch
        {
           FarmType.BreedingForMilk => new BreedingForMilkToMilkTransition(_farmCows, _farms),
           FarmType.BreedingForMeat => new BreedingForMeatToMeatTransition(_farmCows, _farms),
            _ => throw new Exception($"FarmType {farmType} unknown.")
        };
}