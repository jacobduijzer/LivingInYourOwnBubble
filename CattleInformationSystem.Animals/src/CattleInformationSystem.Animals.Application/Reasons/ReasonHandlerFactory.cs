using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class ReasonHandlerFactory
{
    private readonly IReadOnlyCollection<AnimalCategory> _animalCategories;
    private readonly IReadOnlyCollection<Farm> _farms;
    private readonly IAnimalACL _animals;

    public ReasonHandlerFactory(
        IReadOnlyCollection<AnimalCategory> animalCategories,
        IReadOnlyCollection<Farm> farms,
        IAnimalACL animals)
    {
        _animalCategories = animalCategories;
        _farms = farms;
        _animals = animals;
    }

    public IReasonHandler CreateHandler(IncomingAnimalEventCreated incomingAnimalEvent) =>
        incomingAnimalEvent.Reason switch
        {
            Reason.Birth => new BirthHandler(_animals, _farms, new AnimalCategoryDeterminationService(_animalCategories)),
            Reason.Departure => throw new NotImplementedException($"A handler for reason '{incomingAnimalEvent.Reason}' is not implemented."),
            Reason.Calved => throw new NotImplementedException($"A handler for reason '{incomingAnimalEvent.Reason}' is not implemented."),
            Reason.Death => new DeathHandler(_animals, _farms),
            _ => throw new NotImplementedException($"A handler for reason '{incomingAnimalEvent.Reason}' is not implemented.")
        };
}