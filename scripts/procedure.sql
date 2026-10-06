USE bd5_gran_dt;

-- -----------------------------------------------------
-- Procedure: AltaEquipo
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaEquipo $$
CREATE PROCEDURE AltaEquipo (
    OUT unidEquipo TINYINT UNSIGNED, 
    IN unnombre VARCHAR(45)
)
BEGIN
    INSERT INTO Equipo (nombre)
    VALUES (unnombre);
    
    SET unidEquipo = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AltaTipoFutbolista
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaTipoFutbolista $$
CREATE PROCEDURE AltaTipoFutbolista (
    OUT unidTipoFutbolista TINYINT UNSIGNED, 
    IN unTipoFutbolista VARCHAR(45)
)
BEGIN
    INSERT INTO Tipo_Futbolista (tipoFutbolista)
    VALUES (unTipoFutbolista);
    
    SET unidTipoFutbolista = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AltaFutbolista
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaFutbolista $$
CREATE PROCEDURE AltaFutbolista (
    OUT unidFutbolista SMALLINT UNSIGNED, 
    IN unnombre VARCHAR(45), 
    IN unapellido VARCHAR(45), 
    IN unapodo VARCHAR(45), 
    IN unfechaNac DATE, 
    IN uncotizacion DECIMAL(10,2), 
    IN unidEquipo TINYINT UNSIGNED, 
    IN unidTipoFutbolista TINYINT UNSIGNED
)
BEGIN
    INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
    VALUES (unnombre, unapellido, unapodo, unfechaNac, uncotizacion, unidEquipo, unidTipoFutbolista);
    
    SET unidFutbolista = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AltaUsuario
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaUsuario $$
CREATE PROCEDURE AltaUsuario (
    OUT unidUsuario SMALLINT UNSIGNED,
    IN unnombreUsuario VARCHAR(45),
    IN unnombre VARCHAR(45),
    IN unapellido VARCHAR(45),
    IN unemail VARCHAR(100),
    IN unfechaNac DATE,
    IN uncontrasena CHAR(64),
    IN unes_admin TINYINT(1)
)
BEGIN
    INSERT INTO Usuario (nombreUsuario, nombre, apellido, email, fechaNac, contrasena, es_admin)
    VALUES (unnombreUsuario, unnombre, unapellido, unemail, unfechaNac, uncontrasena, IFNULL(unes_admin, 0));
    
    SET unidUsuario = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AltaPlantilla
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPlantilla $$
CREATE PROCEDURE AltaPlantilla (
    OUT unidPlantilla INT UNSIGNED, 
    IN unnombreP VARCHAR(45), 
    IN uncantMaxMonto DECIMAL(10,2), 
    IN uncantMaxFutbolista TINYINT UNSIGNED, 
    IN unidUsuario SMALLINT UNSIGNED
)
BEGIN
    INSERT INTO Plantilla (nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario)
    VALUES (
        unnombreP, 
        IFNULL(uncantMaxMonto, 99999999.99), 
        IFNULL(uncantMaxFutbolista, 20), 
        unidUsuario
    );
    
    SET unidPlantilla = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AgregarFutbolistaAPlantilla
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AgregarFutbolistaAPlantilla $$
CREATE PROCEDURE AgregarFutbolistaAPlantilla (
    IN unidPlantilla INT UNSIGNED, 
    IN unidFutbolista SMALLINT UNSIGNED,
    IN unfutbolistaTitular TINYINT(1)
)
BEGIN
    INSERT INTO Futbolista_Plantilla (idPlantilla, idFutbolista, futbolistaTitular)
    VALUES (unidPlantilla, unidFutbolista, IFNULL(unfutbolistaTitular, 0));
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: AltaPuntuacion
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS AltaPuntuacion $$
CREATE PROCEDURE AltaPuntuacion (
    OUT unidPuntuacion INT UNSIGNED,
    IN unidFutbolista SMALLINT UNSIGNED,
    IN unpuntuacion DECIMAL(3,1),
    IN unnroFecha TINYINT UNSIGNED
)
BEGIN
    INSERT INTO Puntuacion (idFutbolista, puntuacion, nroFecha)
    VALUES (unidFutbolista, unpuntuacion, unnroFecha);
    
    SET unidPuntuacion = LAST_INSERT_ID();
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: LoginUsuario (soporta email o nombreUsuario)
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS LoginUsuario $$
CREATE PROCEDURE LoginUsuario (
    IN unIdentificador VARCHAR(100), -- Puede ser email o nombreUsuario
    IN uncontrasena CHAR(64)
)
BEGIN
    SELECT idUsuario, nombreUsuario, nombre, apellido, email, es_admin
    FROM Usuario
    WHERE (email = unIdentificador OR nombreUsuario = unIdentificador) 
      AND contrasena = uncontrasena;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: ObtenerPuntajePlantillaPorFecha
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS ObtenerPuntajePlantillaPorFecha $$
CREATE PROCEDURE ObtenerPuntajePlantillaPorFecha (
    IN unidPlantilla INT UNSIGNED,
    IN unnroFecha TINYINT UNSIGNED
)
BEGIN
    SELECT 
        p.idPlantilla,
        p.nombreP,
        unnroFecha AS nroFechaConsultada,
        IFNULL(SUM(pt.puntuacion), 0) AS puntajeTotal
    FROM Plantilla p
    INNER JOIN Futbolista_Plantilla fp ON p.idPlantilla = fp.idPlantilla
    INNER JOIN Futbolista f ON fp.idFutbolista = f.idFutbolista
    INNER JOIN Puntuacion pt ON f.idFutbolista = pt.idFutbolista
    WHERE p.idPlantilla = unidPlantilla
      AND fp.futbolistaTitular = 1
      AND pt.nroFecha = unnroFecha
    GROUP BY p.idPlantilla, p.nombreP;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: BuscarPlantillaPorNombreUsuario
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS BuscarPlantillaPorNombreUsuario $$
CREATE PROCEDURE BuscarPlantillaPorNombreUsuario (
    IN unnombreUsuario VARCHAR(45)
)
BEGIN
    SELECT 
        p.idPlantilla,
        p.nombreP,
        u.nombreUsuario,
        f.idFutbolista,
        f.nombre AS nombreFutbolista,
        f.apellido AS apellidoFutbolista,
        tf.tipoFutbolista,
        f.cotizacion,
        fp.futbolistaTitular
    FROM Usuario u
    INNER JOIN Plantilla p ON u.idUsuario = p.idUsuario
    INNER JOIN Futbolista_Plantilla fp ON p.idPlantilla = fp.idPlantilla
    INNER JOIN Futbolista f ON fp.idFutbolista = f.idFutbolista
    INNER JOIN Tipo_Futbolista tf ON f.idTipoFutbolista = tf.idTipoFutbolista
    WHERE u.nombreUsuario = unnombreUsuario;
END $$
DELIMITER ;

-- -----------------------------------------------------
-- Procedure: CalcularPresupuestoConsumido
-- -----------------------------------------------------
DELIMITER $$
DROP PROCEDURE IF EXISTS CalcularPresupuestoConsumido $$
CREATE PROCEDURE CalcularPresupuestoConsumido (
    IN unidPlantilla INT UNSIGNED
)
BEGIN
    SELECT 
        p.idPlantilla,
        p.nombreP,
        p.cantMaxMonto AS presupuestoMaximo,
        IFNULL(SUM(f.cotizacion), 0) AS presupuestoConsumido,
        (p.cantMaxMonto - IFNULL(SUM(f.cotizacion), 0)) AS presupuestoRestante
    FROM Plantilla p
    LEFT JOIN Futbolista_Plantilla fp ON p.idPlantilla = fp.idPlantilla
    LEFT JOIN Futbolista f ON fp.idFutbolista = f.idFutbolista
    WHERE p.idPlantilla = unidPlantilla
    GROUP BY p.idPlantilla, p.nombreP, p.cantMaxMonto;
END $$
DELIMITER ;