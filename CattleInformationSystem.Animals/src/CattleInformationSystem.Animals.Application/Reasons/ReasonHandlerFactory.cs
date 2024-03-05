using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class ReasonHandlerFactory(
    IEnumerable<AnimalCategory> animalCategories,
    IEnumerable<Farm> farms,
    IAnimalAcl animals)
{
    private readonly AnimalCategoryDeterminationService _animalCategoryDeterminationService = new(animalCategories);

    public IReasonHandler CreateHandler(IncomingAnimalEventCreated incomingAnimalEvent) =>
        incomingAnimalEvent.Reason switch
        {
            Reason.Birth => new BirthHandler(animals, farms, _animalCategoryDeterminationService),
            Reason.Departure => throw new NotImplementedException($"The handler for '{Reason.Departure}' is not implemented yet."),
            Reason.Calved => new CalvedHandler(animals, farms, _animalCategoryDeterminationService),
            Reason.Death => new DeathHandler(animals, farms),
            _ => throw new NotImplementedException($"A handler for reason '{incomingAnimalEvent.Reason}' is not implemented.")
        };
}