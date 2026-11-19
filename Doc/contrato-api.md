# Contrato de la API

Este contrato describe las rutas HTTP del proyecto Gran DT. Las respuestas se representan como JSON.

## Convenciones generales

- Los códigos `400`, `404` y `409` tienen el siguiente significado:
  - `400 Bad Request`: datos inválidos o incompletos.
  - `404 Not Found`: el recurso solicitado no existe.
  - `409 Conflict`: el recurso entra en conflicto con una regla o ya existe.
- Las cuentas `administrador` e `invitado` son cuentas de MySQL creadas para permisos de base de datos. No representan autenticación ni autorización HTTP.
- La cotización se expresa como un número decimal y puede ser como máximo `99999999.99`.
- La puntuación debe estar entre `1.0` y `10.0`.
- `nroFecha` representa la jornada y debe estar entre `1` y `49`.
- Los importes no deben superar `100000000.00`.
- Las respuestas de error pueden incluir:

```json
{
  "status": 400,
  "title": "Los datos enviados no son válidos.",
  "errors": {}
}
```

## 1. Listar equipos

**GET** `/api/equipos`

### Respuesta: `200 OK`

```json
[
  {
    "idEquipo": 1,
    "nombre": "River Plate"
  },
  {
    "idEquipo": 2,
    "nombre": "Boca Juniors"
  }
]
```

## 2. Listar futbolistas

**GET** `/api/futbolistas`

### Respuesta: `200 OK`

```json
[
  {
    "idFutbolista": 1,
    "nombre": "Franco",
    "apellido": "Armani",
    "apodo": "El Pulpo",
    "fechaNac": "1986-10-16",
    "cotizacion": 15000000.00,
    "idEquipo": 1,
    "idTipoFutbolista": 1
  }
]
```

## 3. Crear futbolista

**POST** `/api/futbolistas`

### Entrada

```json
{
  "nombre": "Lionel",
  "apellido": "Prueba",
  "apodo": "Leo",
  "fechaNac": "2000-06-24",
  "cotizacion": 25000000.00,
  "idEquipo": 1,
  "idTipoFutbolista": 4,
  "puntuaciones": [
    {
      "puntuacion": 8.5,
      "nroFecha": 1
    }
  ]
}
```

### Respuestas

- `201 Created`: futbolista creado.
- `400 Bad Request`: campo obligatorio ausente, cotización fuera de rango, jornada inválida o fecha inválida.
- `409 Conflict`: el futbolista ya tiene una puntuación duplicada para la misma fecha.

```json
{
  "idFutbolista": 63,
  "nombre": "Lionel",
  "apellido": "Prueba",
  "apodo": "Leo",
  "fechaNac": "2000-06-24",
  "cotizacion": 25000000.00,
  "idEquipo": 1,
  "idTipoFutbolista": 4,
  "puntuaciones": [
    {
      "idPuntuacion": 101,
      "idFutbolista": 63,
      "puntuacion": 8.5,
      "nroFecha": 1
    }
  ]
}
```

## 4. Actualizar futbolista

**PUT** `/api/futbolistas/{id}`

### Entrada

```json
{
  "nombre": "Lionel",
  "apellido": "Actualizado",
  "apodo": "Leo",
  "fechaNac": "2000-06-24",
  "cotizacion": 27500000.00,
  "idEquipo": 2,
  "idTipoFutbolista": 4
}
```

### Respuestas

- `200 OK`: devuelve el futbolista actualizado.
- `400 Bad Request`: cotización o datos personales inválidos.
- `404 Not Found`: el `id` no corresponde a un futbolista existente.

```json
{
  "idFutbolista": 63,
  "nombre": "Lionel",
  "apellido": "Actualizado",
  "apodo": "Leo",
  "fechaNac": "2000-06-24",
  "cotizacion": 27500000.00,
  "idEquipo": 2,
  "idTipoFutbolista": 4,
  "puntuaciones": []
}
```

## 5. Crear usuario

**POST** `/api/usuarios`

### Entrada

```json
{
  "nombre": "Ana",
  "apellido": "Ejemplo",
  "email": "ana@example.com",
  "fechaNac": "2000-03-15",
  "contrasena": "ClaveDePruebaTemporal",
  "es_admin": false
}
```

### Respuestas

- `201 Created`: usuario creado sin exponer la contraseña.
- `400 Bad Request`: email, contraseña o campos obligatorios inválidos.
- `409 Conflict`: el email ya está registrado.

