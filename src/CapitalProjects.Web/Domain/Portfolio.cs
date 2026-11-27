namespace CapitalProjects.Web.Domain;

public sealed class Portfolio
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;
}