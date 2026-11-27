namespace CapitalProjects.Web.Domain;

public sealed class Actor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid? BusinessPartyId { get; set; }

    public BusinessParty? BusinessParty { get; set; }

    public string IdentityIssuer { get; set; } = string.Empty;

    public string ExternalSubjectId { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int Version { get; set; } = 1;

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}