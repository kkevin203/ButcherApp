using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Database;
using Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using ButcherApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
/*builder.Services.AddDbContext<ButcherAppContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ButcherAppContext") ?? throw new InvalidOperationException("Connection string 'ButcherAppContext' not found.")));*/
builder.Services.AddServerSideBlazor();

// Add DbContext service.
builder.Services.AddDbContext<ButcherDatabase>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSqlite")));



//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<ButcherService>();
builder.Services.AddScoped<FarmerService>();

//builder.Services.AddDbContext<ButcherDatabase>();

var app = builder.Build();

// Configure l'application
var cultureInfo = new CultureInfo("fr-FR");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

CreateDbIfNotExists(app);

app.Run();
void CreateDbIfNotExists(IApplicationBuilder app)
{
    using (var scope = app.ApplicationServices.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ButcherDatabase>();
            context.Database.EnsureDeleted();
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Une erreur s'est produite lors de la création de la base de données.");
        }
    }
}