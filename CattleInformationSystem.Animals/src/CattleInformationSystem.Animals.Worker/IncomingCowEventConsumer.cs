using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.SharedKernel.Contracts;
using MassTransit;

namespace CattleInformationSystem.Animals.Worker;

public class IncomingCowEventConsumer(
    IncomingCowEventHandler incomingCowEventHandler,
    ILogger<IncomingCowEventConsumer> logger)
    : IConsumer<IncomingAnimalEventCreated>
{
    public async Task Consume(ConsumeContext<IncomingAnimalEventCreated> context)
    {
        logger.LogInformation($"New event: {context.Message}");
        await incomingCowEventHandler.Handle(context.Message);
    }
}
