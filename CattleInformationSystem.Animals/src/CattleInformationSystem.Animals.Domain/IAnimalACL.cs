namespace CattleInformationSystem.Animals.Domain;

public interface IAnimalACL
{
    Task<Animal> ByLifeNumber(string lifeNumber);
    Task Save(Animal animal);
    Task Update(Animal animal);
}