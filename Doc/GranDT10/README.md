# Gran DT .NET 10

Solución organizada por capas en `Doc/GranDT10`: `GranDT.Dominio`, `GranDT.Aplicacion`, `GranDT.Infraestructura`, `GranDT.Api` y `GranDT.Pruebas`. La API implementa las doce rutas de [contrato-api.md](../contrato-api.md), además de consultas para validar una plantilla y calcular el puntaje de una jornada.

## Requisitos

- .NET SDK 10.
- MySQL 8 compatible con el esquema de [00_ddl.sql](../00_ddl.sql).
- Un usuario MySQL local con permisos `SELECT`, `INSERT` y `UPDATE` sobre `bd_gran_dt`.

## Base de datos

Desde la raíz del repositorio, iniciar MySQL local o un contenedor:

```sh
docker run --name grantd-mysql -e MYSQL_ROOT_PASSWORD='CAMBIAR_LOCALMENTE' -e MYSQL_DATABASE=bd_gran_dt -p 3306:3306 -d mysql:8.4
```

Inicializar el esquema y los datos de ejemplo:

```sh
mysql -h 127.0.0.1 -u root -p < Doc/00_ddl.sql
mysql -h 127.0.0.1 -u root -p < Doc/GranDT10/03_migracion_actividad.sql
mysql -h 127.0.0.1 -u root -p < Doc/01_datos.sql
```

La migración se ejecuta una sola vez sobre el esquema anterior: eleva el máximo de jugadores a 20, amplía el almacenamiento del hash y crea la clave única por futbolista y jornada. Si la base ya recibió esos cambios, no volver a ejecutarla. El script `Doc/02_usuarios.sql` crea cuentas de ejemplo para permisos; reemplazá sus contraseñas de marcador antes de usarlo. Esas cuentas no autentican la API.

Creá un usuario de aplicación local con permisos de lectura, escritura e inserción. No guardes su clave en los archivos del repositorio. La API lee la conexión de `ConnectionStrings__GranDT`:

```sh
export ConnectionStrings__GranDT='Server=127.0.0.1;Port=3306;Database=bd_gran_dt;User ID=grantd_app;Password=CLAVE_LOCAL'
```

No hay credenciales en `appsettings.json`; la variable de entorno puede reemplazarse por User Secrets en desarrollo.

## Ejecutar

Desde la raíz del repositorio:

```sh
dotnet build Doc/GranDT10/GranDT10.slnx
dotnet run --project Doc/GranDT10/GranDT.Api/GranDT.Api.csproj --urls http://localhost:5080
```

Abrí Swagger en <http://localhost:5080/swagger>. Cada operación incluye descripción, esquema de entrada y códigos de respuesta. El recorrido reproducible está en [GranDT.Api.http](GranDT.Api.http); usa REST Client de VS Code y requiere la base con los datos iniciales.

## Pruebas

```sh
dotnet test Doc/GranDT10/GranDT10.slnx
```

La suite unitaria no requiere MySQL. Cubre casos válidos e inválidos de presupuesto, formación, jugadores duplicados, correos repetidos, cotizaciones, puntuaciones y hash de contraseña. El archivo HTTP reproduce el recorrido contra la API y una base MySQL real.

## Rutas adicionales de consulta

Además de las doce rutas contractuales, la API ofrece:

- `GET /api/plantillas/{id}/validacion`: informa validez, costo y errores de presupuesto, cantidad o formación.
- `GET /api/plantillas/{id}/puntaje?nroFecha=1`: suma las puntuaciones de los titulares; una jornada sin puntuación aporta cero.
