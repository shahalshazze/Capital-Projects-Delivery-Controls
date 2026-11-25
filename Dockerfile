FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source
COPY global.json ./
COPY src/CapitalProjects.Web/CapitalProjects.Web.csproj src/CapitalProjects.Web/
RUN dotnet restore src/CapitalProjects.Web/CapitalProjects.Web.csproj
COPY src/CapitalProjects.Web/ src/CapitalProjects.Web/
RUN dotnet publish src/CapitalProjects.Web/CapitalProjects.Web.csproj -c Release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out ./
USER app
EXPOSE 8080
ENTRYPOINT ["dotnet", "CapitalProjects.Web.dll"]
