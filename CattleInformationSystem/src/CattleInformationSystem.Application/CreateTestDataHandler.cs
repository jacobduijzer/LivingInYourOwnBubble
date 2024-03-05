using CattleInformationSystem.Application.Transitions;
using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Application;

public record CreateTestDataCommand();

public class CreateTestDataHandler(
    IDataGenerator dataGenerator,
    ICowRepository cows,
    IFarmRepository farms,
    IFarmCowRepository farmCows)
{
    private readonly Random _random = new();

    public async Task Handle(CreateTestDataCommand createTestDataCommand)
    {
        await CreateFarmsAndCows();

        await AddCowsToInitialFarm();

        await MakeCowTransitions();
    }

    private async Task CreateFarmsAndCows()
    {
        var farms1 = dataGenerator.Farms();
        await farms.AddRange(farms1);

        var cows1 = dataGenerator.Cows();
        await cows.AddRange(cows1);
    }

    private async Task AddCowsToInitialFarm()
    {
        var farms1 = await farms.All();
        var cows1 = await cows.All();

        foreach (var cow in cows1)
        {
            if (cow.Gender == Gender.Female)
            {
                var farm = farms1
                    .Where(farm => farm.FarmType == FarmType.BreedingForMilk)
                    .RandomElement();

                await farmCows.Add(new FarmCow()
                {
                    FarmId = farm.Id,
                    CowId = cow.Id,
                    StartDate = cow.DateOfBirth
                });
            }
            else
            {
                var farm = farms1
                    .Where(farm => farm.FarmType == FarmType.BreedingForMeat)
                    .RandomElement();

                await farmCows.Add(new FarmCow()
                {
                    FarmId = farm.Id,
                    CowId = cow.Id,
                    StartDate = cow.DateOfBirth
                });
            }
        }
    }

    private async Task MakeCowTransitions()
    {
        var farms1 = await farms.All();
        var transitionFactory = new TransitionFactory(farmCows, farms1);

        foreach (var farm in farms1.Where(farm => farm.Cows != null && farm.Cows.Any()).ToList())
        {
            await transitionFactory
                .Create(farm.FarmType)
                .Handle(farm);
        }

        await farms.SaveChanges();
    }
}