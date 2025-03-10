using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DigitalGarden.Data;
using MVCView.Models;
using MVCView.Data;
using MVCView.Repositories;

var builder = WebApplication.CreateBuilder(args);
// # Create initial migration
// dotnet ef migrations add InitialCreate

// # Apply migrations to the database
// dotnet ef database update
// Hardcode environment to Staging
var environment = "Production";

// Explicitly load the configuration for the hardcoded environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

if (environment == "Staging" || environment == "Production")
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException($"Connection string 'DefaultConnection' not found in appsettings.{environment}.json");

    // Log the connection string to verify it's correct (remove in production!)
    Console.WriteLine($"Using connection string: {connectionString}");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    Console.WriteLine($"Using SQL Server in {environment} environment");
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));

    Console.WriteLine("Using SQLite in Development environment");
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// Use the hardcoded environment for repository registration too
if (environment == "Development")
{
    builder.Services.AddSingleton<IPlantRepository, MockPlantRepository>();
    builder.Services.AddSingleton<ICareLogRepository, MockCareLogRepository>();
    builder.Services.AddSingleton<ICommunityTipRepository, MockCommunityTipRepository>();
    builder.Services.AddSingleton<IProfileRepository, MockProfileRepository>();
}
else
{
    builder.Services.AddScoped<IPlantRepository, PlantRepository>();
    builder.Services.AddScoped<ICareLogRepository, CareLogRepository>();
    builder.Services.AddScoped<ICommunityTipRepository, CommunityTipRepository>();
    builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline - use the hardcoded environment here too
if (environment == "Development")
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
