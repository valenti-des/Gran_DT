
### 1. Objetos y datos que podrías describir

- **Futbolista:** nombre, apellido, apodo, equipo, posición/tipo y cotización. Son datos propios del jugador.
- **Plantilla:** nombre, usuario dueño y presupuesto máximo. Podría tener también un máximo de futbolistas.
- **Jugador en una plantilla** —la relación entre ambos—: si es titular o suplente. Un futbolista puede existir en el sistema sin pertenecer a una plantilla.
- **Puntuación:** futbolista, fecha y puntaje. Es el puntaje de ese jugador en una fecha; la plantilla podría calcular su puntaje total a partir de sus titulares.

### 2. Primer modelo de clases

```text
Usuario 1 ───── 1..* Plantilla
                    |
                    | 1
                    | contiene
                    | 1..*
             PlantillaFutbolista
                    |              \
                    |               \ 1
                    | 1               Futbolista
                    |                  |
                    |                  | 1
                    |                  | 0..*
                    |              Puntuacion
                    |
                    └── titular o suplente
```

`PlantillaFutbolista` representa que un futbolista pertenece a una plantilla y permite guardar si está como titular o suplente. `Puntuacion` asocia un puntaje con un futbolista y una fecha. Este es un **boceto**, no una definición final.

### 3. Qué podría salir mal

- Intentar agregar a la plantilla un futbolista que ya está en ella.
- Superar el presupuesto al agregar un jugador.
- Superar el máximo permitido de futbolistas.
- Dejar la plantilla con demasiados titulares o una composición inválida.
- Consultar una fecha para la que todavía no existen puntuaciones.
- Registrar dos puntuaciones para el mismo futbolista y fecha.
- Intentar agregar un futbolista inexistente o una plantilla que no pertenece al usuario.

### 4. Hipótesis sobre dónde validar

Anotá estas como **hipótesis para conversar**, no como decisiones definitivas:

1. **API/controlador:** validar que la solicitud tenga los datos necesarios y formatos correctos, por ejemplo, un identificador y una fecha válidos.
2. **Servicio o lógica de dominio:** comprobar reglas del negocio, como presupuesto disponible, cantidad máxima de jugadores y si corresponde que el futbolista sea titular o suplente.
3. **Base de datos:** proteger la integridad de las relaciones —por ejemplo, que la plantilla y el futbolista existan— y evitar duplicados que la base pueda detectar.
4. **Consulta del puntaje:** definir en el servicio qué jugadores cuentan para el total de la fecha y qué respuesta dar si falta alguna puntuación.

### 5. Dudas que podés registrar

- ¿Cuántos futbolistas puede tener una plantilla y cuántos deben ser titulares?
- ¿Los suplentes tienen que ser de posiciones específicas?
- ¿Se puede comprar un futbolista si eso supera el presupuesto, o se rechaza la operación?
- ¿El puntaje de la plantilla suma solo a los titulares?
- ¿Qué ocurre si un titular no tiene puntuación cargada para esa fecha?
- ¿La fecha se identifica como una fecha calendario o como un número de jornada?

