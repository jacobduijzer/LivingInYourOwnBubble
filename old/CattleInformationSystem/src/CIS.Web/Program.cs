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
            


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});
app.UseAuthorization();


app.Run();