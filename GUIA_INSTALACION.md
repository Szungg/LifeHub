# Guía de Instalación y Configuración

## 🚀 Inicio Rápido (Recomendado)

### Opción 1: Desarrollo Híbrido (Mejor Rendimiento) ⭐

**Requisitos:** Docker Desktop + Node.js 20+

Esta configuración te da el mejor rendimiento: Backend/DB en Docker + Frontend local.

**Terminal 1 - Backend y Database:**
```powershell
docker compose -f docker-compose.dev.yml up
```

**Terminal 2 - Frontend:**
```powershell
cd LifeHub-Frontend
npm install
npm start
```

Esto iniciará:
- ✅ **Azure SQL Edge** en `localhost:1433`
- ✅ **Backend** en `http://localhost:5000` (con hot-reload)
- ✅ **Frontend** en `http://localhost:4200` (ultra-rápido ⚡)

**Primera ejecución:** Backend/DB tardan 30-40 segundos, Frontend 15-20 segundos.

### Opción 2: Todo en Docker (Más Lento)

**Requisitos:** Solo Docker Desktop

```powershell
docker compose up
```

**Nota:** Esta opción es más lenta debido al overhead de Docker/WSL2 en el frontend.

---

## 📋 Requisitos Previos

Dependiendo de tu opción de instalación:

### Opción A: Docker (Recomendado)
- **Docker Desktop**: [Descargar](https://www.docker.com/products/docker-desktop)
- **Git**: [Descargar](https://git-scm.com/) (opcional)

### Opción B: Local (Sin Docker)
- **.NET 8 SDK**: [Descargar](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 20+**: [Descargar](https://nodejs.org/)
- **SQL Server 2019+**: [Descargar](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - O usar **LocalDB** que viene con Visual Studio
- **Visual Studio Code** o **Visual Studio 2022**
- **Git**: [Descargar](https://git-scm.com/)

## 🛠️ Instalación Local (Sin Docker)

### 1. Backend (.NET 8)

```bash
# Navegar a la carpeta del backend
cd LifeHub-Backend

# Restaurar paquetes NuGet
dotnet restore

# Aplicar migraciones a la base de datos
dotnet ef database update

# Ejecutar el servidor
dotnet run
```

**Output esperado:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5000
      Now listening on: http://localhost:5000
```

✅ Backend disponible en: `https://localhost:5000`

### 2. Frontend (Angular)

```bash
# Navegar a la carpeta del frontend
cd LifeHub-Frontend

# Instalar dependencias
npm install

# Ejecutar servidor de desarrollo
npm start
```

**Output esperado:**
```
✔ Compiled successfully
✔ Build succeeded

Initial Chunk Files | Names         | Size
main.js             | main          | 500 KB
styles.css          | styles        | 50 KB

Application bundle generation complete. [3 seconds]

Watch mode enabled. Watching for file changes in the workspace directory.
✔ Compiled successfully.
```

✅ Frontend disponible en: `http://localhost:4200`

## 🐳 Instalación con Docker

### Modo Desarrollo (Con Hot-Reload)

```bash
# Desde la raíz del proyecto
docker-compose -f docker-compose.dev.yml up --build
```

Características:
- ✅ Recompilación automática al cambiar código
- ✅ Terminal interactiva en los contenedores
- ✅ Logs en tiempo real

### Modo Producción (Optimizado)

```bash
# Desde la raíz del proyecto
docker compose up --build
```

Características:
- ✅ Imágenes optimizadas y comprimidas
- ✅ Menor consumo de recursos
- ✅ Ideal para testing final

## 🔐 Configuración de la Base de Datos

### Opción A: LocalDB (Recomendado para desarrollo)

En `LifeHub-Backend/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "(localdb)\\mssqllocaldb;Database=LifeHubDB;Integrated Security=true;"
}
```

### Opción B: SQL Server Local

En `LifeHub-Backend/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=LifeHubDB;User=sa;Password=Str0n9*P@55w0rd;TrustServerCertificate=True;"
}
```

### Opción C: SQL Server en Docker

Ya está configurado en `docker-compose.yml`

## 🔑 Variables de Entorno

### Backend

El archivo `appsettings.json` incluye:

- **JWT Key**: Clave para firmar tokens (cambiar en producción)
- **Database Connection**: Cadena de conexión a BD
- **CORS**: Configurado para `http://localhost:4200`

### Frontend

Los servicios usan `http://localhost:5000` como URL base de la API.

Para cambiar en producción, edita los archivos de servicios en `src/app/services/`.

## 🧪 Pruebas Iniciales

### 1. Crear Cuenta

1. Abre `http://localhost:4200`
2. Haz clic en "Registrarse"
3. Completa el formulario con:
   - Email: `usuario@ejemplo.com`
   - Nombre: `Tu Nombre`
   - Contraseña: `Contraseña123!`

### 2. Iniciar Sesión

1. Haz clic en "Iniciar Sesión"
2. Usa las credenciales que acabas de crear
3. Deberías ver la página de inicio

### 3. Explorar Funcionalidades

- **Perfil**: Edita tu información y contraseña
- **Amigos**: Crea solicitudes de amistad
- **Recomendaciones**: Recomienda películas o libros
- **Documentos**: Crea notas y documentos
- **Música**: Registra archivos de tu biblioteca

## 🔍 Swagger API Documentation

Accede a la documentación interactiva de la API:

`https://localhost:5000/swagger/index.html`

Aquí puedes:
- Ver todos los endpoints disponibles
- Probar endpoints directamente
- Ver esquemas de request/response

## 🐛 Solución de Problemas

### Error: "Connection refused" en el backend

**Solución:**
```bash
# Verifica que SQL Server está corriendo
# Si usas Docker:
docker ps | grep sqlserver

# Si usas LocalDB:
sqllocaldb info
sqllocaldb start mssqllocaldb
```

### Error: "Port 5000 is already in use"

**Solución:**
```bash
# Busca el proceso que usa el puerto
netstat -ano | findstr :5000

# Termina el proceso (reemplaza PID)
taskkill /PID <PID> /F

# O cambia el puerto en launchSettings.json
```

### Error: "Module not found" en el frontend

**Solución:**
```bash
# Borra node_modules y reinstala
rm -r node_modules
npm install
```

### Error de migraciones EF Core

**Solución:**
```bash
# Elimina la BD y crea de nuevo
dotnet ef database drop
dotnet ef database update
```

## 📦 Estructura de Carpetas

```
LifeHub/
├── LifeHub-Backend/
│   ├── bin/              # Compilados (generados)
│   ├── obj/              # Objetos compilados (generados)
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Properties/
│   ├── Hubs/
│   ├── Utilidades/
│   └── LifeHub.csproj
│
├── LifeHub-Frontend/
│   ├── node_modules/     # Dependencias (generadas)
│   ├── dist/             # Build (generado)
│   ├── src/
│   │   ├── app/
│   │   │   ├── models/
│   │   │   ├── services/
│   │   │   ├── pages/
│   │   │   ├── guards/
│   │   │   └── interceptors/
│   │   ├── assets/
│   │   ├── styles.scss
│   │   └── main.ts
│   └── angular.json
│
└── docker-compose.yml
```

## 🚀 Despliegue en Producción

### Docker Build

```bash
# Construir imágenes
docker build -t lifehub-backend LifeHub-Backend/
docker build -t lifehub-frontend LifeHub-Frontend/

# Ejecutar contenedores
docker run -d -p 5000:8080 lifehub-backend
docker run -d -p 80:80 lifehub-frontend
```

### Azure Deployment

```bash
# Publica a Azure App Service
dotnet publish -c Release

# O usa GitHub Actions para CI/CD
```

## 📚 Recursos Adicionales

- [Documentación Angular](https://angular.io/docs)
- [Documentación .NET](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SignalR Documentation](https://docs.microsoft.com/en-us/aspnet/core/signalr)

## 💡 Tips Útiles

1. **Debugging en Visual Studio Code:**
   ```bash
   # Backend
   dotnet watch run
   
   # Frontend
   npm start
   ```

2. **Hot Reload:**
   - El backend se recarga automáticamente con `dotnet watch`
   - El frontend se recarga automáticamente con `ng serve`

3. **Base de Datos:**
   - Usa SQL Server Management Studio (SSMS) para inspeccionar la BD
   - Las migraciones se aplican automáticamente al iniciar

4. **Tokens JWT:**
   - Los tokens expiran después de 60 minutos (configurable)
   - Se envían en el header: `Authorization: Bearer <token>`

## ✅ Checklist de Verificación

Antes de considerar el setup completado:

- [ ] Backend compila sin errores
- [ ] BD se crea correctamente
- [ ] Frontend compila sin errores
- [ ] Puedes registrar un usuario
- [ ] Puedes iniciar sesión
- [ ] Puedes acceder a las páginas protegidas
- [ ] API responde en Swagger

---

**¡Listo para comenzar a desarrollar!** 🎉

Si tienes problemas, revisa los archivos README.md en cada carpeta.
