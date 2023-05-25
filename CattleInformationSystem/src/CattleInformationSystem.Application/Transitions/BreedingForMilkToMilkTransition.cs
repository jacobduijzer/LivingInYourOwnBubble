using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Application.Transitions;

public class BreedingForMilkToMilkTransition : ICowTransition
{
   private readonly IFarmCowRepository _farmCows;
   private readonly List<Farm> _farms;
   private readonly Random _random;

   public BreedingForMilkToMilkTransition(
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

         // move cow to milk farm on end date
         var milkFarm = _farms
            .Where(f => f.FarmType == FarmType.Milk)
            .RandomElement();
         await _farmCows.Add(new FarmCow()
         {
            FarmId = milkFarm.Id,
            CowId = farmCow.CowId,
            StartDate = randomEndDate
         });

         // update cow, set calved date to random date, enddate + random 3 months
         var randomCalvedDate = randomEndDate
            .AddMonths(8)
            .AddDays(_random.Next(1, 50));
         farmCow.Cow.DateFirstCalved = randomCalvedDate;
      } 
   }
}