using MassTransit;

namespace CattleInformationSystem.Animals.Worker;

public class IncomingCowEventConsumerDefinition : ConsumerDefinition<IncomingCowEventConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<IncomingCowEventConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
        endpointConfigurator.UseInMemoryOutbox();
        endpointConfigurator.ConcurrentMessageLimit = 1;
    }
}