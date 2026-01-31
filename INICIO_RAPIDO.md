## 🚀 Inicio Rápido

### **Opción 1: Desarrollo Híbrido (Recomendado - Mejor Rendimiento)**

#### Requisitos: Docker Desktop + Node.js 20+

Esta configuración te da el **mejor rendimiento**: Backend/DB en Docker (consistencia) + Frontend local (velocidad).

**Terminal 1 - Backend y Database:**
```powershell
cd LifeHub
docker compose -f docker-compose.dev.yml up
```

**Terminal 2 - Frontend (en otra terminal):**
```powershell
cd LifeHub-Frontend
npm install
npm start
```

✅ Espera 30-40 segundos para backend/DB, luego:
- Frontend: http://localhost:4200 (hot-reload ultra-rápido ⚡)
- Backend: http://localhost:5000
- Database: localhost:1433

---

### **Opción 2: Todo en Docker (Más Lento)**

#### Requisito: Docker Desktop

**Nota:** Esta opción tarda más en compilar y el hot-reload es más lento.

**En Windows (PowerShell):**
```powershell
cd LifeHub
docker compose up
```

**En Mac/Linux (Terminal):**
```bash
cd LifeHub
docker compose up
```

✅ Espera 2-5 minutos en primera ejecución

---

### **Opción 3: Local (Sin Docker)**

#### Requisitos:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [SQL Server Express](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) o LocalDB

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

✅ Abre http://localhost:4200

---

### **Opción 3: Dev Container (VS Code)**

Si tienes instalada la extensión "Dev Containers":

1. Abre la carpeta LifeHub en VS Code
2. Ctrl+Shift+P → "Dev Containers: Reopen in Container"
3. Espera a que configure el contenedor
4. Abre terminal integrada → `npm start` en LifeHub-Frontend

---

## 🔐 Credenciales Predeterminadas

```
Email: admin@lifehub.com
Password: Admin123!
```

---

## 📝 Problemas Comunes

### Docker no inicia
- Asegúrate de que Docker Desktop está ejecutándose
- En Windows, verifica que WSL2 está instalado

### Puerto 4200 o 5000 en uso
- Cambia el puerto en docker-compose.yml:
  ```yaml
  ports:
    - "4201:4200"  # Cambiar 4201 al puerto que quieras
  ```

### Base de datos no conecta (local)
- Verifica SQL Server está ejecutándose
- Comprueba la conexión en appsettings.json

---

## 📚 Documentación Completa

Ver [GUIA_INSTALACION.md](./GUIA_INSTALACION.md) para instrucciones detalladas.
