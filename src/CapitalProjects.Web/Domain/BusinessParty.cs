namespace CapitalProjects.Web.Domain;

public sealed class BusinessParty
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public string Code { get; set; } = string.Empty;

    public string LegalName { get; set; } = string.Empty;

    public string PartyType { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int Version { get; set; } = 1;

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}