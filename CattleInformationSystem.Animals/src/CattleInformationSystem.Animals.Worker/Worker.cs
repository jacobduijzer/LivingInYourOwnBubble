using MassTransit;

namespace CattleInformationSystem.Animals.Worker;

public class Worker : IHostedService
{
    private readonly IBusControl _busControl;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IBusControl busControl,
        ILogger<Worker> logger)
    {
        _busControl = busControl;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting the bus for {nameof(IncomingCowEventConsumer)}...");
        return _busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Stopping the bus for {nameof(IncomingCowEventConsumer)}...");
        return _busControl.StopAsync(cancellationToken);
    }
}