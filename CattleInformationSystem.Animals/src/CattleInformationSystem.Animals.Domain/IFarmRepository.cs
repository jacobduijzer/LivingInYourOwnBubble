namespace CattleInformationSystem.Animals.Domain;

public interface IFarmRepository
{
    Task<IReadOnlyCollection<Farm>> ByUbns(string[] ubns);
    Task<IReadOnlyCollection<Farm>> ByFarmIds(int[] farmIds);
}