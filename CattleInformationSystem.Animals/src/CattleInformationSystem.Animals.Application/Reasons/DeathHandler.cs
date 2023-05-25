using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class DeathHandler : IReasonHandler
{
    private readonly IAnimalRepository _animals;
    private readonly IReadOnlyCollection<Farm> _farms;

    public DeathHandler(
        IAnimalRepository animals,
        IReadOnlyCollection<Farm> farms)
    {
        _animals = animals;
        _farms = farms;
    }
        
    public async Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        var currentFarm = _farms.First(farm => farm.UBN.Equals(incomingAnimalEvent.CurrentUbn));
        
        var deadAnimal = await _animals.ByLifeNumber(incomingAnimalEvent.LifeNumber);
        deadAnimal.HandleDeath(currentFarm.UBN, incomingAnimalEvent.EventDate);
        
        await _animals.Update(deadAnimal);
    }
}