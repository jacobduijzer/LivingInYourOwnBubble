using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application;

public class IncomingCowEventHandler
{
    private readonly IAnimalACL _animals;
    private readonly IFarmRepository _farms;
    private readonly ICategoryRepository _categories;

    public IncomingCowEventHandler(
        IAnimalACL animalAcl,
        ICategoryRepository categoryRepository,
        IFarmRepository farms)
    {
        _animals = animalAcl;
        _categories = categoryRepository;
        _farms = farms;
    }

    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        // ACL: GET NEEDED DATA FROM LEGACY
        var animalCategories = await _categories.All();
        var farms = await _farms.ByUbns(CreateUbnList(incomingAnimalEvent));

        // HANDLE INCOMING EVENT
        ReasonHandlerFactory factory = new(animalCategories, farms, _animals);
        var incomingEventHandler = factory.CreateHandler(incomingAnimalEvent);
        await incomingEventHandler.Handle(incomingAnimalEvent);
    }

    private string[] CreateUbnList(IncomingAnimalEventCreated incomingAnimalEventCreated)
    {
        if (!string.IsNullOrEmpty(incomingAnimalEventCreated.TargetUbn))
            return new[] {incomingAnimalEventCreated.CurrentUbn, incomingAnimalEventCreated.TargetUbn};

        return new[] {incomingAnimalEventCreated.CurrentUbn};
    }
}