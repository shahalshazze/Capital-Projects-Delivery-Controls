namespace CapitalProjects.Web.Domain;

public sealed class AccessGrant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid ActorId { get; set; }

    public Actor Actor { get; set; } = null!;

    public Guid? ProjectId { get; set; }

    public Project? Project { get; set; }

    public Guid? PackageId { get; set; }

    public Package? Package { get; set; }

    public string ScopeType { get; set; } = string.Empty;

    public string RoleCode { get; set; } = string.Empty;

    public DateTimeOffset ValidFromUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? ValidUntilUtc { get; set; }

    public Guid GrantedByActorId { get; set; }

    public DateTimeOffset GrantedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public Guid? RevokedByActorId { get; set; }

    public DateTimeOffset? RevokedAtUtc { get; set; }

    public string? RevocationReason { get; set; }

    public int Version { get; set; } = 1;
}