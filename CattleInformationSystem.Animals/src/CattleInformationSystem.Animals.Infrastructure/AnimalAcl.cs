using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

namespace CattleInformationSystem.Animals.Infrastructure;

public class AnimalAcl : IAnimalACL
{
    private readonly IFarmRepository _farms;
    private readonly ICowRepository _cows;

    public AnimalAcl(
        ICowRepository cows,
        IFarmRepository farms)
    {
        _cows = cows;
        _farms = farms;
    }

    public async Task<Animal> ByLifeNumber(string lifeNumber)
    {
        // ACL => Map Cow to Animal, FarmCow to AnimalLocation, CowEvent to AnimalEvent
        var cow = await _cows.ByLifeNumber(lifeNumber);

        var farms = await _farms.ByFarmIds(CreateFarmIdList(cow));

        var animal = Animal.CreateExisting(cow.LifeNumber, cow.Gender, cow.DateOfBirth, cow.DateFirstCalved ?? null,
            cow.DateOfDeath ?? null);

        foreach (var ce in cow.CowEvents)
        {
            var farm = farms.Single(f => f.Id.Equals(ce.FarmId));
            animal.AddAnimalEvent(farm.UBN, ce.Reason, ce.EventDate, ce.Category);
        }

        foreach (var fc in cow.FarmCows)
        {
            var farm = farms.Single(x => x.Id.Equals(fc.FarmId));
            animal.AddAnimalLocation(farm.UBN, fc.StartDate, fc.EndDate ?? null);
        }

        return animal;
    }

    public async Task Save(Animal animal)
    {
        // ACL => Map Animal to Cow, AnimalLocation to FarmCow, AnimalEvent to CowEvent
        var farms = await _farms.ByUbns(CreateUbnList(animal));

        var cow = new Cow
        {
            LifeNumber = animal.LifeNumber,
            Gender = animal.Gender,
            DateOfBirth = animal.DateOfBirth,
            DateFirstCalved = animal.DateFirstCalved,
            DateOfDeath = animal.DateOfDeath,
            CowEvents = animal.AnimalEvents.Select(ce => new CowEvent
            {
                FarmId = farms.Single(f => f.UBN.Equals(ce.Ubn)).Id,
                Reason = ce.Reason,
                EventDate = ce.EventDate,
                Category = ce.Category,
                Order = ce.Order
            }).ToList(),
            FarmCows = animal.AnimalLocations.Select(al => new FarmCow
            {
                FarmId = farms.Single(f => f.UBN.Equals(al.Ubn)).Id,
                StartDate = al.StartDate
            }).ToList()
        };

        await _cows.Save(cow);
    }

    public async Task Update(Animal animal)
    {
        // ACL => Map Animal to Cow, AnimalLocation to FarmCow, AnimalEvent to CowEvent
        var farms = await _farms.ByUbns(CreateUbnList(animal));

        var cow = await _cows.ByLifeNumber(animal.LifeNumber);
        cow.DateFirstCalved = animal.DateFirstCalved ?? null;
        cow.DateOfDeath = animal.DateOfDeath ?? null;

        foreach (var animalLocation in animal.AnimalLocations)
        {
            var farmId = farms.Single(farm => farm.UBN.Equals(animalLocation.Ubn)).Id;
            var farmCow = cow.FarmCows.FirstOrDefault(fc =>
                fc.FarmId.Equals(farmId) && fc.StartDate.Equals(animalLocation.StartDate));
            if (farmCow == null)
            {
                cow.FarmCows.Add(new()
                {
                    FarmId = farmId,
                    StartDate = animalLocation.StartDate,
                    EndDate = animalLocation.EndDate ?? null
                });
            }
            else if (animalLocation.EndDate.HasValue && !farmCow.EndDate.HasValue)
            {
                farmCow.EndDate = animalLocation.EndDate;
            }
        }

        foreach (var animalEvent in animal.AnimalEvents)
        {
            var farmId = farms.Single(farm => farm.UBN.Equals(animalEvent.Ubn)).Id;
            if (!cow.CowEvents.Any(ce =>
                    ce.FarmId.Equals(farmId) &&
                    ce.Reason.Equals(animalEvent.Reason) &&
                    ce.EventDate.Equals(animalEvent.EventDate) &&
                    ce.Category.Equals(animalEvent.Category) &&
                    ce.Order.Equals(animalEvent.Order)))
            {
                cow.CowEvents.Add(new()
                {
                    FarmId = farmId,
                    Reason = animalEvent.Reason,
                    EventDate = animalEvent.EventDate,
                    Category = animalEvent.Category,
                    Order = animalEvent.Order
                });
            }
        }

        await _cows.Update(cow);
    }

    private string[] CreateUbnList(Animal animal)
    {
        var eventUbns = animal.AnimalEvents.Select(ae => ae.Ubn).Distinct();
        var locationUbns = animal.AnimalLocations.Select(al => al.Ubn).Distinct();
        return eventUbns.Union(locationUbns).ToArray();
    }

    private int[] CreateFarmIdList(Cow cow)
    {
        var eventFarmIds = cow.CowEvents.Select(ce => ce.FarmId).Distinct();
        var locationFarmIds = cow.FarmCows.Select(fc => fc.FarmId).Distinct();
        return eventFarmIds.Union(locationFarmIds).ToArray();
    }
}