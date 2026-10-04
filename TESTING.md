# Pruebas

## Pruebas unitarias

Las pruebas unitarias están en `src/TestUnitarios`. Verifican lógica de dominio
con objetos en memoria: no abren conexiones ni dependen de MySQL u otros servicios.

```powershell
dotnet test src/TestUnitarios/TestUnitarios.csproj
```

## Pruebas de integración

Las pruebas de repositorios están en `src/TestDapper` y marcadas con la categoría
`Integration`. Ejecutan consultas SQL y verifican lectura, escritura y eliminación
contra una base MySQL real:

```powershell
dotnet test src/TestDapper/TestDapper.csproj --filter "Category=Integration"
```

Configura la conexión con las variables de entorno `DB_SERVER`, `DB_NAME`,
`DB_USER`, `DB_PASSWORD` y `DB_PORT`. Los tests de lectura esperan que la base
contenga los datos iniciales utilizados por el proyecto, como River Plate,
`admin@gran_dt.com`, Franco Armani y Plantilla Inicial.

Ejecutar toda la solución incluye las pruebas de integración y, por tanto,
requiere una base MySQL disponible y con esos datos.
