using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application;

public class AnimalBirthFactory(
    IEnumerable<Farm> farms,
    AnimalCategoryDeterminationService animalCategoryDeterminationService)
{
    public Animal PerformBirth(IncomingAnimalEventCreated incomingAnimalEventCreated)
    {
        var animal = Animal.CreateNew(incomingAnimalEventCreated.LifeNumber, incomingAnimalEventCreated.Gender, incomingAnimalEventCreated.DateOfBirth);
        
        var currentFarm = farms.First(farm => farm.UBN.Equals(incomingAnimalEventCreated.CurrentUbn));
        animal.AddAnimalLocation(currentFarm.UBN, incomingAnimalEventCreated.DateOfBirth);
        
        var category = animalCategoryDeterminationService.DeterminateCurrent(animal, currentFarm.FarmType, incomingAnimalEventCreated.DateOfBirth);
        animal.AddAnimalEvent(currentFarm.UBN, incomingAnimalEventCreated.Reason, incomingAnimalEventCreated.DateOfBirth, category, null);

        return animal;
    }
}