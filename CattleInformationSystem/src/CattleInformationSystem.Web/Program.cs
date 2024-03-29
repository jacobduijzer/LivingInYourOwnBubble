using CattleInformationSystem.Application;
using CattleInformationSystem.Domain;
using CattleInformationSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<ICowRepository, CowRepository>();
builder.Services.AddScoped<IIncomingCowEventRepository, IncomingCowEventRepository>();
builder.Services.AddScoped<AllFarmsHandler>();
builder.Services.AddRazorPages();

var app = builder.Build();
await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();
    await new FarmSeeder(dbContext).Seed();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();

public partial class Program { }