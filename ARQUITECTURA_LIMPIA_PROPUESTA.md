# Propuesta de Evolucion Arquitectonica de LifeHub

## 1. Objetivo del documento

Este documento consolida el estudio tecnico de la arquitectura actual de LifeHub y propone una evolucion hacia una arquitectura mas limpia, mantenible y escalable, con separacion clara de responsabilidades.

El enfoque esta pensado para ejecutarse de forma incremental, evitando reescrituras completas y minimizando riesgo funcional.

## 2. Alcance del estudio

Se analizo el estado actual en:

- Backend: ASP.NET Core + EF Core + Identity + JWT + SignalR
- Frontend: Angular (standalone components) + services HTTP + guards + interceptors
- Estructura de carpetas y acoplamientos principales
- Distribucion de responsabilidades en capas
- Puntos de complejidad y deuda tecnica
- Base de pruebas automatizadas

## 3. Estado actual de la arquitectura

### 3.1 Backend

Arquitectura actual orientada a API por controladores:

- Controllers con acceso directo a ApplicationDbContext
- Controllers con logica de negocio, validaciones y reglas de autorizacion
- Modelos de dominio y DTOs coexistiendo en el mismo proyecto
- Infraestructura (EF, Identity, JWT, DataSeeder) acoplada a capa Web

Fortalezas actuales:

- Setup de arranque claro en Program.cs
- Uso correcto de JWT e Identity
- Uso consistente de AutoMapper y DbContext
- Base funcional amplia (auth, docs, spaces, friendships, messages, recomendaciones)

Debilidades detectadas:

- Falta de capa Application explicita (casos de uso)
- Controladores con demasiadas responsabilidades
- Reglas de negocio repartidas por endpoints
- Dificultad creciente para testear logica sin levantar HTTP

### 3.2 Frontend

Estructura actual orientada a paginas y servicios HTTP:

- Services con llamadas API + parte de logica de sesion/estado
- Componentes con logica de presentacion y tambien logica de orquestacion
- Guards/interceptors correctos, pero sin capa de estado por feature formalizada

Fortalezas actuales:

- Rutas y layout bien estructurados
- Interceptor JWT implementado
- Servicios por dominio funcional (documents, spaces, users, etc.)
- Uso de resolvers y formularios reactivos

Debilidades detectadas:

- Componentes grandes con muchas responsabilidades
- Estado de feature sin facade/store dedicados
- Mezcla de logica de validacion de negocio dentro de componentes
- Testeo unitario limitado

## 4. Evidencia principal observada

### 4.1 Hotspots de complejidad

- Space workspace frontend supera las 700 lineas y mezcla:
  - render y estado visual
  - drag and drop
  - validacion de embeds
  - llamadas HTTP
  - control de formularios
- CreativeSpacesController backend supera las 400 lineas y mezcla:
  - endpoints
  - permisos
  - validaciones de dominio
  - serializacion/deserializacion de referencias multimedia
  - acceso a base de datos

### 4.2 Acoplamientos relevantes

- Capa web backend acoplada a EF (DbContext directo en controllers)
- Reglas de negocio acopladas a controllers y componentes
- Algunas validaciones de medios embebidos repartidas entre frontend y backend

### 4.3 Cobertura de pruebas

- Cobertura automatizada detectada: limitada
- Riesgo: cambios de arquitectura sin arnes de pruebas adecuado pueden introducir regresiones

## 5. Diagnostico arquitectonico

La arquitectura actual es valida para MVP/primeras iteraciones y ha permitido entregar funcionalidad rapidamente. Sin embargo, ya presenta sintomas tipicos de crecimiento:

- aumento del costo de cambio
- mayor probabilidad de regresion
- menor claridad en limites de responsabilidades
- complejidad alta en puntos concretos

Conclusion: no conviene una reescritura total; conviene una migracion incremental a arquitectura limpia por slices funcionales.

## 6. Arquitectura objetivo recomendada

## 6.1 Backend objetivo (Clean Architecture ligera)

Separar en 4 proyectos:

1. LifeHub.Api
- Controllers
- Contratos HTTP
- Filtros y middleware web
- Mapeo request/response

2. LifeHub.Application
- Casos de uso (commands/queries)
- Reglas de negocio de aplicacion
- Validaciones
- Interfaces de infraestructura (repositorios, clock, servicios externos)

3. LifeHub.Domain
- Entidades y value objects
- Reglas de dominio puras
- Eventos de dominio (si aplica)

4. LifeHub.Infrastructure
- EF Core
- Identity
- Implementaciones de repositorios
- Integraciones externas

Regla de dependencias:

- Api -> Application
- Application -> Domain
- Infrastructure -> Application + Domain
- Domain -> sin dependencias hacia afuera

## 6.2 Frontend objetivo (Feature-first con capas)

Por cada feature principal (ejemplo spaces):

- features/spaces/ui
- features/spaces/application (facade)
- features/spaces/data-access (api clients)
- features/spaces/domain (modelos y reglas locales)

Capas comunes:

- core/auth
- core/http
- core/config
- shared/ui
- shared/utils

## 7. Principios de diseno a adoptar

1. Un caso de uso, una responsabilidad
2. Controllers/componentes finos
3. Reglas de negocio fuera de adaptadores
4. Dependencias hacia adentro (dominio)
5. Contratos estables en limites de capa
6. Test unitario en Application/Domain antes que en Web
7. Cambios incrementales, feature por feature

## 8. Propuesta de migracion incremental

## Fase 0 - Preparacion

Objetivo:

