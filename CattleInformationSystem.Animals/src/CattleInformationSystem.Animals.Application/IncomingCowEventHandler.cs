using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application;

public class IncomingCowEventHandler(
    IAnimalAcl animalAcl,
    ICategoryRepository categoryRepository,
    IFarmRepository farms)
{
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        // ACL: GET NEEDED DATA FROM LEGACY
        var animalCategories = await categoryRepository.All();
        var farms1 = await farms.ByUbns(CreateUbnList(incomingAnimalEvent));

        // HANDLE INCOMING EVENT
        ReasonHandlerFactory factory = new(animalCategories, farms1, animalAcl);
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