using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalProjects.Web.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class P01_InitialDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessParties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LegalName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PartyType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessParties", x => x.Id);
                    table.UniqueConstraint("AK_BusinessParties_TenantId_Id", x => new { x.TenantId, x.Id });
                    table.ForeignKey(
                        name: "FK_BusinessParties_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                    table.UniqueConstraint("AK_Portfolios_TenantId_Id", x => new { x.TenantId, x.Id });
                    table.ForeignKey(
                        name: "FK_Portfolios_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessPartyId = table.Column<Guid>(type: "uuid", nullable: true),
                    IdentityIssuer = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExternalSubjectId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.UniqueConstraint("AK_Actors_TenantId_Id", x => new { x.TenantId, x.Id });
                    table.ForeignKey(
                        name: "FK_Actors_BusinessParties_TenantId_BusinessPartyId",
                        columns: x => new { x.TenantId, x.BusinessPartyId },
                        principalTable: "BusinessParties",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actors_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.UniqueConstraint("AK_Projects_TenantId_Id", x => new { x.TenantId, x.Id });
                    table.ForeignKey(
                        name: "FK_Projects_Portfolios_TenantId_PortfolioId",
                        columns: x => new { x.TenantId, x.PortfolioId },
                        principalTable: "Portfolios",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CbsNodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CbsNodes", x => x.Id);
                    table.UniqueConstraint("AK_CbsNodes_TenantId_ProjectId_Id", x => new { x.TenantId, x.ProjectId, x.Id });
                    table.CheckConstraint("CK_CbsNodes_NoSelfParent", "\"ParentId\" IS NULL OR \"ParentId\" <> \"Id\"");
                    table.ForeignKey(
                        name: "FK_CbsNodes_CbsNodes_TenantId_ProjectId_ParentId",
                        columns: x => new { x.TenantId, x.ProjectId, x.ParentId },
                        principalTable: "CbsNodes",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CbsNodes_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CbsNodes_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityRevisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    SnapshotJson = table.Column<string>(type: "jsonb", nullable: false),
                    ChangedByActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeReason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    RecordedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityRevisions", x => x.Id);
                    table.CheckConstraint("CK_EntityRevisions_ChangeReason", "length(btrim(\"ChangeReason\")) > 0");
                    table.CheckConstraint("CK_EntityRevisions_PositiveVersion", "\"Version\" > 0");
                    table.CheckConstraint("CK_EntityRevisions_SnapshotObject", "jsonb_typeof(\"SnapshotJson\") = 'object'");
                    table.ForeignKey(
                        name: "FK_EntityRevisions_Actors_TenantId_ChangedByActorId",
                        columns: x => new { x.TenantId, x.ChangedByActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityRevisions_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityRevisions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.UniqueConstraint("AK_Packages_TenantId_ProjectId_Id", x => new { x.TenantId, x.ProjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_Packages_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packages_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WbsNodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WbsNodes", x => x.Id);
                    table.UniqueConstraint("AK_WbsNodes_TenantId_ProjectId_Id", x => new { x.TenantId, x.ProjectId, x.Id });
                    table.CheckConstraint("CK_WbsNodes_NoSelfParent", "\"ParentId\" IS NULL OR \"ParentId\" <> \"Id\"");
                    table.ForeignKey(
                        name: "FK_WbsNodes_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsNodes_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsNodes_WbsNodes_TenantId_ProjectId_ParentId",
                        columns: x => new { x.TenantId, x.ProjectId, x.ParentId },
                        principalTable: "WbsNodes",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: true),
                    ScopeType = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    RoleCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ValidFromUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ValidUntilUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    GrantedByActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RevokedByActorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RevokedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RevocationReason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGrants", x => x.Id);
                    table.CheckConstraint("CK_AccessGrants_Revocation", "(\"RevokedAtUtc\" IS NULL AND \"RevokedByActorId\" IS NULL AND \"RevocationReason\" IS NULL) OR (\"RevokedAtUtc\" IS NOT NULL AND \"RevokedByActorId\" IS NOT NULL AND \"RevocationReason\" IS NOT NULL AND length(btrim(\"RevocationReason\")) > 0 AND \"RevokedAtUtc\" >= \"GrantedAtUtc\")");
                    table.CheckConstraint("CK_AccessGrants_Scope", "(\"ScopeType\" = 'Tenant' AND \"ProjectId\" IS NULL AND \"PackageId\" IS NULL) OR (\"ScopeType\" = 'Project' AND \"ProjectId\" IS NOT NULL AND \"PackageId\" IS NULL) OR (\"ScopeType\" = 'Package' AND \"ProjectId\" IS NOT NULL AND \"PackageId\" IS NOT NULL)");
                    table.CheckConstraint("CK_AccessGrants_ValidityDates", "\"ValidUntilUtc\" IS NULL OR \"ValidUntilUtc\" > \"ValidFromUtc\"");
                    table.ForeignKey(
                        name: "FK_AccessGrants_Actors_TenantId_ActorId",
                        columns: x => new { x.TenantId, x.ActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessGrants_Actors_TenantId_GrantedByActorId",
                        columns: x => new { x.TenantId, x.GrantedByActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessGrants_Actors_TenantId_RevokedByActorId",
                        columns: x => new { x.TenantId, x.RevokedByActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessGrants_Packages_TenantId_ProjectId_PackageId",
                        columns: x => new { x.TenantId, x.ProjectId, x.PackageId },
                        principalTable: "Packages",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessGrants_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessGrants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ResourceType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Outcome = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ReasonCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DetailsJson = table.Column<string>(type: "jsonb", nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    OccurredAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecords", x => x.Id);
                    table.CheckConstraint("CK_AuditRecords_Context", "(\"ProjectId\" IS NULL OR \"TenantId\" IS NOT NULL) AND (\"PackageId\" IS NULL OR (\"TenantId\" IS NOT NULL AND \"ProjectId\" IS NOT NULL)) AND (\"ActorId\" IS NULL OR \"TenantId\" IS NOT NULL)");
                    table.CheckConstraint("CK_AuditRecords_DetailsObject", "\"DetailsJson\" IS NULL OR jsonb_typeof(\"DetailsJson\") = 'object'");
                    table.ForeignKey(
                        name: "FK_AuditRecords_Actors_TenantId_ActorId",
                        columns: x => new { x.TenantId, x.ActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRecords_Packages_TenantId_ProjectId_PackageId",
                        columns: x => new { x.TenantId, x.ProjectId, x.PackageId },
                        principalTable: "Packages",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRecords_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRecords_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WbsCbsMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    WbsNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CbsNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    SourceUnitReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MeasurementReference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EffectiveFromUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EffectiveToUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedByActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedByActorId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WbsCbsMappings", x => x.Id);
                    table.UniqueConstraint("AK_WbsCbsMappings_TenantId_ProjectId_Id", x => new { x.TenantId, x.ProjectId, x.Id });
                    table.CheckConstraint("CK_WbsCbsMappings_ActiveValidity", "(\"IsActive\" = TRUE AND \"EffectiveToUtc\" IS NULL) OR (\"IsActive\" = FALSE AND \"EffectiveToUtc\" IS NOT NULL)");
                    table.CheckConstraint("CK_WbsCbsMappings_EffectiveDates", "\"EffectiveToUtc\" IS NULL OR \"EffectiveToUtc\" > \"EffectiveFromUtc\"");
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_Actors_TenantId_CreatedByActorId",
                        columns: x => new { x.TenantId, x.CreatedByActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_Actors_TenantId_UpdatedByActorId",
                        columns: x => new { x.TenantId, x.UpdatedByActorId },
                        principalTable: "Actors",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_CbsNodes_TenantId_ProjectId_CbsNodeId",
                        columns: x => new { x.TenantId, x.ProjectId, x.CbsNodeId },
                        principalTable: "CbsNodes",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_Projects_TenantId_ProjectId",
                        columns: x => new { x.TenantId, x.ProjectId },
                        principalTable: "Projects",
                        principalColumns: new[] { "TenantId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WbsCbsMappings_WbsNodes_TenantId_ProjectId_WbsNodeId",
                        columns: x => new { x.TenantId, x.ProjectId, x.WbsNodeId },
                        principalTable: "WbsNodes",
                        principalColumns: new[] { "TenantId", "ProjectId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGrants_TenantId_ActorId_ScopeType_ProjectId_PackageId",
                table: "AccessGrants",
                columns: new[] { "TenantId", "ActorId", "ScopeType", "ProjectId", "PackageId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGrants_TenantId_GrantedByActorId",
                table: "AccessGrants",
                columns: new[] { "TenantId", "GrantedByActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGrants_TenantId_ProjectId_PackageId",
                table: "AccessGrants",
                columns: new[] { "TenantId", "ProjectId", "PackageId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGrants_TenantId_RevokedByActorId",
                table: "AccessGrants",
                columns: new[] { "TenantId", "RevokedByActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_TenantId_BusinessPartyId",
                table: "Actors",
                columns: new[] { "TenantId", "BusinessPartyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_TenantId_IdentityIssuer_ExternalSubjectId",
                table: "Actors",
                columns: new[] { "TenantId", "IdentityIssuer", "ExternalSubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_CorrelationId",
                table: "AuditRecords",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_TenantId_ActorId_OccurredAtUtc",
                table: "AuditRecords",
                columns: new[] { "TenantId", "ActorId", "OccurredAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_TenantId_ProjectId_OccurredAtUtc",
                table: "AuditRecords",
                columns: new[] { "TenantId", "ProjectId", "OccurredAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_TenantId_ProjectId_PackageId",
                table: "AuditRecords",
                columns: new[] { "TenantId", "ProjectId", "PackageId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessParties_TenantId_Code",
                table: "BusinessParties",
                columns: new[] { "TenantId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CbsNodes_TenantId_ProjectId_Code",
                table: "CbsNodes",
                columns: new[] { "TenantId", "ProjectId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CbsNodes_TenantId_ProjectId_ParentId",
                table: "CbsNodes",
                columns: new[] { "TenantId", "ProjectId", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityRevisions_TenantId_ChangedByActorId",
                table: "EntityRevisions",
                columns: new[] { "TenantId", "ChangedByActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityRevisions_TenantId_EntityType_EntityId_Version",
                table: "EntityRevisions",
                columns: new[] { "TenantId", "EntityType", "EntityId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityRevisions_TenantId_ProjectId_RecordedAtUtc",
                table: "EntityRevisions",
                columns: new[] { "TenantId", "ProjectId", "RecordedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_TenantId_ProjectId_Code",
                table: "Packages",
                columns: new[] { "TenantId", "ProjectId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_TenantId_Code",
                table: "Portfolios",
                columns: new[] { "TenantId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TenantId_Code",
                table: "Projects",
                columns: new[] { "TenantId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TenantId_PortfolioId",
                table: "Projects",
                columns: new[] { "TenantId", "PortfolioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Code",
                table: "Tenants",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WbsCbsMappings_TenantId_CreatedByActorId",
                table: "WbsCbsMappings",
                columns: new[] { "TenantId", "CreatedByActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_WbsCbsMappings_TenantId_ProjectId_CbsNodeId",
                table: "WbsCbsMappings",
                columns: new[] { "TenantId", "ProjectId", "CbsNodeId" });

            migrationBuilder.CreateIndex(
                name: "IX_WbsCbsMappings_TenantId_ProjectId_WbsNodeId_CbsNodeId",
                table: "WbsCbsMappings",
                columns: new[] { "TenantId", "ProjectId", "WbsNodeId", "CbsNodeId" },
                unique: true,
                filter: "\"IsActive\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_WbsCbsMappings_TenantId_UpdatedByActorId",
                table: "WbsCbsMappings",
                columns: new[] { "TenantId", "UpdatedByActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_WbsNodes_TenantId_ProjectId_Code",
                table: "WbsNodes",
                columns: new[] { "TenantId", "ProjectId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WbsNodes_TenantId_ProjectId_ParentId",
                table: "WbsNodes",
                columns: new[] { "TenantId", "ProjectId", "ParentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGrants");

            migrationBuilder.DropTable(
                name: "AuditRecords");

            migrationBuilder.DropTable(
                name: "EntityRevisions");

            migrationBuilder.DropTable(
                name: "WbsCbsMappings");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "CbsNodes");

            migrationBuilder.DropTable(
                name: "WbsNodes");

            migrationBuilder.DropTable(
                name: "BusinessParties");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
