using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class BirthHandler : IReasonHandler
{
    private readonly IAnimalRepository _animals;
    private readonly AnimalBirthFactory _factory;

    public BirthHandler(
        IAnimalRepository animals,
        IReadOnlyCollection<Farm> farms, 
        AnimalCategoryDeterminationService categoryDetermination)
    {
        _animals = animals;
        _factory = new AnimalBirthFactory(farms, categoryDetermination);
    }
    
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var newAnimal = _factory.Create(incomingAnimalEvent);
        await _animals.Save(newAnimal);
    }
}