# Capital Projects Delivery & Controls · P00

Version 0.1.0 · Engineering foundation for review

A reusable ASP.NET Core 10 host serves system status, process liveness at `GET /health/live` and metadata at `GET /api/v1/platform`. P00 contains no project data, credentials, database, external connection or business routes.

## Run and verify

Use .NET SDK 10.0.404 or a compatible newer .NET 10 feature band. From the extracted source root:

```sh
dotnet build CapitalProjects.slnx -c Release
dotnet run --project src/CapitalProjects.Web -c Release -- --urls http://127.0.0.1:5080
```

Open http://127.0.0.1:5080 and run `python3 scripts/smoke.py` in a separate terminal from the source root. It checks liveness, platform identity, assets and absence of the future project route. To verify the publish artifact, run `dotnet publish src/CapitalProjects.Web -c Release --no-build -o artifacts/publish`. From the publish directory run `dotnet CapitalProjects.Web.dll --urls http://127.0.0.1:5080` and repeat smoke from the source root.

Container recipe: `docker build -t capital-projects:p00 .` then `docker run --rm -p 127.0.0.1:5080:8080 capital-projects:p00`. Repeat smoke against the exposed host when a container is part of the handoff.

## Product handoff

The proposed first profile is an owner-side industrial capital programme. The second is a commercial building delivery profile. Business scope starts in later phases and includes baseline, commitments, verified work, change and claim control, certification, forecast and handover. Use the exact accepted source and produce fresh source evidence for P01. Sets 01–05 define later delivery.

## Verification

| Check | Result |
|---|---|
| .NET SDK 10.0.404 Release build | PASS |
| Publish and boot of published artefact | PASS |
| HTTP smoke against published host | PASS |
| JavaScript syntax | PASS |
| Browser visual status review | PASS |
| Browser network failure handling | PASS |
| Browser retry and recovery | PASS |
| Docker image build | PASS |
| Docker container runtime | PASS |
| HTTP smoke against Docker container | PASS |
| Separate accessibility validation | NOT_RUN |
| External production deployment | NOT_RUN |

### Validation details

- Validation date: 26 November 2026.
- Exact technically tested commit: `7af55f82ea832bb4fc8049d21e3011bb34a9eaec`.
- Browser: Google Chrome 157.0.8103.79 (64-bit).
- Recorded browser viewport: 458 × 730 CSS pixels.
- Docker version: 29.9.3, using the WSL 2 backend.
- P00/F01 — Build and published host: PASS.
- P00/F02 — Visual status and retry: PASS.
- P00/F03 — Local container runtime: PASS.

### Validation limitations

Separate accessibility validation was not performed and remains NOT_RUN. No accessibility compliance claim is made.

Container validation was performed locally using Docker Desktop and WSL 2. No external production deployment was performed.

### Review status

P00 technical review is positive. Formal acceptance remains pending independent verification of the submitted evidence and Repomix by the maintainer.

P01 preparation has been authorized by the maintainer.