```json
{
  "idUsuario": 12,
  "nombre": "Ana",
  "apellido": "Ejemplo",
  "email": "ana@example.com",
  "fechaNac": "2000-03-15",
  "es_admin": false
}
```

La contraseña debe almacenarse como hash; nunca se devuelve en el cuerpo de respuesta.

## 6. Registrar puntuación

**POST** `/api/futbolistas/{id}/puntuaciones`

### Entrada

```json
{
  "puntuacion": 8.5,
  "nroFecha": 7
}
```

### Respuestas

- `201 Created`: puntuación registrada.
- `400 Bad Request`: puntuación fuera de `1.0–10.0` o jornada fuera de `1–49`.
- `404 Not Found`: el futbolista no existe.
- `409 Conflict`: ya existe puntuación para ese futbolista y jornada.

```json
{
  "idPuntuacion": 102,
  "idFutbolista": 63,
  "puntuacion": 8.5,
  "nroFecha": 7
}
```

## 7. Listar plantillas de un usuario

**GET** `/api/usuarios/{id}/plantillas`

### Respuesta: `200 OK`

```json
[
  {
    "idPlantilla": 1,
    "nombreP": "Mi equipo",
    "presupuesto": 90000000.00,
    "cantFutbolista": 20,
    "idUsuario": 12
  }
]
```

### Respuesta adicional

- `404 Not Found`: el usuario no existe.

## 8. Crear plantilla de un usuario

**POST** `/api/usuarios/{id}/plantillas`

### Entrada

```json
{
  "nombreP": "Mi equipo",
  "presupuesto": 90000000.00,
  "cantFutbolista": 20
}
```

### Respuestas

- `201 Created`: plantilla creada.
- `400 Bad Request`: nombre vacío, presupuesto fuera de rango o cantidad fuera de `1–20`.
- `404 Not Found`: el usuario no existe.

```json
{
  "idPlantilla": 1,
  "nombreP": "Mi equipo",
  "presupuesto": 90000000.00,
  "cantFutbolista": 20,
  "idUsuario": 12,
  "detalles": []
}
```

## 9. Incorporar futbolistas a una plantilla

**POST** `/api/plantillas/{id}/jugadores`

### Entrada

```json
{
  "idFutbolista": 63,
  "futbolistaTitular": true
}
```

### Respuestas

- `200 OK`: devuelve la plantilla actualizada.
- `400 Bad Request`: cantidad inválida, cotización fuera de rango o datos incompletos.
- `404 Not Found`: no existe la plantilla o el futbolista.
- `409 Conflict`: el futbolista ya está en la plantilla o se supera su cantidad máxima.

```json
{
  "idPlantilla": 1,
  "nombreP": "Mi equipo",
  "presupuesto": 90000000.00,
  "cantFutbolista": 20,
  "idUsuario": 12,
  "detalles": [
    {
      "idPlantilla": 1,
      "idFutbolista": 63,
      "futbolistaTitular": true
    }
  ]
}
```

## 10. Cambiar titular o suplente

**PUT** `/api/plantillas/{id}/jugadores/{futbolistaId}`

### Entrada

```json
{
  "futbolistaTitular": false
}
```

### Respuestas

- `200 OK`: condición actualizada.
- `400 Bad Request`: el campo `futbolistaTitular` falta o no es booleano.
- `404 Not Found`: no existe la plantilla o el futbolista no pertenece a ella.

```json
{
  "idPlantilla": 1,
  "idFutbolista": 63,
  "futbolistaTitular": false
}
```

## 11. Consultar un futbolista

**GET** `/api/futbolistas/{id}`

### Respuesta: `200 OK`

```json
{
  "idFutbolista": 63,
  "nombre": "Lionel",
  "apellido": "Actualizado",
  "apodo": "Leo",
  "fechaNac": "2000-06-24",
  "cotizacion": 27500000.00,
  "idEquipo": 2,
  "idTipoFutbolista": 4,
  "puntuaciones": []
}
```

### Respuesta adicional

- `404 Not Found`: el futbolista no existe.

## 12. Consultar una plantilla

**GET** `/api/plantillas/{id}`

### Respuesta: `200 OK`

```json
{
  "idPlantilla": 1,
  "nombreP": "Mi equipo",
  "presupuesto": 90000000.00,
  "cantFutbolista": 20,
  "idUsuario": 12,
  "detalles": [
    {
      "idPlantilla": 1,
      "idFutbolista": 63,
      "futbolistaTitular": true
    }
  ]
}
```

### Respuesta adicional

- `404 Not Found`: la plantilla no existe.
