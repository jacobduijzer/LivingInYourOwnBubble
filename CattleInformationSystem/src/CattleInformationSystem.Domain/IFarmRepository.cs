namespace CattleInformationSystem.Domain;

public interface IFarmRepository
{
    Task AddRange(List<Farm> farms);

    Task<List<Farm>> All();

    Task<List<Farm>> ByType(FarmType farmType);

    Task<Farm> ById(int farmId);

    Task SaveChanges();
}