using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class ReasonHandlerFactory
{
    private readonly IReadOnlyCollection<Farm> _farms;
    private readonly IAnimalACL _animals;
    private readonly AnimalCategoryDeterminationService _animalCategoryDeterminationService;

    public ReasonHandlerFactory(
        IReadOnlyCollection<AnimalCategory> animalCategories,
        IReadOnlyCollection<Farm> farms,
        IAnimalACL animals)
    {
        _farms = farms;
        _animals = animals;
        _animalCategoryDeterminationService = new AnimalCategoryDeterminationService(animalCategories);
    }

    public IReasonHandler CreateHandler(IncomingAnimalEventCreated incomingAnimalEvent) =>
        incomingAnimalEvent.Reason switch
        {
            Reason.Birth => new BirthHandler(_animals, _farms, _animalCategoryDeterminationService),
            Reason.Departure => throw new NotImplementedException($"The handler for '{Reason.Departure}' is not implemented yet."),
            Reason.Calved => new CalvedHandler(_animals, _farms, _animalCategoryDeterminationService),
            Reason.Death => new DeathHandler(_animals, _farms),
            _ => throw new NotImplementedException($"A handler for reason '{incomingAnimalEvent.Reason}' is not implemented.")
        };
}