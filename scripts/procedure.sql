DELIMITER $$
USE bd_gran_dt $$
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario (
    OUT unidUsuario SMALLINT, 
    unnombre VARCHAR(45), 
    unapellido VARCHAR(45), 
    unemail VARCHAR(100), 
    unfechaNac DATE, 
    uncontraseña CHAR(64), 
    unes_admin TINYINT
)
BEGIN
    INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin)
    VALUES (unnombre, unapellido, unemail, unfechaNac, uncontraseña, unes_admin);
    
    SET unidUsuario = LAST_INSERT_ID();
END $$
DELIMITER ;



DELIMITER $$
DROP PROCEDURE IF EXISTS AltaEquipo $$
CREATE PROCEDURE AltaEquipo (
    OUT unidEquipo TINYINT UNSIGNED, 
    unnombre VARCHAR(45)
)
BEGIN
    INSERT INTO Equipo (nombre)
    VALUES (unnombre);
    
    SET unidEquipo = LAST_INSERT_ID();
END $$
DELIMITER ;



DELIMITER $$
DROP PROCEDURE IF EXISTS AltaTipoFutbolista $$
CREATE PROCEDURE AltaTipoFutbolista (
    OUT unidTipoFutbolista TINYINT UNSIGNED, 
    unTipoFutbolista VARCHAR(45)
)
BEGIN
    INSERT INTO Tipo_Futbolista (tipoFutbolista)
    VALUES (unTipoFutbolista);
    
    SET unidTipoFutbolista = LAST_INSERT_ID();
END $$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaFutbolista $$
CREATE PROCEDURE AltaFutbolista (
    OUT unidFutbolista SMALLINT UNSIGNED, 
    unnombre VARCHAR(45), 
    unapellido VARCHAR(45), 
    unapodo VARCHAR(45), 
    unfechaNac DATE, 
    uncotizacion DECIMAL(11,2), 
    unidEquipo TINYINT UNSIGNED, 
    unidTipoFutbolista TINYINT UNSIGNED
)
BEGIN
    INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
    VALUES (unnombre, unapellido, unapodo, unfechaNac, uncotizacion, unidEquipo, unidTipoFutbolista);
    
    SET unidFutbolista = LAST_INSERT_ID();
END $$
DELIMITER ;



DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPlantilla $$
CREATE PROCEDURE AltaPlantilla (
    OUT unidPlantilla TINYINT UNSIGNED, 
    unnombreP VARCHAR(45), 
    uncantMaxMonto DECIMAL(11,2), 
    uncantMaxFutbolista TINYINT, 
    unidUsuario SMALLINT
)
BEGIN
    INSERT INTO Plantilla (nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario)
    VALUES (unnombreP, uncantMaxMonto, uncantMaxFutbolista, unidUsuario);
    
    SET unidPlantilla = LAST_INSERT_ID();
END $$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaFutbolistaP $$
CREATE PROCEDURE AltaFutbolistaP (
    OUT unidFutbolistaPlantilla TINYINT UNSIGNED, 
    unfutbolistaTitular TINYINT, 
    unidPlantilla TINYINT UNSIGNED, 
    unidFutbolista SMALLINT UNSIGNED
)
BEGIN
    INSERT INTO Futbolista_Plantilla (futbolistaTitular, idPlantilla, idFutbolista)
    VALUES (unfutbolistaTitular, unidPlantilla, unidFutbolista);
    
    SET unidFutbolistaPlantilla = LAST_INSERT_ID();
END $$
DELIMITER ;


DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPuntuacion $$
CREATE PROCEDURE AltaPuntuacion (
    OUT unidPuntuacion INT UNSIGNED,
    unidFutbolista SMALLINT UNSIGNED,
    unpuntuacion FLOAT,
    uncantFech SMALLINT
)
BEGIN
    INSERT INTO Puntuacion (idFutbolista, puntuacion, cantFech)
    VALUES (unidFutbolista, unpuntuacion, uncantFech);
    
    SET unidPuntuacion = LAST_INSERT_ID();
END $$
DELIMITER ;


/*
Propósito: Validar el acceso consultando la tabla Usuario.

Operación: Requiere recibir parámetros desde C# (unemail, uncontraseña) y realizar una lectura (SELECT) para devolver los datos del usuario autenticado.
*/
DELIMITER $$
DROP PROCEDURE IF EXISTS LoginUsuario $$
CREATE PROCEDURE LoginUsuario (
    IN unemail VARCHAR(100),
    IN uncontraseña CHAR(64)
)
BEGIN
    SELECT idUsuario, nombre, apellido, email, es_admin
    FROM Usuario
    WHERE email = unemail AND contraseña = uncontraseña;
END $$
DELIMITER ;




/*
Propósito: Calcular dinámicamente el puntaje cuando la aplicación lo solicite (por ejemplo, al llamar a plantilla.PuntajeFecha(4)).

Operación: Recibe dos parámetros (unidPlantilla, unfecha), realiza un cálculo con SUM() y retorna el valor a C#.

Incompatibilidad con Trigger: Los Triggers no se pueden invocar a demanda pasándoles argumentos desde el código de la aplicación, ni retornan un resultado directo a la interfaz.
*/


DELIMITER $$
DROP PROCEDURE IF EXISTS ObtenerPuntajePlantillaPorFecha $$
CREATE PROCEDURE ObtenerPuntajePlantillaPorFecha (
    IN unidPlantilla TINYINT UNSIGNED,
    IN unfecha SMALLINT
)
BEGIN
    SELECT 
        p.idPlantilla,
        p.nombreP,
        unfecha AS fechaConsultada,
        IFNULL(SUM(pt.puntuacion), 0) AS puntajeTotal
    FROM Plantilla p
    INNER JOIN Futbolista_Plantilla fp ON p.idPlantilla = fp.idPlantilla
    INNER JOIN Futbolista f ON fp.idFutbolista = f.idFutbolista
    INNER JOIN Puntuacion pt ON f.idFutbolista = pt.idFutbolista
    WHERE p.idPlantilla = unidPlantilla
      AND fp.futbolistaTitular = 1
      AND pt.cantFech = unfecha;
END $$
DELIMITER ;