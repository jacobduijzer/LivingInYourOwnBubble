using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Worker;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("DatabaseConnection")!;
var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMqConnection");

builder.Services
    .AddDbContext<DatabaseContext>(options => options.UseNpgsql(databaseConnection))
    .AddLogging(config => config.AddSeq())
    .AddScoped<IncomingCowEventHandler>()
    .AddScoped<IAnimalACL, AnimalAcl>()
    .AddScoped<ICowRepository, CowRepository>()
    .AddScoped<IFarmRepository, FarmRepository>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddMassTransit(busConfig =>
    {
        busConfig
            .AddConsumer<IncomingCowEventConsumer, IncomingCowEventConsumerDefinition>()
            .Endpoint(with => with.Name = "IncomingCowEvents");

        busConfig.UsingRabbitMq((context, config) =>
        {
            config.Host(rabbitMqConnection);
            config.ConfigureEndpoints(context);
        });
    })
    .AddHostedService<Worker>();

var host = builder.Build();
host.Run();

public partial class Program { }