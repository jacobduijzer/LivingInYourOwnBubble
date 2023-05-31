using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CattleInformationSystem.Animals.Specs;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost");
                    config.ConfigureEndpoints(context);
                });
            });

            // TODO: settings
            services
                .AddDbContext<DatabaseContext>(options => options.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=CattleInformationDatabase;Pooling=true;"))
                .AddScoped<IAnimalACL, AnimalAcl>()
                .AddScoped<ICowRepository, CowRepository>()
                .AddScoped<IFarmRepository, FarmRepository>();
        });
    }

    protected override IHostBuilder CreateHostBuilder()
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureWebHostDefaults(b => b.Configure(app => { }));
        return builder;
    }
}