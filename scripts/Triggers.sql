DELIMITER $$
USE bd_gran_dt $$

/*
Trigger para limitar la cantidad de Equipos

El sistema requiere que no haya más de 32 equipos registrados en total. Este trigger cancela la inserción si ya se alcanzó ese número.
*/

DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarEquipo_Insert $$
CREATE TRIGGER TRG_ValidarEquipo_Insert
BEFORE INSERT ON Equipo
FOR EACH ROW
BEGIN
    DECLARE totalEquipos INT;

    SELECT COUNT(*) INTO totalEquipos FROM Equipo;
    
    IF totalEquipos >= 32 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: Se alcanzó el límite máximo de 32 equipos permitidos.';
    END IF;
END $$
DELIMITER ;


/*
Trigger para reglas de Futbolistas (Límite y Cotización)

Asegura que el sistema no supere los 1500 jugadores y que los valores económicos sean lógicos (positivos y no mayores a $99.999.999,99).
*/


DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarFutbolista_Insert $$
CREATE TRIGGER TRG_ValidarFutbolista_Insert
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    DECLARE totalJugadores INT;

    -- 1. Validar que la cotización sea válida
    IF NEW.cotizacion > 99999999.99 OR NEW.cotizacion < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: La cotización debe ser positiva y no puede superar los $99.999.999,99.';
    END IF;

    -- 2. Validar que no se superen los 1500 futbolistas
    SELECT COUNT(*) INTO totalJugadores FROM Futbolista;
    IF totalJugadores >= 1500 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: Se alcanzó el límite máximo de 1500 futbolistas en el sistema.';
    END IF;
END $$
DELIMITER ;



/*
Trigger para la carga de Puntuaciones

Controla las notas que ingresa el administrador: la nota debe estar entre 1 y 10, y un jugador no puede tener dos puntuaciones en la misma fecha.

*/

DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarPuntuacion_Insert $$
CREATE TRIGGER TRG_ValidarPuntuacion_Insert
BEFORE INSERT ON Puntuacion
FOR EACH ROW
BEGIN
    DECLARE existe INT;

    -- Validar que la nota del jugador esté entre 1 y 10
    IF NEW.puntuacion < 1.0 OR NEW.puntuacion > 10.0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: La puntuación debe ser un decimal entre 1 y 10.';
    END IF;

    -- 3. Validar que no tenga doble nota en la misma fecha
    SELECT COUNT(*) INTO existe FROM Puntuacion 
    WHERE idFutbolista = NEW.idFutbolista AND cantFech = NEW.cantFech;
    
    IF existe > 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: El futbolista ya tiene una puntuación registrada para esta fecha.';
    END IF;
END $$
DELIMITER ;




/*
Trigger para limitar la cantidad de Usuarios

Garantiza que la tabla de registros no exceda el límite de 2000 usuarios permitidos en el sistema.
*/


DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarUsuario_Insert $$
CREATE TRIGGER TRG_ValidarUsuario_Insert
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    DECLARE totalUsuarios INT;

    SELECT COUNT(*) INTO totalUsuarios FROM Usuario;
    
    IF totalUsuarios >= 2000 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: El sistema no admite más de 2000 usuarios.';
    END IF;
END $$
DELIMITER ;



/*
Trigger para la conformación de Plantillas

Como las plantillas en "El Gran DT" suelen tener exactamente 15 jugadores, este trigger se asegura de que al insertar datos en la tabla intermedia Futbolista_Plantilla, no se puedan asociar más de 15 futbolistas a un mismo equipo de usuario.
SQL
*/

DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarCapacidadPlantilla_Insert $$
CREATE TRIGGER TRG_ValidarCapacidadPlantilla_Insert
BEFORE INSERT ON Futbolista_Plantilla
FOR EACH ROW
BEGIN
    DECLARE totalEnPlantilla INT;
    DECLARE maxPermitido TINYINT;

    -- 1. Buscar el límite específico configurado para esa plantilla
    SELECT cantMaxFutbolista INTO maxPermitido 
    FROM Plantilla 
    WHERE idPlantilla = NEW.idPlantilla;

    -- 2. Contar cuántos jugadores ya están asociados
    SELECT COUNT(*) INTO totalEnPlantilla 
    FROM Futbolista_Plantilla 
    WHERE idPlantilla = NEW.idPlantilla;

    -- 3. Validar si supera el máximo
    IF totalEnPlantilla >= maxPermitido THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Se alcanzó la cantidad máxima de futbolistas permitida para esta plantilla.';
    END IF;
END $$
DELIMITER ;

