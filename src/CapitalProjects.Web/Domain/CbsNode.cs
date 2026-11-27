namespace CapitalProjects.Web.Domain;

public sealed class CbsNode
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public Guid? ParentId { get; set; }

    public CbsNode? Parent { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int Version { get; set; } = 1;

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}