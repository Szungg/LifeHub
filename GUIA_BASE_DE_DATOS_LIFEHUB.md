# Guia de Base de Datos de LifeHub

## Resumen ejecutivo

La base de datos actual de `LifeHub-Backend` se construye con tres mecanismos distintos:

1. `ApplicationDbContext` define el modelo EF Core.
2. Las migraciones EF Core materializan ese modelo en SQL Server.
3. `DataSeeder` inserta datos iniciales y ademas ejecuta SQL manual que tambien crea o altera esquema.

Mientras exista esa mezcla, no hay una sola fuente de verdad del esquema. Si quieres control real, el objetivo debe ser que el esquema salga solo de migraciones y que el seeder se limite a datos iniciales.

## Flujo real de arranque

El arranque actual esta en `LifeHub-Backend/Program.cs`:

1. Se registra `ApplicationDbContext` con `UseSqlServer(...)`.
2. Se configura Identity con `AddIdentityCore<ApplicationUser>()`.
3. Al iniciar la app:
   - `db.Database.MigrateAsync()` aplica migraciones pendientes.
   - `DataSeeder.SeedRolesAndAdminAsync(...)` mete datos iniciales.
   - Ese mismo seeder tambien ejecuta `CREATE TABLE` y `ALTER TABLE` manuales.

## Fuente de cada tabla

### 1. Tablas creadas por Identity

Estas tablas aparecen porque `ApplicationDbContext` hereda de `IdentityDbContext<ApplicationUser>`:

- `AspNetUsers`
- `AspNetRoles`
- `AspNetUserRoles`
- `AspNetUserClaims`
- `AspNetRoleClaims`
- `AspNetUserLogins`
- `AspNetUserTokens`

Consecuencia importante: `AspNetUsers` no depende solo de tu clase `ApplicationUser`, sino tambien de `IdentityUser`.

### 2. Tablas de dominio creadas por EF Core

Estas salen del `DbContext`, de las entidades en `Models/` y de `OnModelCreating(...)`:

- `Friendships`
- `Messages`
- `Recommendations`
- `RecommendationRatings`
- `Documents`
- `MusicFiles`
- `CreativeSpaces`
- `DocumentVersions`
- `SpacePermissions`
- `ActivityLogs`
- `AllowedWebsites`
- `DocumentPublications`

Ojo: que una entidad exista en `ApplicationDbContext` no crea nada por si sola. Hace falta generar y aplicar una migracion.

### 3. Tablas o columnas tocadas por SQL manual

En `LifeHub-Backend/Data/DataSeeder.cs` hay SQL manual que hace esto en tiempo de arranque:

- Crea `AllowedWebsites` si no existe.
- Anade `Documents.IsPublic` si no existe.
- Anade `Documents.PublishedAt` si no existe.
- Crea `DocumentPublications` si no existe.

Eso significa que esas estructuras no dependen solo de EF Core. Tambien dependen de que el seeder corra correctamente.

## Migraciones aplicadas ahora mismo

La base real de desarrollo tiene estas migraciones registradas en `__EFMigrationsHistory`:

1. `20260131213252_InitialCreate`
2. `20260402173405_AddCreativeSpacesAndVersioning`
3. `20260404201500_AddCreativeSpaceMediaReferencesJson`

Interpretacion:

- La base ya usa EF Core como mecanismo principal.
- Pero no todo el esquema esta gobernado de forma limpia por migraciones, porque parte del esquema se crea en `DataSeeder`.

## Tablas reales detectadas en la base de desarrollo

Consultadas directamente en SQL Server:

| Tabla | Filas | Origen principal |
|---|---:|---|
| `__EFMigrationsHistory` | 3 | EF Core |
| `ActivityLogs` | 64 | EF Core |
| `AllowedWebsites` | 6 | EF Core + SQL manual en seeder |
| `AspNetRoleClaims` | 0 | Identity |
| `AspNetRoles` | 3 | Identity |
| `AspNetUserClaims` | 1 | Identity |
| `AspNetUserLogins` | 0 | Identity |
| `AspNetUserRoles` | 9 | Identity |
| `AspNetUsers` | 9 | Identity + `ApplicationUser` |
| `AspNetUserTokens` | 0 | Identity |
| `CreativeSpaces` | 18 | EF Core |
| `DocumentPublications` | 0 | EF Core + SQL manual en seeder |
| `Documents` | 22 | EF Core + columnas alteradas por SQL manual |
| `DocumentVersions` | 13 | EF Core |
| `Friendships` | 6 | EF Core |
| `Messages` | 0 | EF Core |
| `MusicFiles` | 0 | EF Core |
| `RecommendationRatings` | 0 | EF Core |
| `Recommendations` | 1 | EF Core |
| `SpacePermissions` | 8 | EF Core |

## Mapa funcional tabla por tabla

