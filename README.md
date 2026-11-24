# Gran DT

Integrantes: Valentin Destri y Fernando Alcalá.

## Antes de empezar debes ingresar esto para poder cambiar de rama de la main a la nueva rama 
- Desde la paleta de comandos:

    Presiona Ctrl + Shift + P (o F1) para abrir la paleta de comandos.
    Escribe Git: Checkout to... y presiona Enter.
    Selecciona la rama de la lista que se muestra 

### Si el metodo normal de cambiar de rama no sirve vuelve a intentar desde una terminal externa (solo para wimdows)

    Abre la terminal normal de tu computadora (busca Símbolo del sistema, CMD o PowerShell en el menú de inicio de Windows).
    Muévete a la carpeta de tu proyecto usando el comando cd. Por ejemplo, si tu proyecto está en una carpeta llamada "Gran_DT" en tus documentos, escribe:
    cmd

**cd Documentos/Gran_DT** (Opcional puedes abrir terminal integrada o terminal normal)

    Usa el código con precaución.
    (Asegúrate de poner la ruta correcta a tu carpeta).
    Ejecuta el comando de actualización desde ahí:
    cmd

**git fetch --all**

    Usa el código con precaución.
    Si el comando termina con éxito y sin errores, ahora escribe esto para cambiarte directamente a la rama que querías:
    cmd

**git checkout nombre-de-tu-nueva-rama**

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

### Compilar y probar

```bash
**Este dotnet servira por si la slnx se niega a compilar el Program se abre directamente el Progam como usando este mismo dotnet run**
dotnet run 
dotnet build src/src.slnx
dotnet test src/TestUnitarios/TestUnitarios.csproj
```

Las pruebas de integración requieren MySQL y datos iniciales. Consulta
[`TESTING.md`](TESTING.md) para configurarlas y ejecutar el resto de las pruebas.



