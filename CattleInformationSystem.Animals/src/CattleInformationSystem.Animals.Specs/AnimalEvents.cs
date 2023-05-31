using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CattleInformationSystem.Animals.Specs;

[Binding]
public class AnimalEvents : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private IEnumerable<IncomingAnimalEventCreated> _incomingEvents;
    private string _lifeNumber;
    private readonly IAsyncPolicy _retryPolicy;
    private Animal _animal;

    public AnimalEvents(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Service.Instance.ValueRetrievers.Register(new DateOnlyValueRetriever());
    }

    [Given(@"the following event\(s\)")]
    public void GivenTheFollowingEvent(Table table)
    {
        _incomingEvents = table.CreateSet<IncomingAnimalEventCreated>();
    }

    [When(@"added to the queue")]
    public async Task WhenAddedToTheQueue()
    {
        _lifeNumber = DateTime.Now.ToString("yyyyddMMhhmmssfff");
        await using var scope = _factory.Services.CreateAsyncScope();
        IBus bus = scope.ServiceProvider.GetRequiredService<IBus>();
        foreach (var animalEvent in _incomingEvents.OrderBy(x => x.EventDate))
        {
            await bus.Publish<IncomingAnimalEventCreated>(animalEvent with {LifeNumber = _lifeNumber});
            await Task.Delay(500);
        }
    }

    [Then(@"it will be processed and added to the legacy database")]
    public async Task ThenItWillBeProcessedAndAddedToTheLegacyDatabase()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var animals = scope.ServiceProvider.GetRequiredService<IAnimalACL>();

        _animal = await _retryPolicy.ExecuteAsync(async () => await animals.ByLifeNumber(_lifeNumber));

        Assert.NotNull(_animal);
        Assert.True(_animal.LifeNumber.Equals(_lifeNumber));
    }

    [Then(@"have the events\(s\)")]
    public void ThenHaveTheEventsS(Table table)
    {
        Assert.True(_animal.AnimalEvents.Count() == table.RowCount,
            $"The number of animal events for animal '{_lifeNumber}' is not the same as the expected events. Expected: {table.RowCount}, found: {_animal.AnimalEvents.Count}");

        foreach (var dataRow in table.Rows)
        {
            Assert.True(_animal.AnimalEvents.Count(ce =>
                    ce.Ubn.Equals(dataRow["Farm"]) &&
                    ce.Reason.Equals(Enum.Parse<Reason>(dataRow["Reason"])) &&
                    ce.Order.Equals(int.Parse(dataRow["Order"])) &&
                    ce.EventDate.Equals(DateOnly.Parse(dataRow["Date"])) &&
                    ce.Category.Equals(int.Parse(dataRow["Category"]))) == 1,
                $"Animal Event not found for animal '{_lifeNumber}': {dataRow["Farm"]} - {dataRow["Reason"]} - {dataRow["Order"]} - {dataRow["Date"]} - {dataRow["Category"]}");
        }
    }
}