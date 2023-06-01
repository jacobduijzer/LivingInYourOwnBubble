using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class CalvedHandler : IReasonHandler
{
    private readonly IAnimalACL _animals;
    private readonly IReadOnlyCollection<Farm> _farms;
    private readonly AnimalCategoryDeterminationService _categoryDetermination;

    public CalvedHandler(
        IAnimalACL animals,
        IReadOnlyCollection<Farm> farms,
        AnimalCategoryDeterminationService categoryDetermination)
    {
        _animals = animals;
        _farms = farms;
        _categoryDetermination = categoryDetermination;
    }

    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var currentFarm = _farms.First(farm => farm.UBN.Equals(incomingAnimalEvent.CurrentUbn));
        var animal = await _animals.ByLifeNumber(incomingAnimalEvent.LifeNumber);
        
        animal.HandleFirstCalved(
            currentFarm, 
            incomingAnimalEvent.EventDate, 
            _categoryDetermination);
        
        await _animals.Update(animal);
    }
}