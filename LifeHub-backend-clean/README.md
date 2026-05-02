# LifeHub Backend Clean Architecture

## Estructura
- **Domain**: Entidades y lógica de dominio
- **Application**: Casos de uso y lógica de aplicación
- **Infrastructure**: EF Core, Identity, servicios externos
- **Api**: Controladores, configuración, entrada/salida

## Arranque local (Docker Compose)

1. Copia `.env` y configura las variables necesarias (ver ejemplo en `appsettings.json`).
2. Ejecuta:

```sh
docker compose -f docker-compose.clean.yml up --build
```

## Migraciones y base de datos

- Las migraciones están en `LifeHub.Infrastructure/Migrations`.
- Para aplicar migraciones:

```sh
docker compose exec backend dotnet ef database update --project ../LifeHub.Infrastructure --startup-project .
```

## Endpoints y documentación
- Swagger UI: `/swagger`
- SignalR Hub: `/hubs/chat`

## Notas
- Todo el backend funciona en .NET 8 y SQL Server.
- No requiere SDK local, solo Docker.
