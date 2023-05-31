using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class BirthHandler : IReasonHandler
{
    private readonly IAnimalACL _animalsACL;
    private readonly AnimalBirthFactory _animalBirthFactory;

    public BirthHandler(
        IAnimalACL animalsAcl,
        IReadOnlyCollection<Farm> farms, 
        AnimalCategoryDeterminationService categoryDetermination)
    {
        _animalsACL = animalsAcl;
        _animalBirthFactory = new AnimalBirthFactory(farms, categoryDetermination);
    }
    
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var newAnimal = _animalBirthFactory.PerformBirth(incomingAnimalEvent);
        await _animalsACL.Save(newAnimal);
    }
}