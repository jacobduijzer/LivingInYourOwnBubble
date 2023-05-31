namespace CattleInformationSystem.Domain;

public interface ICowRepository
{
   Task AddRange(List<Cow> cows);

   Task<List<Cow?>> All();

   Task<Cow?> ById(int id);

   Task<Cow?> ByLifeNumber(string lifeNumber);
}