- Estabilizar base para migracion

Acciones:

- Definir convenciones de carpetas y nomenclatura
- Acordar formato de errores estandar
- Acordar estilo de comandos/queries
- Crear baseline de pruebas smoke

Entregable:

- ADR inicial de arquitectura y convenciones

## Fase 1 - Primer vertical slice (Spaces)

Objetivo:

- Validar el nuevo patron sobre un modulo de alto impacto

Acciones backend:

- Extraer casos de uso desde CreativeSpacesController:
  - GetCreativeSpace
  - AddMediaReference
  - ShareCreativeSpace
  - RemovePermission
- Introducir interfaces de repositorio para espacios/permisos
- Mantener contrato HTTP actual (sin breaking changes)

Acciones frontend:

- Crear SpacesFacade
- Mover orquestacion de estado/carga fuera del componente principal
- Mantener UX actual

Entregable:

- Primer modulo con separacion de capas efectiva

## Fase 2 - Documents y Friendships

Objetivo:

- Repetir patron y consolidar consistencia

Acciones:

- Mover logica de DocumentsController y FriendshipsController a Application
- Introducir servicios de dominio para reglas recurrentes
- Ajustar frontend de documentos con facade ligera

Entregable:

- 3 modulos clave usando patron comun

## Fase 3 - Auth y sesion

Objetivo:

- Endurecer seguridad y ciclo de sesion

Acciones:

- Backend: centralizar emision/validacion de JWT en servicio dedicado
- Frontend: separar AuthApiClient, TokenStorage y AuthSessionStore
- Homologar manejo de 401/403

Entregable:

- Flujo de autenticacion con limites claros por capa

## Fase 4 - Cross-cutting y calidad

Objetivo:

- Escalabilidad operativa y calidad sostenida

Acciones:

- Middleware global de excepciones + ProblemDetails
- Logging estructurado y correlation id
- Telemetria basica por endpoint/caso de uso
- Pruebas de integracion para endpoints criticos

Entregable:

- Plataforma lista para crecimiento

## 9. Plan de pruebas recomendado

## 9.1 Backend

Prioridad de pruebas:

1. Unit tests en Application (handlers/casos de uso)
2. Unit tests en Domain (reglas puras)
3. Integration tests API para rutas criticas

Casos minimos iniciales:

- AddMediaReference valida dominio permitido y deniega no permitido
- ShareCreativeSpace valida ownership
- Documents solo retorna/actualiza del usuario autenticado
- Auth/login devuelve claims/roles esperados

## 9.2 Frontend

Prioridad:

1. Facades y servicios de estado
2. Interceptors/guards
3. Componentes complejos (solo logica clave)

Casos minimos:

- 401 en interceptor fuerza logout + redireccion
- SpacesFacade sincroniza cargas de documentos/media
- AuthSessionStore refleja estado de usuario/token correctamente

## 10. Riesgos y mitigaciones

Riesgo 1: migracion larga sin valor visible

- Mitigacion: ejecutar por vertical slices con entregas cada sprint

Riesgo 2: regresiones funcionales

- Mitigacion: pruebas baseline + pruebas de contrato + despliegues graduales

Riesgo 3: sobreingenieria temprana

- Mitigacion: Clean Architecture ligera, sin capas innecesarias

Riesgo 4: friccion del equipo por nuevos patrones

- Mitigacion: plantillas, ejemplos de referencia y revisiones de codigo enfocadas

## 11. Backlog inicial sugerido

Sprint 1 (alto impacto, bajo riesgo):

- Crear estructura Application/Domain/Infrastructure en backend
- Mover 2 casos de uso de CreativeSpaces
- Crear SpacesFacade en frontend
- Agregar pruebas unitarias de esos casos

Sprint 2:

- Completar migracion de Spaces
- Migrar Documents
- Estandarizar manejo de errores API

Sprint 3:

- Migrar Friendships
- Refactor de AuthService en frontend por subservicios
- Aumentar cobertura de pruebas criticas

## 12. Criterios de exito

1. Controllers y componentes significativamente mas pequenos
2. Casos de uso testeables sin HTTP/UI
3. Menor tiempo medio para implementar cambios en features existentes
4. Menor tasa de regresion en modulos migrados
5. Contratos API estables durante la transicion

## 13. Recomendacion final

La mejor estrategia para LifeHub es evolucion incremental a arquitectura limpia por vertical slices, empezando por Spaces. Es el punto donde actualmente se concentra mas complejidad y donde el retorno tecnico sera mas alto.

No se recomienda reescribir todo de una vez. Si se ejecuta en fases cortas, con pruebas y sin romper contratos, el proyecto ganara mantenibilidad y velocidad de evolucion sin sacrificar estabilidad.

## 14. Anexo: estructura objetivo orientativa

Backend:

- LifeHub.Api/
  - Controllers/
  - Middleware/
  - Extensions/
- LifeHub.Application/
  - Features/
    - Spaces/
      - Commands/
      - Queries/
      - Validators/
  - Common/
- LifeHub.Domain/
  - Entities/
  - ValueObjects/
  - Services/
- LifeHub.Infrastructure/
  - Persistence/
  - Identity/
  - Repositories/
  - Services/

Frontend:

- src/app/core/
  - auth/
  - http/
  - config/
- src/app/shared/
  - ui/
  - utils/
- src/app/features/
  - spaces/
    - ui/
    - application/
    - data-access/
    - domain/
  - documents/
  - friendships/

Este anexo es una guia de destino. La transicion debe respetar la compatibilidad funcional durante todo el proceso.
