using CattleInformationSystem.Animals.Domain;
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

    public AnimalEvents(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        
        _retryPolicy  = Policy
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
        foreach (var animalEvent in _incomingEvents)
            await bus.Publish<IncomingAnimalEventCreated>(animalEvent with { LifeNumber = _lifeNumber });
    }

    [Then(@"it will be processed and added to the legacy database")]
    public async Task ThenItWillBeProcessedAndAddedToTheLegacyDatabase()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var animals = scope.ServiceProvider.GetRequiredService<IAnimalACL>();

        var animal = await _retryPolicy.ExecuteAsync(async () => await animals.ByLifeNumber(_lifeNumber));
        
        Assert.NotNull(animal);
        Assert.True(animal.LifeNumber.Equals(_lifeNumber));
        Assert.True(animal.AnimalEvents.Count.Equals(_incomingEvents.Count()));
    }
}