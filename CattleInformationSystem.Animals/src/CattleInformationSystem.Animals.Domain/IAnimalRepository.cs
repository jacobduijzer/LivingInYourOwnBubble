namespace CattleInformationSystem.Animals.Domain;

public interface IAnimalRepository
{
    Task<Animal> ByLifeNumber(string lifeNumber);
    Task Save(Animal animal);
    Task Update(Animal animal);
}