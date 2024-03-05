using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class CalvedHandler(
    IAnimalACL animals,
    IEnumerable<Farm> farms,
    AnimalCategoryDeterminationService categoryDetermination)
    : IReasonHandler
{
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var currentFarm = farms.First(farm => farm.UBN.Equals(incomingAnimalEvent.CurrentUbn));
        var animal = await animals.ByLifeNumber(incomingAnimalEvent.LifeNumber);
        
        animal.HandleFirstCalved(
            currentFarm, 
            incomingAnimalEvent.EventDate, 
            categoryDetermination);
        
        await animals.Update(animal);
    }
}