using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CattleInformationSystem.Specs;

[Binding]
public class IncomingAnimalEvents : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    private IncomingCowEvent _incomingCowEvent;
    private Farm _targetFarm;
    private Cow? _cow;
    private static string _lifeNumber;
    private IEnumerable<IncomingCowEvent> _incomingEvents;

    public IncomingAnimalEvents(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Service.Instance.ValueRetrievers.Register(new DateOnlyValueRetriever());
        _lifeNumber = DateTime.Now.ToString("yyyyddMMhhmmssfff");
    }

    [Given(@"the following event\(s\)")]
    public void GivenTheFollowingEventS(Table table)
    {
        _incomingEvents = table.CreateSet<IncomingCowEvent>(ConvertToIncomingCowEvent);
    }

    [When(@"it is added to the incoming events table")]
    public async Task WhenItIsAddedToTheIncomingEventsTable()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var incomingCowEvents = scope.ServiceProvider.GetRequiredService<IIncomingCowEventRepository>();
        foreach (var incomingEvent in _incomingEvents)
        {
            await incomingCowEvents.Add(incomingEvent);
            await Task.Delay(50);
        }
    }

    [Then(@"it should be processed and stored in the database")]
    public async Task ThenItShouldBeStoredInTheDatabase()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var cows = scope.ServiceProvider.GetRequiredService<ICowRepository>();
        _cow = await cows.ByLifeNumber(_lifeNumber);

        Assert.True(_cow != null,
            $"A cow with life number {_lifeNumber} is not available in the database.");
    }

    [Then(@"have the events\(s\)")]
    public async Task ThenHaveTheEvents(Table table)
    {
        Assert.True(_cow.CowEvents.Count() == table.RowCount,
            "The number of cow events is not the same as the expected events.");

        foreach (var dataRow in table.Rows)
        {
            var cowEvent = _cow.CowEvents.FirstOrDefault(ce =>
                ce.Farm.UBN.Equals(dataRow["Ubn"]) &&
                ce.Reason.Equals(Enum.Parse<Reason>(dataRow["Reason"])) &&
                ce.Order.Equals(int.Parse(dataRow["Order"])) &&
                ce.EventDate.Equals(DateOnly.Parse(dataRow["Date"])) &&
                ce.Category.Equals(int.Parse(dataRow["Category"])));

            Assert.True(cowEvent != null,
                $"Cow Event not found: {dataRow["Ubn"]} - {dataRow["Reason"]} - {dataRow["Order"]} - {dataRow["Date"]} - {dataRow["Category"]}");
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
                fc.Farm.UBN.Equals(dataRow["Ubn"]) &&
                fc.StartDate.Equals(DateOnly.Parse(dataRow["StartDate"])) &&
                CheckForEndDate(fc, dataRow["EndDate"]));

            Assert.True(location != null,
                $"Location not found: {dataRow["Ubn"]} - {dataRow["StartDate"]} - {dataRow["EndDate"]}");
        }
        
        bool CheckForEndDate(FarmCow farmCow, string endDate) =>
            !string.IsNullOrEmpty(endDate) ?
                farmCow.EndDate.HasValue && farmCow.EndDate.Value.Equals(DateOnly.Parse(endDate)) :
                !farmCow.EndDate.HasValue;
    }

    [Then(@"the animal should have a date of death of '(.*)'")]
    public void ThenTheAnimalShouldHaveADateOfDeathOf(string dateOfDeath)
    {
        Assert.True(_cow!.DateOfDeath.HasValue, "Date of death is not set.");
        Assert.True(_cow!.DateOfDeath.Value.Equals(DateOnly.Parse(dateOfDeath)), "Date of death is incorrect.");
    }
    
    [Then(@"the animal should have a date first calved of '(.*)'")]
    public void ThenTheAnimalShouldHaveADateFirstCalvedOf(string dateFirstCalved)
    {
        Assert.True(_cow!.DateFirstCalved.HasValue, "Date first calved is not set.");
        Assert.True(_cow!.DateFirstCalved.Value.Equals(DateOnly.Parse(dateFirstCalved)));
    }

    private static IncomingCowEvent ConvertToIncomingCowEvent(TableRow row) =>
        new IncomingCowEvent()
        {
            LifeNumber = _lifeNumber,
            Gender = Enum.Parse<Gender>(row["Gender"]),
            DateOfBirth = DateOnly.Parse(row["DateOfBirth"]),
            Reason = Enum.Parse<Reason>(row["Reason"]),
            UBN_1 = row["CurrentUbn"],
            UBN_2 = !string.IsNullOrEmpty(row["TargetUbn"]) ? row["TargetUbn"] : null,
            EventDate = DateOnly.Parse(row["EventDate"])
        };

    
}