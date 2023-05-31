using Bogus;
using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Infrastructure;

public class FakeDataGenerator : IDataGenerator
{
   private readonly Random _random;
   private const int NumberOfFarmsPerType = 2;
   private const int NumberOfCowsPerBatch = 100;
   private static readonly DateTime StartDate = new DateTime(2015, 1, 1);
   private static readonly DateTime EndDate = new DateTime(2015, 3, 1);

   public FakeDataGenerator()
   {
      Randomizer.Seed = new Random(8675309);
      _random = new Random();
   }
   
   public List<Farm> Farms()
   {
      var farmId = 0;
      List<Farm> farms = new();
      foreach (var farmType in  (FarmType[]) Enum.GetValues(typeof(FarmType)))
      {
         var fakeFarms = new Faker<Farm>()
            .RuleFor(farm => farm.FarmType, farmType)
            .RuleFor(farm => farm.UBN, faker => $"100{DateTime.Now.Ticks}{farmId++}")
            .Generate(NumberOfFarmsPerType);
         
         farms.AddRange(fakeFarms);
      }

      return farms;
   }
   
   public List<Cow> Cows()
   {
      var lifeNumberId = 0;
      var cows = new Faker<Cow>()
         .RuleFor(cow => cow.LifeNumber, faker => $"200{DateTime.Now.Ticks}{lifeNumberId++}")
         .RuleFor(cow => cow.Gender, faker => faker.PickRandom<Gender>())
         .RuleFor(cow => cow.DateOfBirth,
            faker => DateOnly.Parse(faker.Date.Between(StartDate, EndDate).ToString()));
            
      return cows.Generate(NumberOfCowsPerBatch);
   }
}