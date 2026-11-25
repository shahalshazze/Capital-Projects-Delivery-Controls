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
| SDK 10.0.404 Release build | Passed with zero warnings and errors |
| Publish and boot of published artefact | Passed |
| HTTP smoke against published host | Passed |
| JavaScript syntax | Passed |
| Browser visual/accessibility review | Pending |
| Container runtime review | Pending when a container engine is available |
