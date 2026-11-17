USE bd5_gran_dt;

-- =====================================================
-- TRIGGER 1: Limitar a un maximo de 32 equipos
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS TRG_ValidarEquipo_Insert $$

CREATE TRIGGER TRG_ValidarEquipo_Insert
BEFORE INSERT ON Equipo
FOR EACH ROW
BEGIN
    DECLARE totalEquipos INT;

    SELECT COUNT(*) INTO totalEquipos
    FROM Equipo;

    IF totalEquipos >= 32 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Se alcanzo el limite maximo de 32 equipos permitidos.';
    END IF;

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 2: Limitar a 1500 futbolistas y validar cotizacion
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS TRG_ValidarFutbolista_Insert $$

CREATE TRIGGER TRG_ValidarFutbolista_Insert
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    DECLARE totalJugadores INT;

    IF NEW.cotizacion < 0 OR NEW.cotizacion > 99999999.99 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La cotizacion debe ser positiva y no puede superar los $99.999.999,99.';
    END IF;

    SELECT COUNT(*) INTO totalJugadores
    FROM Futbolista;

    IF totalJugadores >= 1500 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Se alcanzo el limite maximo de 1500 futbolistas en el sistema.';
    END IF;

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 3: Validar puntuaciones y fechas
-- Evitar puntuaciones duplicadas por futbolista y fecha
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS TRG_ValidarPuntuacion_Insert $$

CREATE TRIGGER TRG_ValidarPuntuacion_Insert
BEFORE INSERT ON Puntuacion
FOR EACH ROW
BEGIN
    DECLARE existe INT;

    IF NEW.puntuacion IS NOT NULL
       AND (NEW.puntuacion < 1.0 OR NEW.puntuacion > 10.0) THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La puntuacion debe ser un decimal entre 1 y 10.';

    END IF;

    IF NEW.nroFecha IS NOT NULL AND NEW.nroFecha >= 50 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: El numero de fecha debe ser menor a 50.';

    END IF;

    IF NEW.nroFecha IS NOT NULL THEN

        SELECT COUNT(*) INTO existe
        FROM Puntuacion
        WHERE idFutbolista = NEW.idFutbolista
          AND nroFecha = NEW.nroFecha;

        IF existe > 0 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Error: El futbolista ya tiene una puntuacion registrada para esta fecha.';
        END IF;

    END IF;

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 4: Limitar a un maximo de 2000 usuarios
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS TRG_ValidarUsuario_Insert $$

CREATE TRIGGER TRG_ValidarUsuario_Insert
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    DECLARE totalUsuarios INT;

    SELECT COUNT(*) INTO totalUsuarios
    FROM Usuario;

    IF totalUsuarios >= 2000 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: El sistema no admite mas de 2000 usuarios.';
    END IF;

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 5: Validar capacidad y presupuesto de plantilla
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS TRG_ValidarCapacidadPlantilla_Insert $$

CREATE TRIGGER TRG_ValidarCapacidadPlantilla_Insert
BEFORE INSERT ON Futbolista_Plantilla
FOR EACH ROW
BEGIN
    DECLARE totalEnPlantilla INT;
    DECLARE maxPermitido TINYINT UNSIGNED DEFAULT 20;
    DECLARE presupuestoDisponible DECIMAL(10,2);
    DECLARE costoFutbolista DECIMAL(10,2);

    SELECT presupuesto INTO presupuestoDisponible
    FROM Plantilla
    WHERE idPlantilla = NEW.idPlantilla
    FOR UPDATE;

    SELECT COUNT(*) INTO totalEnPlantilla
    FROM Futbolista_Plantilla
    WHERE idPlantilla = NEW.idPlantilla;

    SELECT cotizacion INTO costoFutbolista
    FROM Futbolista
    WHERE idFutbolista = NEW.idFutbolista;

    IF totalEnPlantilla >= maxPermitido THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La plantilla no puede superar los 20 futbolistas.';
    END IF;

    IF presupuestoDisponible < costoFutbolista THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Presupuesto insuficiente para incorporar este futbolista.';
    END IF;

END $$

DELIMITER ;

-- =====================================================
-- TRIGGER 6: Crear automaticamente una plantilla
-- cuando se registra un nuevo usuario
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS AftInsertUsuario $$

CREATE TRIGGER AftInsertUsuario
AFTER INSERT ON Usuario
FOR EACH ROW
BEGIN

    INSERT INTO Plantilla (
        nombreP,
        presupuesto,
        cantFutbolista,
        idUsuario
    )
    VALUES (
        CONCAT('Plantilla de ', NEW.nombre),
        90000000,
        0,
        NEW.idUsuario
    );

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 7: Descontar la cotizacion del presupuesto
-- y aumentar la cantidad de futbolistas
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS AfInsrtPlantillaJugadores $$

CREATE TRIGGER AfInsrtPlantillaJugadores
AFTER INSERT ON Futbolista_Plantilla
FOR EACH ROW
BEGIN

    UPDATE Plantilla
    SET presupuesto = presupuesto - (
            SELECT cotizacion
            FROM Futbolista
            WHERE idFutbolista = NEW.idFutbolista
        ),
        cantFutbolista = cantFutbolista + 1

    WHERE idPlantilla = NEW.idPlantilla;

END $$

DELIMITER ;


-- =====================================================
-- TRIGGER 8: Devolver el presupuesto al eliminar
-- un futbolista de una plantilla
-- =====================================================

DELIMITER $$

DROP TRIGGER IF EXISTS AfDeletePlantillaFutbolista $$

CREATE TRIGGER AfDeletePlantillaFutbolista
AFTER DELETE ON Futbolista_Plantilla
FOR EACH ROW
BEGIN

    UPDATE Plantilla
    SET presupuesto = presupuesto + (
            SELECT cotizacion
            FROM Futbolista
            WHERE idFutbolista = OLD.idFutbolista
        ),
        cantFutbolista = cantFutbolista - 1

    WHERE idPlantilla = OLD.idPlantilla;

END $$

DELIMITER ;




