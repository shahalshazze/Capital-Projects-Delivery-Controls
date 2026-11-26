using Microsoft.EntityFrameworkCore;

namespace CapitalProjects.Web.Persistence;

public sealed class CapitalProjectsDbContext : DbContext
{
    public CapitalProjectsDbContext(
        DbContextOptions<CapitalProjectsDbContext> options)
        : base(options)
    {
    }
}