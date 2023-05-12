using CattleInformationSystem.Domain;
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
    private string _ubn;

    public IncomingAnimalEvents(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Given(@"a farm of type '(.*)', with UBN '(.*)'")]
    public async Task GivenAFarmOfType(FarmType farmType, string ubn)
    {
        _ubn = ubn;
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
            DateOfBirth = DateTime.Parse(incomingCowEvent["EventDate"]),
            Reason = Enum.Parse<Reason>(incomingCowEvent["Reason"]),
            UBN_1 = _ubn,
            EventDate = DateTime.Parse(incomingCowEvent["EventDate"])
        };
    }

    [Given(@"it is added to the incoming events table")]
    public async Task WhenItIsAddedToTheIncomingEventsTable()
    {
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
    public void ThenHaveTheEvents(Table table)
    {
        var eventData = Extensions.ToDictionary(table);
        Assert.True(_cow.CowEvents.Count() == 1, "There are more than 1 events.");
        Assert.True(_cow.CowEvents.First().EventDate == DateTime.Parse(table.Rows[0]["EventDate"]));
        Assert.True(_cow.CowEvents.First().Reason == Enum.Parse<Reason>(table.Rows[0]["Reason"]));
        Assert.True(_cow.CowEvents.First().Category == int.Parse(table.Rows[0]["Category"]));
    }
}