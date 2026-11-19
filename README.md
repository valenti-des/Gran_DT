# Gran DT

Integrantes: Valentin Destri y Fernando Alcalá.

## Requisitos

- .NET SDK 10.
- MySQL disponible localmente o en un servidor accesible.

## Preparar la base de datos

Desde la raíz del repositorio, ejecuta los scripts en este orden con un cliente
MySQL:

```bash
mysql -u root -p < scripts/DDl.Sql
mysql -u root -p < scripts/inserts.Sql
mysql -u root -p < scripts/procedure.sql
mysql -u root -p < scripts/Triggers.sql
```

**Atención:** `DDl.Sql` elimina y vuelve a crear la base `bd5_gran_dt`. No lo
ejecutes si necesitas conservar los datos existentes.

La API obtiene la conexión de `ConnectionStrings:GranDT`. Configúrala para tu
servidor con User Secrets (reemplaza los valores de ejemplo):

```bash
dotnet user-secrets set "ConnectionStrings:GranDT" "Server=127.0.0.1;Port=3306;Database=bd5_gran_dt;User Id=TU_USUARIO;Password=TU_CLAVE;" --project src/GranDTApi/GranDTApi.csproj
```

## Ejecutar la API

Desde la raíz del repositorio:

```bash
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/GranDTApi/GranDTApi.csproj
```

La consola indicará las direcciones HTTP/HTTPS disponibles. En desarrollo, la
documentación interactiva de Scalar se encuentra en `/scalar` y el documento
OpenAPI en `/openapi/v1.json`.

## Compilar y probar

```bash
dotnet build src/src.slnx
dotnet test src/TestUnitarios/TestUnitarios.csproj
```

Las pruebas de integración requieren MySQL y datos iniciales. Consulta
[`TESTING.md`](TESTING.md) para configurarlas y ejecutar el resto de las pruebas.
