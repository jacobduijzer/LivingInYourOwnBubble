using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class DeathHandler(
    IAnimalAcl animals,
    IEnumerable<Farm> farms) : IReasonHandler
{
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var currentFarm = farms.First(farm => farm.UBN.Equals(incomingAnimalEvent.CurrentUbn));
        
        var deadAnimal = await animals.ByLifeNumber(incomingAnimalEvent.LifeNumber);
        deadAnimal.HandleDeath(currentFarm.UBN, incomingAnimalEvent.EventDate);
        
        await animals.Update(deadAnimal);
    }
}