### Identity

#### `AspNetUsers`

- Usuario principal del sistema.
- Sale de `ApplicationUser : IdentityUser`.
- Tiene campos tuyos: `FullName`, `ProfilePictureUrl`, `Bio`, `CreatedAt`, `UpdatedAt`.
- Tiene campos de Identity: `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `TwoFactorEnabled`, `LockoutEnd`, etc.

#### `AspNetRoles`

- Roles globales como `Admin`, `User`, `Moderator`.
- Se crean por Identity.
- El seeder mete datos iniciales.

#### `AspNetUserRoles`

- Relacion muchos a muchos entre usuarios y roles.

#### `AspNetUserClaims`

- Claims directos de usuario.
- Ahora mismo hay al menos un claim de permisos de administracion.

#### `AspNetRoleClaims`, `AspNetUserLogins`, `AspNetUserTokens`

- Infraestructura de Identity.
- Existen aunque hoy apenas se usen.

### Dominio principal

#### `Documents`

- Documentos del usuario.
- FK a `AspNetUsers` por `UserId`.
- FK opcional a `CreativeSpaces` por `CreativeSpaceId`.
- Tiene versionado mediante `DocumentVersions`.
- Tiene publicacion 1:1 mediante `DocumentPublications`.
- `IsPublic` y `PublishedAt` hoy dependen de SQL manual en el seeder.

#### `DocumentVersions`

- Historial de versiones de cada documento.
- FK a `Documents`.
- FK a `AspNetUsers` por `CreatedByUserId`.
- Indice unico en `(DocumentId, VersionNumber)`.

#### `DocumentPublications`

- Publicacion publica de un documento.
- FK a `Documents`.
- FK a `AspNetUsers` por `PublishedByUserId`.
- Indice unico en `DocumentId` para garantizar una publicacion por documento.
- Ahora existe tanto en el modelo EF como en SQL manual del seeder.

#### `CreativeSpaces`

- Espacios creativos de usuario.
- FK a `AspNetUsers` por `OwnerId`.
- Relacion con `Documents` y `SpacePermissions`.
- `MediaReferencesJson` se anadio con migracion idempotente porque antes hubo una solucion manual.

#### `SpacePermissions`

- Permisos por usuario dentro de un `CreativeSpace`.
- FK a `CreativeSpaces`.
- FK a `AspNetUsers` por `UserId`.
- FK a `AspNetUsers` por `GrantedByUserId`.
- Indice unico en `(CreativeSpaceId, UserId)`.

### Social

#### `Friendships`

- Relaciones de amistad entre usuarios.
- Dos FKs a `AspNetUsers`: `RequesterId` y `ReceiverId`.
- Indice unico en `(RequesterId, ReceiverId)`.

#### `Messages`

- Mensajes privados.
- Dos FKs a `AspNetUsers`: `SenderId` y `ReceiverId`.

### Contenido adicional

#### `Recommendations`

- Recomendaciones de peliculas, libros o series.
- FK a `AspNetUsers`.

#### `RecommendationRatings`

- Valoraciones de recomendaciones.
- FK a `Recommendations`.
- FK a `AspNetUsers`.
- Indice unico en `(RecommendationId, UserId)`.

#### `MusicFiles`

- Metadatos de musica de usuario.
- FK a `AspNetUsers`.

### Soporte

#### `ActivityLogs`

- Auditoria de acciones.
- FK opcional a `AspNetUsers`.
- Al borrar usuario, el log conserva fila y pone `UserId` a null.

#### `AllowedWebsites`

- Lista blanca de dominios permitidos para embeds.
- Tiene indice unico por `Domain`.
- Existe en el modelo EF, pero tambien se crea manualmente en el seeder.

## Relaciones reales observadas

Relaciones clave detectadas en la base real:

- `ActivityLogs.UserId -> AspNetUsers.Id`
- `CreativeSpaces.OwnerId -> AspNetUsers.Id`
- `Documents.UserId -> AspNetUsers.Id`
- `Documents.CreativeSpaceId -> CreativeSpaces.Id`
- `DocumentVersions.DocumentId -> Documents.Id`
- `DocumentVersions.CreatedByUserId -> AspNetUsers.Id`
- `DocumentPublications.DocumentId -> Documents.Id`
- `DocumentPublications.PublishedByUserId -> AspNetUsers.Id`
- `Friendships.RequesterId -> AspNetUsers.Id`
- `Friendships.ReceiverId -> AspNetUsers.Id`
- `Messages.SenderId -> AspNetUsers.Id`
- `Messages.ReceiverId -> AspNetUsers.Id`
- `MusicFiles.UserId -> AspNetUsers.Id`
- `Recommendations.UserId -> AspNetUsers.Id`
- `RecommendationRatings.RecommendationId -> Recommendations.Id`
- `RecommendationRatings.UserId -> AspNetUsers.Id`
- `SpacePermissions.CreativeSpaceId -> CreativeSpaces.Id`
- `SpacePermissions.UserId -> AspNetUsers.Id`
- `SpacePermissions.GrantedByUserId -> AspNetUsers.Id`

## Como modificar la base con control

### Regla 1: el modelo vive en entidades y `OnModelCreating`

Debes tocar:

- La clase de entidad en `LifeHub-Backend/Models/`.
- Si hace falta, la configuracion relacional en `LifeHub-Backend/Data/ApplicationDbContext.cs`.

Ejemplos:

- Nueva columna: anades propiedad a la entidad.
- Nueva relacion: anades navegacion y `HasOne/WithMany/HasForeignKey`.
- Nuevo indice o unicidad: lo declaras en `OnModelCreating`.

### Regla 2: el cambio real se registra en una migracion

Despues del cambio en el modelo debes generar una migracion nueva.

Comandos habituales:

```powershell
dotnet ef migrations add NombreDelCambio --project LifeHub-Backend
dotnet ef database update --project LifeHub-Backend
```

Antes de aplicar la migracion, revisa el archivo generado en `LifeHub-Backend/Migrations/`.

### Regla 3: el seeder no deberia crear esquema

`DataSeeder` deberia usarse solo para:

- roles iniciales
- usuario admin
- claims iniciales
- datos por defecto como dominios permitidos

No deberia contener:

- `CREATE TABLE`
- `ALTER TABLE`
- correcciones estructurales permanentes

Si un cambio es de esquema, debe vivir en una migracion.

## Procedimientos tipicos

### Anadir una columna

1. Anade la propiedad en la entidad.
2. Ajusta `OnModelCreating` si necesita indice, precision, restriccion o relacion.
3. Genera migracion.
4. Revisa `Up` y `Down`.
5. Aplica migracion.

### Eliminar una columna

1. Elimina la propiedad o configuracion.
2. Genera migracion.
3. Verifica si se genera `DropColumn`.
4. Comprueba si hay datos que debas migrar antes.
5. Aplica migracion.

### Anadir una tabla nueva

1. Crea la entidad en `Models/`.
2. Anade el `DbSet<>` al `ApplicationDbContext`.
3. Configura relaciones e indices en `OnModelCreating`.
4. Genera migracion.
5. Aplica migracion.

### Tocar usuarios

Si solo quieres anadir o quitar campos tuyos:

1. Modifica `ApplicationUser`.
2. Genera migracion.

Si quieres simplificar `AspNetUsers` de verdad:

- mientras uses `IdentityUser`, seguiras teniendo columnas base de Identity.
- para adelgazar mas, ya no es un simple cambio de tabla: es una decision de arquitectura sobre autenticacion.

## Problemas concretos que conviene corregir

### 1. Doble autoridad sobre `AllowedWebsites`

- Existe como entidad EF.
- Pero tambien se crea por SQL manual en el seeder.

Debe quedarse solo una autoridad: migraciones EF.

### 2. Doble autoridad sobre `DocumentPublications`

- Existe como entidad EF y relacion 1:1.
- Pero tambien se crea por SQL manual en el seeder.

Debe pasar a migracion formal y salir del seeder.

### 3. `Documents.IsPublic` y `Documents.PublishedAt` no deberian depender del seeder

- Son columnas estructurales de la tabla `Documents`.
- Deben estar en una migracion, no en un `ALTER TABLE` manual al arrancar.

### 4. `CreativeSpaces.MediaReferencesJson` ya muestra una historia de parche

- Su migracion actual es idempotente porque hubo un arreglo manual previo.
- Esto confirma que el proyecto ha mezclado cambios rapidos en produccion/desarrollo con migraciones oficiales.

## Ruta recomendada para recuperar control

1. Mover toda modificacion estructural del seeder a migraciones nuevas.
2. Dejar `DataSeeder` solo para datos iniciales.
3. Verificar que `ApplicationDbContextModelSnapshot` refleja el esquema deseado.
4. Probar sobre una base limpia para confirmar que todo se crea sin depender del seeder.

## Regla mental correcta para este proyecto

No pienses:

- "La base se crea desde el DbContext"

Piensa asi:

- "El `DbContext` define el modelo"
- "Las migraciones construyen el esquema"
- "Identity aporta tablas base de autenticacion"
- "El seeder solo deberia meter datos iniciales"

## Siguiente paso recomendado

La mejora mas util ahora mismo es refactorizar `DataSeeder` para que deje de tocar esquema y crear una o varias migraciones que formalicen:

- `AllowedWebsites`
- `DocumentPublications`
- `Documents.IsPublic`
- `Documents.PublishedAt`

Esa limpieza te dejaria una base mucho mas predecible y facil de mantener.