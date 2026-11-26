using CapitalProjects.Web.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseReadinessHealthCheck>(
        "database",
        tags: new[] { "ready" });

var connectionString = builder.Configuration
    .GetConnectionString("CapitalProjects")
    ?? throw new InvalidOperationException(
        "CapitalProjects database connection string is not configured.");

builder.Services.AddDbContext<CapitalProjectsDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapGet("/api/v1/platform", () => TypedResults.Ok(new
{
    product = "Capital Projects Delivery & Controls",
    phase = "P00",
    profile = "foundation",
    version = typeof(Program).Assembly.GetName().Version?.ToString(3)
}));

app.Run();