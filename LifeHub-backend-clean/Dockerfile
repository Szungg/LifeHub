# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY LifeHub.Api ./LifeHub.Api
COPY LifeHub.Domain ./LifeHub.Domain
COPY LifeHub.Infrastructure ./LifeHub.Infrastructure
COPY LifeHub.Application ./LifeHub.Application
COPY *.sln ./
WORKDIR /src/LifeHub.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LifeHub.Api.dll"]
