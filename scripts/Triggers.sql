USE bd5_gran_dt;

-- -----------------------------------------------------
-- Trigger: Limitar a máximo 32 equipos
-- -----------------------------------------------------
DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarEquipo_Insert $$
CREATE TRIGGER TRG_ValidarEquipo_Insert
BEFORE INSERT ON Equipo
FOR EACH ROW
BEGIN
    DECLARE totalEquipos INT;

    SELECT COUNT(*) INTO totalEquipos FROM Equipo;
    
    IF totalEquipos >= 32 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Se alcanzó el límite máximo de 32 equipos permitidos.';
    END IF;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Trigger: Limitar a máximo 1500 futbolistas y validar cotización
-- -----------------------------------------------------
DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarFutbolista_Insert $$
CREATE TRIGGER TRG_ValidarFutbolista_Insert
BEFORE INSERT ON Futbolista
FOR EACH ROW
BEGIN
    DECLARE totalJugadores INT;

    IF NEW.cotizacion > 99999999.99 OR NEW.cotizacion < 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: La cotización debe ser positiva y no puede superar los $99.999.999,99.';
    END IF;

    SELECT COUNT(*) INTO totalJugadores FROM Futbolista;
    IF totalJugadores >= 1500 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Se alcanzó el límite máximo de 1500 futbolistas en el sistema.';
    END IF;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Trigger: Validar rango de notas (1 a 10) y evitar doble nota por fecha
-- -----------------------------------------------------
DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarPuntuacion_Insert $$
CREATE TRIGGER TRG_ValidarPuntuacion_Insert
BEFORE INSERT ON Puntuacion
FOR EACH ROW
BEGIN
    DECLARE existe INT;

    IF NEW.puntuacion < 1.0 OR NEW.puntuacion > 10.0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: La puntuación debe ser un decimal entre 1 y 10.';
    END IF;

    SELECT COUNT(*) INTO existe FROM Puntuacion 
    WHERE idFutbolista = NEW.idFutbolista AND nroFecha = NEW.nroFecha;
    
    IF existe > 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: El futbolista ya tiene una puntuación registrada para esta fecha.';
    END IF;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Trigger: Limitar a máximo 2000 usuarios
-- -----------------------------------------------------
DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarUsuario_Insert $$
CREATE TRIGGER TRG_ValidarUsuario_Insert
BEFORE INSERT ON Usuario
FOR EACH ROW
BEGIN
    DECLARE totalUsuarios INT;

    SELECT COUNT(*) INTO totalUsuarios FROM Usuario;
    
    IF totalUsuarios >= 2000 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: El sistema no admite más de 2000 usuarios.';
    END IF;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Trigger: Limitar cantidad máxima de jugadores en una plantilla
-- -----------------------------------------------------
DELIMITER $$
DROP TRIGGER IF EXISTS TRG_ValidarCapacidadPlantilla_Insert $$
CREATE TRIGGER TRG_ValidarCapacidadPlantilla_Insert
BEFORE INSERT ON Futbolista_Plantilla
FOR EACH ROW
BEGIN
    DECLARE totalEnPlantilla INT;
    DECLARE maxPermitido TINYINT;

    SELECT cantMaxFutbolista INTO maxPermitido 
    FROM Plantilla 
    WHERE idPlantilla = NEW.idPlantilla;

    SELECT COUNT(*) INTO totalEnPlantilla 
    FROM Futbolista_Plantilla 
    WHERE idPlantilla = NEW.idPlantilla;

    IF totalEnPlantilla >= maxPermitido THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Error: Se alcanzó la cantidad máxima de futbolistas permitida para esta plantilla.';
    END IF;
END $$
DELIMITER ;