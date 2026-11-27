namespace CapitalProjects.Web.Domain;

public sealed class AuditRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? TenantId { get; set; }

    public Tenant? Tenant { get; set; }

    public Guid? ProjectId { get; set; }

    public Project? Project { get; set; }

    public Guid? PackageId { get; set; }

    public Package? Package { get; set; }

    public Guid? ActorId { get; set; }

    public Actor? Actor { get; set; }

    public string Action { get; set; } = string.Empty;

    public string ResourceType { get; set; } = string.Empty;

    public Guid? ResourceId { get; set; }

    public string Outcome { get; set; } = string.Empty;

    public string ReasonCode { get; set; } = string.Empty;

    public string? DetailsJson { get; set; }

    public string CorrelationId { get; set; } = string.Empty;

    public DateTimeOffset OccurredAtUtc { get; set; }
        = DateTimeOffset.UtcNow;
}