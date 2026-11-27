using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class AccessGrantConfiguration
    : IEntityTypeConfiguration<AccessGrant>
{
    public void Configure(EntityTypeBuilder<AccessGrant> builder)
    {
        builder.ToTable("AccessGrants", table =>
        {
            table.HasCheckConstraint(
                "CK_AccessGrants_Scope",
                "(\"ScopeType\" = 'Tenant' AND " +
                "\"ProjectId\" IS NULL AND " +
                "\"PackageId\" IS NULL) OR " +
                "(\"ScopeType\" = 'Project' AND " +
                "\"ProjectId\" IS NOT NULL AND " +
                "\"PackageId\" IS NULL) OR " +
                "(\"ScopeType\" = 'Package' AND " +
                "\"ProjectId\" IS NOT NULL AND " +
                "\"PackageId\" IS NOT NULL)");

            table.HasCheckConstraint(
                "CK_AccessGrants_ValidityDates",
                "\"ValidUntilUtc\" IS NULL OR " +
                "\"ValidUntilUtc\" > \"ValidFromUtc\"");

            table.HasCheckConstraint(
                "CK_AccessGrants_Revocation",
                "(\"RevokedAtUtc\" IS NULL AND " +
                "\"RevokedByActorId\" IS NULL AND " +
                "\"RevocationReason\" IS NULL) OR " +
                "(\"RevokedAtUtc\" IS NOT NULL AND " +
                "\"RevokedByActorId\" IS NOT NULL AND " +
                "\"RevocationReason\" IS NOT NULL AND " +
                "length(btrim(\"RevocationReason\")) > 0 AND " +
                "\"RevokedAtUtc\" >= \"GrantedAtUtc\")");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ScopeType)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.RoleCode)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.RevocationReason)
            .HasMaxLength(1000);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.ActorId,
            x.ScopeType,
            x.ProjectId,
            x.PackageId
        });

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Actor)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Package)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.PackageId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Actor>()
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.GrantedByActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Actor>()
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.RevokedByActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Version)
            .IsRequired()
            .IsConcurrencyToken();

        builder.Property(x => x.ValidFromUtc)
            .IsRequired();

        builder.Property(x => x.GrantedAtUtc)
            .IsRequired();
    }
}