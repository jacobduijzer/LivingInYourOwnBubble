using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class DepartureHandler(
    IAnimalAcl animals,
    IEnumerable<Farm> farms,
    AnimalCategoryDeterminationService categoryDetermination)
    : IReasonHandler
{
    private readonly IAnimalAcl _animals = animals;
    private readonly IEnumerable<Farm> _farms = farms;
    private readonly AnimalCategoryDeterminationService _categoryDetermination = categoryDetermination;

    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        throw new NotImplementedException();
    }
}