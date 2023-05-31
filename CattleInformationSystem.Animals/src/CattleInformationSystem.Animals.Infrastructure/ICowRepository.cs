using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

namespace CattleInformationSystem.Animals.Infrastructure;

public interface ICowRepository
{
    Task<Cow> ByLifeNumber(string lifeNumber);

    Task Save(Cow cow);
    Task Update(Cow cow);

}