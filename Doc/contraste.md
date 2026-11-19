# Actividad 1: Reglas, capas y pruebas pequeñas

Este documento contrasta las hipótesis de `Doc/hipotesis.md` con la implementación realizada en `src plan taller/`.

## Separación de responsabilidades

- **Dominio:** define entidades como `Futbolista`, `Plantilla`, `Puntuacion` y `PlantillaFutbolista`, junto con límites constantes.
- **Aplicación:** contiene casos de uso y interfaces de repositorio. Allí se implementan las reglas del torneo y se transforman las operaciones inválidas en excepciones de negocio.
- **Infraestructura:** implementa los repositorios con Dapper y MySQL. Su responsabilidad principal es leer y persistir datos; no decide reglas propias del torneo.
- **API:** valida el formato básico del modelo de entrada, transforma comandos en llamadas a servicios y convierte excepciones en respuestas HTTP.
- **Pruebas:** prueban los servicios con repositorios en memoria, sin requerir una base MySQL activa.

Esta separación permite cambiar el acceso a datos o el formato HTTP sin duplicar las reglas de negocio.

## Preguntas guía

### ¿Cómo se evita repetir un futbolista en una plantilla?

El servicio `PlantillaFutbolistaService.Incorporar` consulta la relación existente antes de agregarla. Si encuentra el mismo par plantilla-futbolista, rechaza la operación con una excepción de negocio.

Además, el esquema MySQL debe mantener una clave primaria o restricción única sobre `(idPlantilla, idFutbolista)`. La restricción de la base es una defensa final ante escrituras que no pasen por el servicio.

### ¿Qué pasa si falta su puntuación en una fecha?

El método de puntaje de `Futbolista` devuelve `0m` cuando no existe una `Puntuacion` para el número de fecha solicitado. Por lo tanto, el servicio suma cero por ese futbolista si falta su puntuación. No se considera un error del cálculo: la ausencia de puntaje tiene una respuesta numérica conocida.

Una fecha de jornada debe estar entre 1 y 49. Registrar dos puntuaciones para el mismo futbolista y fecha se rechaza buscando primero la puntuación existente y también mediante la clave única o restricción de MySQL correspondiente.

### ¿Qué diferencia hay entre validar datos y verificar permisos de MySQL?

Validar datos significa aplicar reglas del sistema en la aplicación: rangos, campos obligatorios, presupuesto, composición, duplicados y coherencia entre entidades.

Verificar permisos de MySQL significa comprobar si la cuenta conectada puede ejecutar una operación sobre tablas o procedimientos, por ejemplo `SELECT` pero no `INSERT`, `UPDATE` o `DELETE`. Los permisos no reemplazan las validaciones: protegen la base ante aplicaciones, usuarios o sesiones no autorizadas.

Un usuario podría tener permiso SQL válido y aun así intentar una operación prohibida por las reglas del torneo. También puede llegar un valor inválido desde otra aplicación que sí tiene permiso de escritura; por eso MySQL y la aplicación deben aplicar controles compatibles.

## Validaciones y endpoint

### Correspondencia de validaciones

- **Servicios:** presupuesto, máximo de 20 futbolistas, composición de titulares, futbolistas repetidos, cotización fuera de rango, puntuaciones 1–10, jornada 1–49 y ausencia de puntuación.
- **Dominio:** cálculo de puntaje y ausencia de puntuación como cero.
- **API:** estructura, tipos y presencia de parámetros de la solicitud; conversión de identificadores, fecha/jornada y JSON.
- **Base de datos:** claves primarias y foráneas, unicidad de email y pares plantilla-futbolista, además de restricciones finales sobre rangos.

Un endpoint no debería resolver las reglas del torneo porque repetirlas en cada controlador produce versiones distintas de una misma validación. El controlador podría recibir datos válidos para HTTP y, aun así, esos datos ser rechazados por el servicio. Esto permite reutilizar las reglas desde otra API, una consola o pruebas unitarias.

El endpoint debe traducir el resultado: `400` o `409` para una operación inválida, `404` cuando no existe la entidad y `201` o `200` cuando se crea o consulta correctamente.

## Prueba de concepto: Minimal API y JSON

La solución aislada usa controladores ASP.NET en `src plan taller/GranDTApi/`, no un proyecto descartable nuevo, para mantener un archivo de prueba reproducible dentro del entregable. Implementa:

- endpoints para equipos, usuarios, futbolistas y puntuaciones;
- endpoints para crear plantillas, incorporar/remover futbolistas y cambiar titular/suplente;
- consulta de validación y cálculo por `nroFecha`;
- respuestas JSON mediante controladores y `ActionResult`;
- documentación OpenAPI con `AddOpenApi()`;
- interfaz interactiva Scalar con `MapScalarApiReference()` en desarrollo.

La configuración observada fue:

```csharp
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
```

Swagger/OpenAPI se configura al registrar `Microsoft.AspNetCore.OpenApi` y exponer el documento OpenAPI. Scalar sirve el explorador interactivo de ese documento. Para conectarse a MySQL se configura `ConnectionStrings:GranDT` o `GRANDT_CONNECTION_STRING` mediante variables de entorno o configuración local, nunca con una contraseña real en el repositorio.

### Resultado observado

Se verificó la solución con:

```sh
dotnet build "src plan taller/src.slnx"
dotnet test "src plan taller/Pruebas/GranDT.Pruebas.csproj"
```

La compilación fue correcta y la suite tuvo 15 pruebas aprobadas. Los casos cubren presupuesto, composición, duplicados, rango de puntuación, jornada, ausencia de puntaje, cotización, alta con puntuación inicial y hash BCrypt.

No se inició una ejecución HTTP contra MySQL porque no se dispuso de una cadena de conexión real ni de credenciales locales verificadas. La prueba de concepto quedó comprobada a nivel de compilación y de servicios con repositorios en memoria.

## Contraste con `Doc/hipotesis.md`

Las hipótesis eran mayormente correctas: se anticipó la separación entre API, servicio, base de datos y consulta del puntaje. También se identificaron los riesgos de duplicados, presupuesto, cantidad máxima y falta de puntuaciones.

Cambios respecto del boceto:

1. **Cantidad máxima:** se resolvió en 20 futbolistas por plantilla.
2. **Presupuesto:** se fijó un máximo de 100.000.000.
3. **Composición:** los titulares deben tener 1 arquero, 4 defensores, 4 mediocampistas y 2 delanteros.
4. **Fecha:** se confirmó el número de jornada entre 1 y 49, en lugar de una fecha calendario.
5. **Puntuación ausente:** aporta cero al puntaje total.
6. **Integridad duplicada:** se combina la comprobación del servicio con la clave primaria o única de `(idPlantilla, idFutbolista)` y `(idFutbolista, nroFecha)`.
7. **Contraseñas:** se usa hash BCrypt en lugar de guardar texto plano.
8. **Permisos:** se mantienen separados de las reglas de negocio; una cuenta MySQL con solo `SELECT` no puede escribir aunque se invoque correctamente el endpoint.

La hipótesis de ubicar las reglas en servicios quedó confirmada. La API debe validar transporte y delegar decisiones del torneo; los repositorios persisten datos; la base agrega integridad física y permisos independientes.
