FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LifeHub.Api/LifeHub.Api.csproj", "LifeHub.Api/"]
COPY ["LifeHub.Infrastructure/LifeHub.Infrastructure.csproj", "LifeHub.Infrastructure/"]
COPY ["LifeHub.Domain/LifeHub.Domain.csproj", "LifeHub.Domain/"]
COPY ["LifeHub.Application/LifeHub.Application.csproj", "LifeHub.Application/"]
RUN dotnet restore "LifeHub.Api/LifeHub.Api.csproj"
COPY . .
WORKDIR "/src/LifeHub.Api"
RUN dotnet build "LifeHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LifeHub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LifeHub.Api.dll"]
