namespace CattleInformationSystem.Animals.Domain;

public interface IAnimalAcl
{
    Task<Animal> ByLifeNumber(string lifeNumber);
    Task Save(Animal animal);
    Task Update(Animal animal);
}