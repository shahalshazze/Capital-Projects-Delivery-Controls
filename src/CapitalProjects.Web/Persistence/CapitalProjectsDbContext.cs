using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace CapitalProjects.Web.Persistence;

public sealed class CapitalProjectsDbContext : DbContext
{
    public CapitalProjectsDbContext(
        DbContextOptions<CapitalProjectsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<Portfolio> Portfolios => Set<Portfolio>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Package> Packages => Set<Package>();

    public DbSet<BusinessParty> BusinessParties
        => Set<BusinessParty>();

    public DbSet<WbsNode> WbsNodes => Set<WbsNode>();

    public DbSet<CbsNode> CbsNodes => Set<CbsNode>();

    public DbSet<WbsCbsMapping> WbsCbsMappings
        => Set<WbsCbsMapping>();

    public DbSet<Actor> Actors => Set<Actor>();

    public DbSet<AccessGrant> AccessGrants
        => Set<AccessGrant>();

    public DbSet<EntityRevision> EntityRevisions
        => Set<EntityRevision>();

    public DbSet<AuditRecord> AuditRecords
        => Set<AuditRecord>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CapitalProjectsDbContext).Assembly);
    }
}