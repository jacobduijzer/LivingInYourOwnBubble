using CattleInformationSystem.Domain;
using CattleInformationSystem.Infrastructure;
using CattleInformationSystem.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        services.AddSingleton<IFarmRepository, FarmRepository>();
        services.AddSingleton<DataTester>();
    })
    .Build();

var dataTester = host.Services.GetService<DataTester>();
await dataTester.Test();

await host.RunAsync();