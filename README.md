# LifeHub

**LifeHub** es un portal web completo desarrollado con **Angular** (frontend) y **.NET 8** (backend), diseñado como un entorno centralizado para herramientas útiles del día a día.

## 🚀 Características Principales

### ✅ Sistema de Autenticación
- Registro e inicio de sesión seguros
- Autenticación basada en JWT
- Gestión de permisos y roles
- Persistencia de sesión (localStorage)
- Cierre de sesión con un clic

### 👥 Gestión de Amigos
- Solicitudes de amistad
- Aceptar/rechazar solicitudes
- Lista de amigos

### 💬 Chat en Tiempo Real
- Mensajería instantánea con SignalR
- Notificaciones de mensajes leídos
- Conversaciones uno a uno

### 📚 Recomendaciones
- Recomendar películas, series y libros
- Sistema de calificaciones
- Comentarios en recomendaciones

### 📄 Gestión de Documentos
- Crear, editar y eliminar documentos
- Editor de texto en línea
- Diferentes tipos de documentos (notas, archivos, listas)

### 🎵 Reproductor de Música
- Registro de archivos locales
- Metadatos de canciones (artista, álbum, duración)
- Gestión de biblioteca local

## 🏗️ Arquitectura

### Backend (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- SignalR para chat en tiempo real
- JWT para autenticación
- AutoMapper para mapeo de DTOs

### Frontend (Angular 19)
- Standalone Components
- Reactive Forms
- HttpClient con interceptores
- Routing y Guards
- Services para comunicación HTTP
- SignalR para chat

## 📋 Requisitos

- **.NET 8 SDK** o superior
- **Node.js 20+** y npm
- **SQL Server 2019+** (o LocalDB)
- **Visual Studio Code** o **Visual Studio 2022**

## 🚀 Inicio Rápido

### Opción 1: Desarrollo Híbrido (Recomendado - Mejor Rendimiento)

**Requisitos:** Docker Desktop + Node.js 20+

**Terminal 1 - Backend + Database en Docker:**
```bash
docker compose -f docker-compose.dev.yml up
```

**Terminal 2 - Frontend Local:**
```bash
cd LifeHub-Frontend
npm install
npm start
```

✅ **Ventajas:**
- Frontend ultra-rápido con hot-reload instantáneo
- Backend y Database aislados en contenedores
- Mejor experiencia de desarrollo

### Opción 2: Todo en Docker

```bash
docker compose up
```

### Opción 3: Todo Local

**Requisitos:** .NET 8 SDK + Node.js 20+ + SQL Server

**Terminal 1 - Backend:**
```bash
cd LifeHub-Backend
dotnet restore
dotnet ef database update
dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd LifeHub-Frontend
npm install
npm start
```

---

**Acceso:**
- Frontend: http://localhost:4200
- Backend: http://localhost:5000
- Swagger: http://localhost:5000/swagger

## ⚡ Guía Detallada (Flujo Híbrido)

El flujo habitual es arrancar **backend + base de datos en Docker** y el **frontend en local**:

### Terminal 1 — Backend y base de datos

```powershell
docker compose -f docker-compose.dev.yml up -d mssql backend
```

Verificar que el backend responde:

```powershell
Invoke-WebRequest http://localhost:5000/swagger/v1/swagger.json -UseBasicParsing
```

Si devuelve `200`, el backend está listo.

### Terminal 2 — Frontend local

```powershell
cd LifeHub-Frontend
npm install
npm run start
```

### Solución de problemas comunes

**Puerto 4200 ocupado:**
```powershell
npm run start -- --port 4201
```

**Backend no responde:**
```powershell
docker compose -f docker-compose.dev.yml logs backend --tail 100
```

**Reiniciar Docker de desarrollo:**
```powershell
docker compose -f docker-compose.dev.yml down
docker compose -f docker-compose.dev.yml up -d mssql backend
```

## 📁 Estructura del Proyecto

```
LifeHub/
├── LifeHub-Backend/          # API REST .NET 8
│   ├── Controllers/          # Endpoints
│   ├── Models/               # Entidades de BD
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Services/             # Lógica de negocio
│   ├── Data/                 # DbContext y Migrations
│   └── Hubs/                 # SignalR Hubs
├── LifeHub-Frontend/         # Aplicación Angular
│   ├── src/
│   │   ├── app/
│   │   │   ├── models/       # Interfaces
│   │   │   ├── services/     # HTTP Services
│   │   │   ├── pages/        # Componentes de página
│   │   │   ├── guards/       # Auth Guards
│   │   │   └── interceptors/ # HTTP Interceptors
│   │   ├── assets/           # Archivos estáticos
│   │   └── styles.scss       # Estilos globales
│   └── angular.json          # Configuración Angular
└── docker-compose.yml        # Orquestación Docker

```

## 🔐 Seguridad

- ✅ Autenticación basada en JWT
- ✅ Hash de contraseñas con Identity
- ✅ HTTPS en desarrollo y producción
- ✅ Validación en servidor y cliente
- ✅ Protección de rutas con Guards
- ✅ No almacenamiento de contenido protegido por derechos de autor

## 📝 Notas Importantes

- **No almacenamos archivos multimedia** - Los archivos de música y documentos se almacenan en el dispositivo del usuario
- **Enlaces externos** - Integramos contenido mediante enlaces oficiales de servicios como YouTube, Spotify, etc.
- **GDPR Compliant** - Respetamos la privacidad del usuario

## 👥 Contribución

1. Fork el proyecto
2. Crea una rama (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo LICENSE para detalles.

## 📧 Contacto

Para soporte y preguntas: support@lifehub.local

---

**LifeHub** - Tu portal personal para herramientas útiles del día a día
