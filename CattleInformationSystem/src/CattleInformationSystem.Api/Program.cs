using System.Text.Json.Serialization;
using CattleInformationSystem.Application;
using CattleInformationSystem.Domain;
using CattleInformationSystem.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ICowRepository, CowRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IFarmCowRepository, FarmCowRepository>();
builder.Services.AddScoped<IDataGenerator, FakeDataGenerator>();
builder.Services.AddScoped<CreateTestDataHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
//     dbContext.Database.EnsureDeleted();
//     dbContext.Database.Migrate();
// }

app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Cattle Information System - API"); });
app.MapPost("/create-test-data", async (CreateTestDataHandler createTestDataHandler) =>
{
    await createTestDataHandler.Handle(new CreateTestDataCommand());
    return Results.Accepted();
});
app.MapGet("/farm/all", async (IFarmRepository farms) =>
{
    var farmsData = await farms.All();
    return Results.Ok(farmsData);
});
app.MapGet("/farm", async (FarmType farmType, IFarmRepository farms) =>
{
    var farmsData = await farms.ByType(farmType);
    return Results.Ok(farmsData);
});
app.MapGet("/cow", async (int cowId, ICowRepository cows) =>
{
    var cow = await cows.ById(cowId);
    return Results.Ok(cow);
});
app.Run();