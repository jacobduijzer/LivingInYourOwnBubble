using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application;

public class AnimalBirthFactory
{
    private readonly IEnumerable<Farm> _farms;
    private readonly AnimalCategoryDeterminationService _categories;

    public AnimalBirthFactory(
        IEnumerable<Farm> farms,
        AnimalCategoryDeterminationService animalCategoryDeterminationService)
    {
        _farms = farms;
        _categories = animalCategoryDeterminationService;
    }

    public Animal PerformBirth(IncomingAnimalEventCreated incomingAnimalEventCreated)
    {
        var animal = Animal.CreateNew(incomingAnimalEventCreated.LifeNumber, incomingAnimalEventCreated.Gender, incomingAnimalEventCreated.DateOfBirth);
        
        var currentFarm = _farms.First(farm => farm.UBN.Equals(incomingAnimalEventCreated.CurrentUbn));
        animal.AddAnimalLocation(currentFarm.UBN, incomingAnimalEventCreated.DateOfBirth);
        
        var category = _categories.DeterminateCurrent(animal, currentFarm.FarmType, incomingAnimalEventCreated.DateOfBirth);
        animal.AddAnimalEvent(currentFarm.UBN, incomingAnimalEventCreated.Reason, incomingAnimalEventCreated.DateOfBirth, category, null);

        return animal;
    }
}