using CIS.Domain;
using CIS.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CattleInformationDatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<RawCowDataRepository>();
builder.Services.AddScoped(typeof(IGetRepository<>), typeof(GetRepository<>));
builder.Services.AddRazorPages();

var app = builder.Build();
            
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CattleInformationDatabaseContext>();
    dbContext.Database.EnsureDeleted();
    //dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
    //dbContext.Products.AddRange(new FakeProducts().Products);
    //dbContext.SaveChanges();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();