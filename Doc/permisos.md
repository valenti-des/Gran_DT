# Comprobacion de permisos

Ejecutar los scripts en este orden desde la carpeta del proyecto:

```sh
mysql -u root -p < scripts/00_ddl.sql
mysql -u root -p < scripts/01_datos.sql
```

Antes de ejecutar `scripts/02_usuarios.sql`, reemplazar los dos valores
`REEMPLAZAR_CON_PASSWORD_...` por contrasenas de prueba. Son marcadores, no
credenciales reales. Luego ejecutar:

```sh
mysql -u root -p < scripts/02_usuarios.sql
```

## Prueba como invitado

Al solicitar la contrasena, usar la contrasena de ejemplo configurada para
`invitado` en el script. La lectura debe devolver filas:

```sh
mysql -u invitado -p -h localhost bd_gran_dt \
  -e "SELECT idEquipo, nombre FROM Equipo ORDER BY idEquipo;"
```

Resultado esperado: las dos filas ficticias `Deportivo Horizonte` y `Club
Puerto Norte`.

Intentar una escritura:

```sh
mysql -u invitado -p -h localhost bd_gran_dt \
  -e "INSERT INTO Equipo (nombre) VALUES ('Equipo no autorizado');"
```

Resultado esperado: MySQL rechaza el `INSERT` con `ERROR 1142` (`INSERT
command denied` para el usuario `invitado`). La tabla no debe modificarse.

## Registro de resultados esperado

Esta transcripcion es la salida que se debe confirmar al ejecutar las dos
pruebas anteriores con las cuentas configuradas; no se presenta como una
ejecucion ya realizada:

```text
SELECT idEquipo, nombre FROM Equipo ORDER BY idEquipo;
1  Deportivo Horizonte
2  Club Puerto Norte

INSERT INTO Equipo (nombre) VALUES ('Equipo no autorizado');
ERROR 1142 (42000): INSERT command denied to user 'invitado'@'localhost' for table 'Equipo'
```

Los permisos se pueden volver a consultar con `SHOW GRANTS FOR
'invitado'@'localhost';`: debe aparecer `GRANT SELECT ON bd_gran_dt.*` y no
deben aparecer permisos de escritura. El administrador recibe `ALL PRIVILEGES`
limitados a `bd_gran_dt.*`.

## Restricciones del esquema

- `Usuario.email` tiene una restriccion `UNIQUE`.
- `Puntuacion` usa como clave primaria `(idFutbolista, cantFech)`, por lo que un
  futbolista solo puede tener una puntuacion por fecha. `idPuntuacion` se
  conserva como identificador unico compatible con la aplicacion.
- `Puntuacion.jornada` admite valores de 1 a 49; `cantFech` conserva la fecha
  calendario utilizada por la aplicacion.
- `Futbolista.cotizacion` es `DECIMAL(10,2)` y no puede superar
  `99.999.999,99`.
- `Futbolista_Plantilla.futbolistaTitular` distingue titulares (`1`) de
  suplentes (`0`); las claves foraneas vinculan cada futbolista y plantilla.