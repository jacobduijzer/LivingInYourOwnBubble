using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public class Animal : IAggregateRoot
{
    public string LifeNumber { get; private set; }
    public Gender Gender { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public DateOnly? DateFirstCalved { get; private set; }
    public DateOnly? DateOfDeath { get; private set; }
    public List<AnimalLocation> AnimalLocations { get; } = new();
    public List<AnimalEvent> AnimalEvents { get; } = new();

    public static Animal CreateNew(string lifenumber, Gender gender, DateOnly dateOfBirth) => new Animal
    {
        LifeNumber = lifenumber,
        Gender = gender,
        DateOfBirth = dateOfBirth,
    };

    public static Animal CreateExisting(string lifenumber, Gender gender, DateOnly dateOfBirth,
        DateOnly? dateFirstCalved, DateOnly? dateOfDeath) => new Animal
    {
        LifeNumber = lifenumber,
        Gender = gender,
        DateOfBirth = dateOfBirth,
        DateFirstCalved = dateFirstCalved,
        DateOfDeath = dateOfDeath
    };

    public int AgeInDays(DateOnly eventDate) => eventDate.DayNumber - DateOfBirth.DayNumber;

    public void AddAnimalLocation(string ubn, DateOnly startDate, DateOnly? endDate = null)
    {
        if (startDate < DateOfBirth)
            throw new Exception("The date of birth should be before or equal to the date of arrival.");

        var animalLocation = new AnimalLocation(ubn, startDate);
        if (endDate.HasValue)
            animalLocation.SetEndDate(endDate.Value);

        AnimalLocations.Add(animalLocation);
    }

    public void AddAnimalEvent(string ubn, Reason reason, DateOnly eventDate, int category, int? order) =>
        AnimalEvents.Add(new(ubn, reason, eventDate, category, order.HasValue ? order.Value : GetNextOrder(ubn, eventDate)));

    public void HandleFirstCalved(Farm farm, DateOnly eventDate, AnimalCategoryDeterminationService categoryDeterminationService)
    {
        DateFirstCalved = eventDate;

        var category = categoryDeterminationService.DeterminateCurrent(this, farm.FarmType, eventDate);
        AddAnimalEvent(farm.UBN, Reason.Calved, eventDate, category, null);
    }

    public void HandleDeath(string ubn, DateOnly eventDate)
    {
        DateOfDeath = eventDate;

        AddAnimalEvent(ubn, Reason.Death, eventDate, GetLastCategory(ubn), null);

        SetEndDateForLocation(ubn, eventDate);
    }

    private int GetNextOrder(string ubn, DateOnly eventDate)
    {
        if (AnimalEvents.Any(ev => ev.Ubn.Equals(ubn) && ev.EventDate.Equals(eventDate)))
            return AnimalEvents
                .Where(ev => ev.Ubn.Equals(ubn))
                .OrderByDescending(ev => ev.EventDate)
                .ThenByDescending(ev => ev.Order)
                .First().Order + 1;
        return 0;
    }

    private int GetLastCategory(string ubn) =>
        AnimalEvents
            .Where(ae => ae.Ubn.Equals(ubn))
            .OrderByDescending(ev => ev.EventDate)
            .ThenByDescending(ev => ev.Order)
            .First()
            .Category;

    private void SetEndDateForLocation(string ubn, DateOnly eventDate)
    {
        var animalLocation = AnimalLocations.First(loc =>
            loc.Ubn.Equals(ubn) && !loc.EndDate.HasValue);
        AnimalLocations.Remove(animalLocation);

        animalLocation.SetEndDate(eventDate);
        AnimalLocations.Add(animalLocation);
    }

    public async Task HandleDepartureAndArrivalEvent(Farm currentFarm, Farm destinationFarm, DateOnly eventDate, AnimalCategoryDeterminationService categoryDetermination)
    {
        SetEndDateForLocation(currentFarm.UBN, eventDate);
        
        var animalLocation = new AnimalLocation(destinationFarm.UBN, eventDate);
        AnimalLocations.Add(animalLocation);

        var lastCategory = GetLastCategory(currentFarm.UBN);
        AddAnimalEvent(currentFarm.UBN, Reason.Departure, eventDate, lastCategory, null);

        var newCategory = categoryDetermination.DeterminateCurrent(this, destinationFarm.FarmType, eventDate);
        AddAnimalEvent(destinationFarm.UBN, Reason.Arrival, eventDate, newCategory, null);
    }
}