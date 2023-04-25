namespace CattleInformationSystem.Domain;

public interface IFarmCowRepository
{
    Task Add(FarmCow farmCow);

    Task Update(FarmCow farmCow);
}