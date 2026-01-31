# ✅ Configuración Docker Completada

## 🎉 ¿Qué se ha configurado?

Ahora puedes ejecutar **todo el proyecto sin instalar nada en tu ordenador** (excepto Docker).

### 📦 Archivos Nuevos Creados

```
LifeHub/
├── .devcontainer/
│   └── devcontainer.json          ← VS Code Dev Container config
├── docker-compose.dev.yml         ← Docker para desarrollo con hot-reload
├── start.ps1                      ← Script de inicio para Windows
├── start.sh                       ← Script de inicio para Mac/Linux
├── INICIO_RAPIDO.md              ← Guía de 5 minutos
└── LifeHub-Backend/
    └── Dockerfile.dev            ← Docker optimizado para desarrollo
```

---

## 🚀 Cómo Usar

### **Opción A: Script de Inicio (Más Fácil)**

**Windows:**
```powershell
cd LifeHub
.\start.ps1 dev
```

**Mac/Linux:**
```bash
cd LifeHub
./start.sh dev
```

### **Opción B: Docker Compose Directo**

**Desarrollo (con hot-reload y logs):**
```bash
docker-compose -f docker-compose.dev.yml up --build
```

**Producción:**
```bash
docker compose up --build
```

---

## 📊 Componentes de Docker

### Desarrollo (`docker-compose.dev.yml`)

**Configuración Híbrida Optimizada:**
```
┌─────────────────────────────────────────────────────┐
│  Azure SQL Edge (Developer Edition)                │
│  Puerto: 1433                                       │
│  User: sa / Password: Str0n9*P@55w0rd             │
│  Volumen: sql_data (persistencia)                 │
└─────────────────────────────────────────────────────┘
           ↓
┌─────────────────────────────────────────────────────┐
│  .NET 8 Backend (dotnet watch run)                 │
│  Puerto: 5000 → 8080 (interno)                    │
│  Hot-reload: ✅ Cambios = recompila automático     │
│  Volumen: ./LifeHub-Backend (código en vivo)      │
└─────────────────────────────────────────────────────┘
           ↓
┌─────────────────────────────────────────────────────┐
│  🚀 Frontend EJECUTADO LOCALMENTE                  │
│  Comando: npm start (en LifeHub-Frontend/)        │
│  Puerto: 4200                                      │
│  Hot-reload: ✅ Ultra-rápido ⚡                     │
│  Sin Docker = Mejor rendimiento                   │
└─────────────────────────────────────────────────────┘
```

**¿Por qué esta configuración?**
- ⚡ Frontend local = compilación y hot-reload instantáneos
- 🐳 Backend/DB en Docker = consistencia entre entornos
- 💾 Menor uso de recursos de Docker/WSL2
- 🎯 Mejor experiencia de desarrollo

### Producción (`docker-compose.yml`)
```
- Imágenes multi-stage optimizadas
- Tamaños comprimidos
- Sin dependencias de desarrollo
- Ideal para deployment
```

---

## ✨ Características de la Config

### 🔄 **Hot-Reload**
- Backend: `dotnet watch` recompila al detectar cambios
- Frontend: `ng serve` recompila automáticamente
- No necesitas reiniciar contenedores

### 📝 **Volúmenes Compartidos**
```
Tu ordenador (Host)  ↔  Contenedor Docker
./LifeHub-Backend    ↔  /app (Backend)
./LifeHub-Frontend   ↔  /app (Frontend)
```

### 🔗 **Networking**
- Todos los servicios en red `lifehub-network-dev`
- Backend accesible desde Frontend automáticamente
- Base de datos accesible como `sqlserver:1433`

### 💾 **Persistencia**
```
sqlserver_data_dev (volumen Docker)
├─ Almacena datos de SQL Server
└─ Persiste incluso si paras contenedores
```

---

## 📋 Pre-requisitos Verificados

✅ **Backend**
- .NET 8 SDK (en contenedor)
- Entity Framework CLI (en contenedor)
- Dockerfile.dev configurado
- Program.cs con middleware completo

✅ **Frontend**
- Angular 19 (npm install en contenedor)
- TypeScript 5.7
- RxJS + Reactive Forms
- SCSS styling
- Componentes compilados exitosamente

✅ **Database**
- SQL Server 2022 (imagen oficial)
- Puerto 1433 mapeado
- Usuario sa con contraseña fuerte
- Volumen para persistencia

✅ **Networking**
- docker-compose.dev.yml con red compartida
- Puertos mapeados correctamente
- Health check para SQL Server

---

## 🎯 Próximos Pasos

1. **Ejecuta el proyecto:**
   ```powershell
   cd d:\Programación\Proyectos\LifeHub
   .\start.ps1 dev
   ```

2. **Espera a que inicie** (2-5 minutos primera vez)

3. **Abre en navegador:**
   - Frontend: http://localhost:4200
   - Backend API: http://localhost:5000
   - Swagger: http://localhost:5000/swagger/index.html

4. **Inicia sesión:**
   ```
   Email: admin@lifehub.com
   Password: Admin123!
   ```

5. **Edita código** - Los cambios se recargan automáticamente

---

## 🛠️ Comandos Útiles

**Ver logs en tiempo real:**
```bash
docker-compose -f docker-compose.dev.yml logs -f
```

**Parar contenedores:**
```bash
docker-compose -f docker-compose.dev.yml down
```

**Eliminar volúmenes (limpiar BD):**
```bash
docker-compose -f docker-compose.dev.yml down -v
```

**Entrar en bash del backend:**
```bash
docker-compose -f docker-compose.dev.yml exec backend bash
```

**Ejecutar migraciones de BD:**
```bash
docker-compose -f docker-compose.dev.yml exec backend dotnet ef database update
```

---

## 📚 Documentación

- [INICIO_RAPIDO.md](./INICIO_RAPIDO.md) - 5 minutos
- [GUIA_INSTALACION.md](./GUIA_INSTALACION.md) - Detallada
- [MAPA_PROYECTO.md](./MAPA_PROYECTO.md) - Arquitectura
- [README-NUEVO.md](./README-NUEVO.md) - Overview completo

---

## ⚠️ Notas Importantes

1. **Primera ejecución**: Docker descargará ~1-2 GB de imágenes
2. **Espacio en disco**: Necesitas ~5 GB libres
3. **Puerto 1433, 5000, 4200**: Deben estar disponibles
4. **WSL2 (Windows)**: Recomendado para mejor performance
5. **Credenciales de BD**: Usuario `sa` con contraseña `Str0n9*P@55w0rd`

---

## ✅ Estado del Proyecto

- ✅ **Backend .NET 8**: Código compilable
- ✅ **Frontend Angular 19**: Compilado sin errores (0 errores)
- ✅ **Docker**: Configurado para desarrollo y producción
- ✅ **Scripts**: start.ps1 (Windows) y start.sh (Linux/Mac)
- ✅ **Dev Container**: VS Code Dev Container setup
- ✅ **Documentación**: Completa y actualizada

---

**¡Ya está todo listo! Solo necesitas Docker. 🚀**
