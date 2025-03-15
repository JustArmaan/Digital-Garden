using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DigitalGarden.Data;
using MVCView.Models;
using MVCView.Data;
using MVCView.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// # Create initial migration
// dotnet ef migrations add InitialCreate

// # Apply migrations to the database
// dotnet ef database update
// Hardcode environment to Staging
var environment = "Staging";

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
        options.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

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

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ExceptionFilterAttribute(environment));
});


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
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";

            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = errorFeature?.Error;

            Console.Error.WriteLine($"Error: {exception?.Message}");

            if (environment == "Staging")
            {
                await context.Response.WriteAsync(@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Application Error - Staging Environment</title>
                    <style>
                        body { font-family: Arial, sans-serif; padding: 20px; }
                        .error-container { border: 1px solid #ffa500; padding: 20px; border-radius: 5px; }
                        .error-details { background-color: #f8f8f8; padding: 10px; margin-top: 20px; }
                    </style>
                </head>
                <body>
                    <div class='error-container'>
                        <h2>Digital Garden Application Error (Staging Environment)</h2>
                        <p>An error occurred while processing your request. Our team has been notified.</p>
                        <div class='error-details'>
                            <h3>Technical Details (Staging Only):</h3>
                            <p>Error Message: " + exception?.Message + @"</p>
                            <p>Stack Trace: " + exception?.StackTrace?.Replace("\n", "<br/>") + @"</p>
                        </div>
                    </div>
                </body>
                </html>");
            }
            else
            {
                await context.Response.WriteAsync(@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Digital Garden - Temporary Issue</title>
                    <style>
                        body { font-family: Arial, sans-serif; padding: 20px; text-align: center; }
                        .error-container { border: 1px solid #4CAF50; padding: 20px; border-radius: 5px; max-width: 600px; margin: 0 auto; }
                        .plant-icon { font-size: 48px; margin-bottom: 20px; }
                    </style>
                </head>
                <body>
                    <div class='error-container'>
                        <div class='plant-icon'>🌱</div>
                        <h2>Oops! Something's Not Growing Right</h2>
                        <p>We're experiencing a temporary issue with our digital garden.</p>
                        <p>Our gardeners have been notified and are working to fix it. Please try again later.</p>
                        <p><a href='/'>Return to Home Page</a></p>
                    </div>
                </body>
                </html>");
            }
        });
    });

    app.UseHsts();
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.ContentType = "text/html";

        if (environment == "Development")
        {
            await response.WriteAsync(@"
            <h1>Page Not Found (404) - Development Environment</h1>
            <p>The requested URL " + context.HttpContext.Request.Path + @" was not found.</p>
            <p>Check your route configuration and ensure the controller and action exist.</p>");
        }
        else if (environment == "Staging")
        {
            await response.WriteAsync(@"
            <h1>Page Not Found (404) - Staging Environment</h1>
            <p>The requested URL " + context.HttpContext.Request.Path + @" was not found.</p>
            <p>This is the staging environment - please report this missing page.</p>");
        }
        else
        {
            await response.WriteAsync(@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Page Not Found - Digital Garden</title>
                <style>
                    body { font-family: Arial, sans-serif; padding: 20px; text-align: center; }
                    .error-container { border: 1px solid #4CAF50; padding: 20px; border-radius: 5px; max-width: 600px; margin: 0 auto; }
                </style>
            </head>
            <body>
                <div class='error-container'>
                    <h2>Page Not Found</h2>
                    <p>Sorry, the page you're looking for doesn't exist in our garden.</p>
                    <p><a href='/'>Return to Home Page</a></p>
                </div>
            </body>
            </html>");
        }
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter
{
    private readonly string _environment;

    public ExceptionFilterAttribute(string environment)
    {
        _environment = environment;
    }

    public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
    {
        Console.Error.WriteLine($"Exception detected: {context.Exception.Message}");

        if (context.ExceptionHandled)
            return;

        var result = new ViewResult { ViewName = "Error" };

        if (_environment == "Development")
        {
            result.ViewData["ErrorTitle"] = "Development Error";
            result.ViewData["ErrorMessage"] = context.Exception.Message;
            result.ViewData["StackTrace"] = context.Exception.StackTrace;
        }
        else if (_environment == "Staging")
        {
            result.ViewData["ErrorTitle"] = "Staging Environment Error";
            result.ViewData["ErrorMessage"] = context.Exception.Message;
            result.ViewData["StackTrace"] = "Stack trace available for admins only";
        }
        else
        {
            result.ViewData["ErrorTitle"] = "Sorry, something went wrong";
            result.ViewData["ErrorMessage"] = "An unexpected error occurred. Our team has been notified.";
        }

        context.Result = result;
        context.ExceptionHandled = true;
    }
}
