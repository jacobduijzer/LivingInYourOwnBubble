using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

namespace CattleInformationSystem.Animals.UnitTests.Stubs;

public class CowRepositoryStub : ICowRepository
{
    private readonly Cow _defaultCow = new Cow()
    {
        CowEvents = new List<CowEvent>(),
        FarmCows = new List<FarmCow>()
    };
    
    public Task<Cow> ByLifeNumber(string lifeNumber)
    {
        return Task.FromResult(new Cow()
        {
            LifeNumber = lifeNumber,
            CowEvents = _defaultCow.CowEvents,
            FarmCows = _defaultCow.FarmCows
        });
    }

    public Task Save(Cow cow)
    {
        throw new NotImplementedException();
    }

    public Task Update(Cow cow)
    {
        throw new NotImplementedException();
    }
}