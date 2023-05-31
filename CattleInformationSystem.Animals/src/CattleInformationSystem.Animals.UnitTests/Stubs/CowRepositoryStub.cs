using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.UnitTests.Stubs;

public class CowRepositoryStub : ICowRepository
{
    private Cow? _cow;
    private readonly Cow _defaultCow = new Cow()
    {
        CowEvents = new List<CowEvent>
        {
          new CowEvent
          {
              Order = 0,
              Category = 101,
              Reason = Reason.Birth,
              FarmId = 1,
          }   
        },
        FarmCows = new List<FarmCow>
        {
            new FarmCow
            {
                FarmId = 1,
                StartDate = DateOnly.MinValue
            }
        }
    };


    public Task<Cow> ByLifeNumber(string lifeNumber)
    {
        return Task.FromResult(_cow ?? new Cow()
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

    public void SetCow(Cow cow) => _cow = cow;
}