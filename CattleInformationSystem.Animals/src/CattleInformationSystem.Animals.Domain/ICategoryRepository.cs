namespace CattleInformationSystem.Animals.Domain;

public interface ICategoryRepository
{
   Task<IReadOnlyCollection<AnimalCategory>> All();
}