using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class DepartureHandler : IReasonHandler
{
    private readonly IAnimalACL _animals;
    private readonly IReadOnlyCollection<Farm> _farms;
    private readonly AnimalCategoryDeterminationService _categoryDetermination;

    public DepartureHandler(
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
        throw new NotImplementedException();
    }
}