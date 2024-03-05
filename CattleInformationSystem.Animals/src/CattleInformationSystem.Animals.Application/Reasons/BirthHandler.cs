using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class BirthHandler(
    IAnimalACL animalsAcl,
    IEnumerable<Farm> farms,
    AnimalCategoryDeterminationService categoryDetermination)
    : IReasonHandler
{
    private readonly AnimalBirthFactory _animalBirthFactory = new(farms, categoryDetermination);

    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var newAnimal = _animalBirthFactory.PerformBirth(incomingAnimalEvent);
        await animalsAcl.Save(newAnimal);
    }
}