namespace CapitalProjects.Web.Domain;

public sealed class WbsCbsMapping
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public Guid WbsNodeId { get; set; }

    public WbsNode WbsNode { get; set; } = null!;

    public Guid CbsNodeId { get; set; }

    public CbsNode CbsNode { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public int Version { get; set; } = 1;

    public string? SourceUnitReference { get; set; }

    public string? MeasurementReference { get; set; }

    public DateTimeOffset EffectiveFromUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? EffectiveToUtc { get; set; }

    public Guid CreatedByActorId { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public Guid? UpdatedByActorId { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}