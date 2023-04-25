using System.Text.Json.Serialization;
using CIS.Application;
using CIS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<JsonOptions>(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<CattleInformationDatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<RawCowDataRepository>();
builder.Services.AddScoped<CowDataToDatabaseHandler.Handler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CattleInformationDatabaseContext>();
    dbContext.Database.EnsureDeleted();
    //dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
    //dbContext.Products.AddRange(new FakeProducts().Products);
    //dbContext.SaveChanges();
}

app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Cattle Information System - API"); });

app.MapPost("/cowpassport", async ([FromServices]CowDataToDatabaseHandler.Handler handler, CowDto cow) =>
{
    await handler.Handle(new CowDataToDatabaseHandler.Command(cow));
    return Results.Accepted();
});
app.Run();