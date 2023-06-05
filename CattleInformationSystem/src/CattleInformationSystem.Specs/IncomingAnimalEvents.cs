using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace CattleInformationSystem.Specs;

[Binding]
public class IncomingAnimalEvents : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    private IncomingCowEvent _incomingCowEvent;
    private Farm _targetFarm;
    private Cow? _cow;

    public IncomingAnimalEvents(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Given(@"a farm of type '(.*)', with UBN '(.*)'")]
    public async Task GivenAFarmOfType(FarmType farmType, string ubn)
    {
        Farm farm = new Farm
        {
            UBN = ubn,
            FarmType = farmType
        };

        await using var scope = _factory.Services.CreateAsyncScope();
        var farms = scope.ServiceProvider.GetRequiredService<IFarmRepository>();
        await farms.AddOrUpdate(farm);
    }

    [Given(@"an animal, born today")]
    public async Task GivenAnAnimalBornToday(Table table)
    {
        var incomingCowEvent = Extensions.ToDictionary(table);

        _incomingCowEvent = new IncomingCowEvent
        {
            LifeNumber = incomingCowEvent["Life Number"],
            Gender = Enum.Parse<Gender>(incomingCowEvent["Gender"]),
            DateOfBirth = DateOnly.Parse(incomingCowEvent["EventDate"]),
            Reason = Enum.Parse<Reason>(incomingCowEvent["Reason"]),
            UBN_1 = incomingCowEvent["Ubn"],
            EventDate = DateOnly.Parse(incomingCowEvent["EventDate"])
        };
    }

    [Given(@"it is added to the incoming events table")]
    [When(@"it is added to the incoming events table")]
    public async Task WhenItIsAddedToTheIncomingEventsTable()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var incomingCowEvents = scope.ServiceProvider.GetRequiredService<IIncomingCowEventRepository>();
        await incomingCowEvents.Add(_incomingCowEvent);
    }

    [When(@"it is moved on '(.*)', from UBN '(.*)' to UBN '(.*)'")]
    public async Task WhenItIsSoldOnToUbn(string eventDate, string fromUbn, string toUbn)
    {
        _incomingCowEvent = new IncomingCowEvent
        {
            LifeNumber = _incomingCowEvent.LifeNumber,
            Gender = _incomingCowEvent.Gender,
            DateOfBirth = _incomingCowEvent.DateOfBirth,
            Reason = Reason.Departure,
            UBN_1 = fromUbn,
            UBN_2 = toUbn,
            EventDate = DateOnly.Parse(eventDate)
        };

        await using var scope = _factory.Services.CreateAsyncScope();
        var incomingCowEvents = scope.ServiceProvider.GetRequiredService<IIncomingCowEventRepository>();
        await incomingCowEvents.Add(_incomingCowEvent);
    }
    
    [When(@"it gave birth on '(.*)' on UBN '(.*)'")]
    public async Task WhenItGaveBirthOnOnUbn(string eventDate, string ubn)
    {
        _incomingCowEvent = new IncomingCowEvent
        {
            LifeNumber = _incomingCowEvent.LifeNumber,
            Gender = _incomingCowEvent.Gender,
            Reason = Reason.Calved,
            UBN_1 = ubn,
            EventDate = DateOnly.Parse(eventDate)
        };

        await using var scope = _factory.Services.CreateAsyncScope();
        var incomingCowEvents = scope.ServiceProvider.GetRequiredService<IIncomingCowEventRepository>();
        await incomingCowEvents.Add(_incomingCowEvent);
    }
    
    [When(@"it died on '(.*)' on UBN '(.*)'")]
    public async Task WhenItDiedOnOnUbn(string eventDate, string ubn)
    {
        _incomingCowEvent = new IncomingCowEvent
        {
            LifeNumber = _incomingCowEvent.LifeNumber,
            Gender = _incomingCowEvent.Gender,
            Reason = Reason.Death,
            UBN_1 = ubn,
            EventDate = DateOnly.Parse(eventDate)
        };

        await using var scope = _factory.Services.CreateAsyncScope();
        var incomingCowEvents = scope.ServiceProvider.GetRequiredService<IIncomingCowEventRepository>();
        await incomingCowEvents.Add(_incomingCowEvent);
    }

    [Then(@"it should be processed and stored in the database")]
    public async Task ThenItShouldBeStoredInTheDatabase()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var cows = scope.ServiceProvider.GetRequiredService<ICowRepository>();
        _cow = await cows.ByLifeNumber(_incomingCowEvent.LifeNumber);

        Assert.True(_cow != null,
            $"A cow with lifenumber {_incomingCowEvent.LifeNumber} is not available in the database.");
    }

    [Then(@"have the events\(s\)")]
    public async Task ThenHaveTheEvents(Table table)
    {
        Assert.True(_cow.CowEvents.Count() == table.RowCount,
            "The number of cow events is not the same as the expected events.");

        foreach (var dataRow in table.Rows)
        {
            var cowEvent = _cow.CowEvents.FirstOrDefault(ce =>
                ce.Farm.UBN.Equals(dataRow["Farm"]) &&
                ce.Reason.Equals(Enum.Parse<Reason>(dataRow["Reason"])) &&
                ce.Order.Equals(int.Parse(dataRow["Order"])) && 
                ce.EventDate.Equals(DateOnly.Parse(dataRow["Date"])) &&
                ce.Category.Equals(int.Parse(dataRow["Category"])));
            
            Assert.True(cowEvent != null,
                $"Cow Event not found: {dataRow["Farm"]} - {dataRow["Reason"]} - {dataRow["Order"]} - {dataRow["Date"]} - {dataRow["Category"]}");
        }
    }
    
    [Then(@"have the location\(s\)")]
    public void ThenHaveTheLocationS(Table table)
    {
        Assert.True(_cow.FarmCows.Count() == table.RowCount,
            "The amount of locations is not the same as the expected amount.");
        
        foreach (var dataRow in table.Rows)
        {
            var location = _cow.FarmCows.FirstOrDefault(fc =>
                fc.Farm.UBN.Equals(dataRow["Farm"]) &&
                fc.StartDate.Equals(DateOnly.Parse(dataRow["StartDate"])) &&
                !string.IsNullOrEmpty(dataRow["EndDate"]) ? fc.EndDate.Value.Equals(DateOnly.Parse(dataRow["EndDate"])) : true);
            
            Assert.True(location != null,
                $"Location not found: {dataRow["Farm"]} - {dataRow["StartDate"]} - {dataRow["EndDate"]}");
        }
    }


    [Then(@"the cow should have a date of death of '(.*)'")]
    public void ThenTheCowShouldHaveADateOfDeathOf(string dateOfDeath)
    {
        Assert.True(_cow!.DateOfDeath.HasValue, "Date of death is not filled.");
        Assert.True(_cow!.DateOfDeath.Value.Equals(DateOnly.Parse(dateOfDeath)), "Date of death is incorrect.");
    }

    [Then(@"the end date on the latest location should be set to '(.*)'")]
    public void ThenTheEndDateOnTheLatestLocationShouldBeSetTo(string dateOfDeath)
    {
        Assert.True(_cow!.FarmCows.OrderByDescending(fc => fc.StartDate).First().EndDate.HasValue, "The end date of the last location has not been filled.");
        Assert.True(_cow!.FarmCows.OrderByDescending(fc => fc.StartDate).First().EndDate.Value.Equals(DateOnly.Parse(dateOfDeath)), "End date is incorrect.");
    }

    
}