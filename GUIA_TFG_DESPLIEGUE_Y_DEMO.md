# Guia TFG: Despliegue Local y Version Publica de LifeHub

## 1. Objetivo

Este documento define una estrategia clara para la defensa del TFG con dos entornos:

- Entorno local reproducible para demostracion en aula
- Entorno publicado accesible desde navegador

La idea es demostrar no solo desarrollo funcional, sino tambien capacidad de despliegue, operacion y mantenimiento.

## 2. Mensaje clave para la exposicion

LifeHub se puede ejecutar de dos formas complementarias:

1. Local, para desarrollo y demo controlada
2. Publica, para acceso remoto y validacion real en entorno cloud

Con esto se cubre ciclo completo de software: construir, desplegar, ejecutar, actualizar base de datos y monitorizar.

## 3. Entorno local para la defensa

## 3.1 Objetivo

Arrancar todo el sistema en pocos minutos, de forma consistente y sin pasos manuales complejos.

## 3.2 Recomendacion operativa

Usar un flujo de arranque unico desde la raiz del repo:

- Comando principal: ./start.ps1 local
- Comando rapido (si ya hay dependencias): ./start.ps1 local-noinstall
- Parada ordenada: ./stop-local.ps1

## 3.3 Que demostrar en local

1. Arranque completo del stack
2. Registro o login de usuario
3. Navegacion por modulos principales
4. Accion de escritura en BD (crear doc, espacio, recomendacion)
5. Verificacion de persistencia tras refrescar

## 3.4 Checklist previo al dia de la defensa

- Docker Desktop funcionando
- Puertos libres (4200, 5000, 1433)
- Script de arranque probado el dia anterior
- Usuario de demo preparado
- Datos semilla listos para mostrar funcionalidades
- Plan B disponible (ver seccion 8)

## 4. Version publica desplegada

## 4.1 Objetivo

Disponer de una URL funcional para que el tribunal pueda acceder desde navegador sin entorno local.

## 4.2 Arquitectura recomendada (simple y defendible)

- Frontend Angular: hosting estatico (ej. Azure Static Web Apps)
- Backend .NET: App Service (o contenedor en App Service)
- Base de datos: Azure SQL
- Secretos: variables de entorno o Key Vault

## 4.3 Ventajas para el TFG

- Demuestras despliegue profesional real
- Mantienes separacion frontend/backend/DB
- Puedes explicar seguridad basica y buenas practicas
- Facil de evaluar por el tribunal (solo abrir URL)

## 4.4 Minimos tecnicos que debes dejar cerrados

- CORS backend permitiendo dominio del frontend desplegado
- Connection string cloud correcta
- JWT key fuera del repositorio
- Migraciones aplicadas en la base cloud
- HTTPS habilitado

## 5. Migraciones y actualizacion de base de datos

## 5.1 Que contar en la defensa

- Las migraciones permiten evolucionar el esquema sin perder datos existentes
- Al arrancar, el backend aplica migraciones pendientes automaticamente
- Esto evita cambios manuales en SQL en cada version

## 5.2 Mensaje tecnico breve

- Si empiezas desde cero, la migracion inicial crea el esquema completo
- Si el modelo cambia, se generan migraciones nuevas para actualizar la base actual
- Sin migraciones, el mantenimiento del esquema se vuelve manual y propenso a errores

## 6. Guion sugerido de exposicion (10-12 minutos)

## 6.1 Introduccion (1-2 min)

- Problema y objetivo de LifeHub
- Tecnologias usadas
- Enfoque de despliegue dual (local + cloud)

## 6.2 Demo local (3-4 min)

1. Mostrar arranque del entorno local
2. Entrar a la app
3. Ejecutar flujo funcional corto (login, crear recurso, guardar)
4. Mostrar que los datos persisten

## 6.3 Demo publica (3-4 min)

1. Abrir URL desplegada
2. Repetir flujo funcional breve
3. Explicar separacion de capas (frontend, API, DB)
4. Explicar como se gestionan migraciones y configuracion

## 6.4 Cierre tecnico (2 min)

- Lecciones aprendidas
- Retos resueltos
- Mejoras futuras (arquitectura mas limpia por capas)

## 7. Preguntas tipicas del tribunal y respuesta corta

Pregunta: Como actualizas la base sin perder datos?

Respuesta: Con migraciones de EF Core. Cada cambio de modelo genera una migracion versionada y el backend aplica las pendientes al arrancar.

Pregunta: Que pasa si falla el despliegue cloud?

Respuesta: La defensa no depende de cloud al 100%. Tambien tengo entorno local reproducible con scripts y datos preparados.

Pregunta: Como gestionas seguridad basica?

Respuesta: JWT para autenticacion, control de CORS, HTTPS, y secretos en configuracion de entorno, no embebidos en codigo.

## 8. Plan B para la defensa (muy recomendado)

Si falla internet, Azure o credenciales:

1. Ejecutar demo local completa
2. Mostrar capturas o video corto de la version publica grabado previamente
3. Enseñar diagrama de arquitectura y logs de despliegue

Esto reduce riesgo y transmite buena planificacion.

## 9. Mejoras futuras propuestas

- Evolucion a arquitectura limpia por capas (Api, Application, Domain, Infrastructure)
- Mayor cobertura de tests automatizados
- Pipeline CI/CD con despliegue automatizado
- Observabilidad con logs estructurados y metricas

## 10. Checklist final (dia de exposicion)

- Repo en estado limpio y version etiquetada
- Script local probado en frio
- URL publica comprobada
- Usuario demo y datos listos
- Slides con arquitectura y flujo de despliegue
- Plan B preparado

Con este enfoque, el TFG demuestra ciclo completo de desarrollo y operacion, no solo implementacion de funcionalidades.
