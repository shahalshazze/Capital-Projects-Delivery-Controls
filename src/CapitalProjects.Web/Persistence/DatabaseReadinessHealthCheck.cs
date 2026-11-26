using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CapitalProjects.Web.Persistence;

public sealed class DatabaseReadinessHealthCheck(
    IServiceScopeFactory scopeFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<CapitalProjectsDbContext>();

            var canConnect = await dbContext.Database
                .CanConnectAsync(cancellationToken);

            return canConnect
                ? HealthCheckResult.Healthy("Database is reachable.")
                : HealthCheckResult.Unhealthy("Database is unavailable.");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return HealthCheckResult.Unhealthy(
                "Database readiness check failed.");
        }
    }
}