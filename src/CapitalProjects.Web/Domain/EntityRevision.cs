namespace CapitalProjects.Web.Domain;

public sealed class EntityRevision
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid? ProjectId { get; set; }

    public Project? Project { get; set; }

    public Guid EntityId { get; set; }

    public string EntityType { get; set; } = string.Empty;

    public int Version { get; set; }

    public string SnapshotJson { get; set; } = string.Empty;

    public Guid ChangedByActorId { get; set; }

    public Actor ChangedByActor { get; set; } = null!;

    public string ChangeReason { get; set; } = string.Empty;

    public DateTimeOffset RecordedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;
}