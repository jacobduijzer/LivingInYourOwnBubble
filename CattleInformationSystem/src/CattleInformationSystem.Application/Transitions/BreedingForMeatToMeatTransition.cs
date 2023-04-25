using CattleInformationSystem.Domain;

namespace CattleInformationSystem.Application.Transitions;

public class BreedingForMeatToMeatTransition : ICowTransition
{
    private readonly IFarmCowRepository _farmCows;
    private readonly List<Farm> _farms;
    private readonly Random _random;

    public BreedingForMeatToMeatTransition(
        IFarmCowRepository farmCows,
        List<Farm> farms)
    {
        _farmCows = farmCows;
        _farms = farms;
        _random = new Random();
    }
    
    public async Task Handle(Farm farm)
    {
        foreach (var farmCow in farm.FarmCows.Where(fc => fc.Cow != null).ToList())
        {
            var randomEndDate = farmCow.Cow.DateOfBirth
                .AddYears(2)
                .AddDays(_random.Next(1, 30));

            // set end date to random date of birth date + 2 years + 1 month
            farmCow.EndDate = randomEndDate;
            
            var randomSlaughterDate = randomEndDate
                .AddMonths(5)
                .AddDays(_random.Next(1, 60));

            // move cow to meat farm on end date
            var meatFarm = _farms
                .Where(f => f.FarmType == FarmType.Meat)
                .RandomElement();
            
            await _farmCows.Add(new FarmCow()
            {
                FarmId = meatFarm.Id,
                CowId = farmCow.CowId,
                StartDate = randomEndDate,
                EndDate = randomSlaughterDate
            });
            
            var slaughterHouse = _farms
                .Where(f => f.FarmType == FarmType.Slaughterhouse)
                .RandomElement();
            
            await _farmCows.Add(new FarmCow()
            {
                FarmId = slaughterHouse.Id,
                CowId = farmCow.CowId,
                StartDate = randomSlaughterDate,
                EndDate = randomSlaughterDate
            });
            
            farmCow.Cow.DateOfDeath = randomSlaughterDate;
        } 
    }
}