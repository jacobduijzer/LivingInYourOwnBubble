using CattleInformationSystem.Application.Transitions;
using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Application;

public record CreateTestDataCommand();

public class CreateTestDataHandler
{
    private readonly IDataGenerator _dataGenerator;
    private readonly ICowRepository _cows;
    private readonly IFarmRepository _farms;
    private readonly IFarmCowRepository _farmCows;
    private readonly Random _random;

    public CreateTestDataHandler(
        IDataGenerator dataGenerator,
        ICowRepository cows,
        IFarmRepository farms,
        IFarmCowRepository farmCows)
    {
        _dataGenerator = dataGenerator;
        _cows = cows;
        _farms = farms;
        _farmCows = farmCows;

        _random = new Random();
    }

    public async Task Handle(CreateTestDataCommand createTestDataCommand)
    {
        await CreateFarmsAndCows();

        await AddCowsToInitialFarm();

        await MakeCowTransitions();
    }

    private async Task CreateFarmsAndCows()
    {
        var farms = _dataGenerator.Farms();
        await _farms.AddRange(farms);

        var cows = _dataGenerator.Cows();
        await _cows.AddRange(cows);
    }

    private async Task AddCowsToInitialFarm()
    {
        var farms = await _farms.All();
        var cows = await _cows.All();

        foreach (var cow in cows)
        {
            if (cow.Gender == Gender.Female)
            {
                var farm = farms
                    .Where(farm => farm.FarmType == FarmType.BreedingForMilk)
                    .RandomElement();

                await _farmCows.Add(new FarmCow()
                {
                    FarmId = farm.Id,
                    CowId = cow.Id,
                    StartDate = cow.DateOfBirth
                });
            }
            else
            {
                var farm = farms
                    .Where(farm => farm.FarmType == FarmType.BreedingForMeat)
                    .RandomElement();

                await _farmCows.Add(new FarmCow()
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
        var farms = await _farms.All();
        var transitionFactory = new TransitionFactory(_farmCows, farms);

        foreach (var farm in farms.Where(farm => farm.Cows != null && farm.Cows.Any()).ToList())
        {
            await transitionFactory
                .Create(farm.FarmType)
                .Handle(farm);
        }

        await _farms.SaveChanges();
    }
}