using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using MassTransit;

namespace CattleInformationSystem.Animals.Worker;

public class IncomingCowEventConsumer : IConsumer<IncomingAnimalEventCreated>
{
    private readonly IncomingCowEventHandler _incomingCowEventHandler;
    private readonly ILogger<IncomingCowEventConsumer> _logger;

    public IncomingCowEventConsumer(
        IncomingCowEventHandler incomingCowEventHandler,
        ILogger<IncomingCowEventConsumer> logger
        )
    {
        _incomingCowEventHandler = incomingCowEventHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IncomingAnimalEventCreated> context)
    {
        _logger.LogInformation($"New event: {context.Message}");
        await _incomingCowEventHandler.Handle(context.Message);
    }
}
