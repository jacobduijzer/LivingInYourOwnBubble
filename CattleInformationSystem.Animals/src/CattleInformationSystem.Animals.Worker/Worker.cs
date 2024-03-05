using MassTransit;

namespace CattleInformationSystem.Animals.Worker;

public class Worker(
    IBusControl busControl,
    ILogger<Worker> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"Starting the bus for {nameof(IncomingCowEventConsumer)}...");
        return busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"Stopping the bus for {nameof(IncomingCowEventConsumer)}...");
        return busControl.StopAsync(cancellationToken);
    }
}