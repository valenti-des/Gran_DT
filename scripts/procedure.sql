USE bd5_gran_dt;

-- =====================================================
-- PROCEDURE 1: AltaEquipo
-- =====================================================

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


-- =====================================================
-- PROCEDURE 2: AltaTipoFutbolista
-- =====================================================

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


-- =====================================================
-- PROCEDURE 3: AltaFutbolista
-- =====================================================

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
    INSERT INTO Futbolista (
        nombre,
        apellido,
        apodo,
        fechaNac,
        cotizacion,
        idEquipo,
        idTipoFutbolista
    )
    VALUES (
        unnombre,
        unapellido,
        unapodo,
        unfechaNac,
        IFNULL(uncotizacion, 0.00),
        unidEquipo,
        unidTipoFutbolista
    );

    SET unidFutbolista = LAST_INSERT_ID();
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 4: AltaUsuario
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS AltaUsuario $$

CREATE PROCEDURE AltaUsuario (
    OUT unidUsuario SMALLINT UNSIGNED,
    IN unnombre VARCHAR(45),
    IN unapellido VARCHAR(45),
    IN unemail VARCHAR(100),
    IN unfechaNac DATE,
    IN uncontrasena CHAR(64),
    IN unes_admin TINYINT(1)
)
BEGIN
    INSERT INTO Usuario (
        nombre,
        apellido,
        email,
        fechaNac,
        contrasena,
        es_admin
    )
    VALUES (
        unnombre,
        unapellido,
        unemail,
        unfechaNac,
        uncontrasena,
        IFNULL(unes_admin, 0)
    );

    SET unidUsuario = LAST_INSERT_ID();
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 5: AltaPlantilla
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS AltaPlantilla $$

CREATE PROCEDURE AltaPlantilla (
    OUT unidPlantilla INT UNSIGNED,
    IN unnombreP VARCHAR(45),
    IN unpresupuesto DECIMAL(10,2),
    IN unidUsuario SMALLINT UNSIGNED
)
BEGIN
    INSERT INTO Plantilla (
        nombreP,
        presupuesto,
        cantFutbolista,
        idUsuario
    )
    VALUES (
        unnombreP,
        IFNULL(unpresupuesto, 90000000.00),
        0,
        unidUsuario
    );

    SET unidPlantilla = LAST_INSERT_ID();
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 6: AgregarFutbolistaAPlantilla
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS AgregarFutbolistaAPlantilla $$

CREATE PROCEDURE AgregarFutbolistaAPlantilla (
    IN unidPlantilla INT UNSIGNED,
    IN unidFutbolista SMALLINT UNSIGNED,
    IN unfutbolistaTitular TINYINT(1)
)
BEGIN
    INSERT INTO Futbolista_Plantilla (
        idPlantilla,
        idFutbolista,
        futbolistaTitular
    )
    VALUES (
        unidPlantilla,
        unidFutbolista,
        IFNULL(unfutbolistaTitular, 0)
    );
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 7: AltaPuntuacion
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS AltaPuntuacion $$

CREATE PROCEDURE AltaPuntuacion (
    OUT unidPuntuacion INT UNSIGNED,
    IN unidFutbolista SMALLINT UNSIGNED,
    IN unpuntuacion DECIMAL(3,1),
    IN unnroFecha TINYINT UNSIGNED
)
BEGIN
    INSERT INTO Puntuacion (
        idFutbolista,
        puntuacion,
        nroFecha
    )
    VALUES (
        unidFutbolista,
        unpuntuacion,
        unnroFecha
    );

    SET unidPuntuacion = LAST_INSERT_ID();
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 8: LoginUsuario
-- Permite iniciar sesión con email
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS LoginUsuario $$

CREATE PROCEDURE LoginUsuario (
    IN unIdentificador VARCHAR(100),
    IN uncontrasena CHAR(64)
)
BEGIN
    SELECT
        idUsuario,
        nombre,
        apellido,
        email,
        es_admin
    FROM Usuario
    WHERE email = unIdentificador
      AND contrasena = uncontrasena;
END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 9: ObtenerPuntajePlantillaPorFecha
-- Obtiene el puntaje total de los titulares
-- de una plantilla para una fecha determinada
-- =====================================================

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
        IFNULL(SUM(pu.puntuacion), 0) AS puntajeTotal

    FROM Plantilla p

    LEFT JOIN Futbolista_Plantilla fp
        ON p.idPlantilla = fp.idPlantilla
        AND fp.futbolistaTitular = 1

    LEFT JOIN Puntuacion pu
        ON fp.idFutbolista = pu.idFutbolista
        AND pu.nroFecha = unnroFecha

    WHERE p.idPlantilla = unidPlantilla

    GROUP BY
        p.idPlantilla,
        p.nombreP;

END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 10: BuscarPlantillaPorNombre
-- Busca las plantillas de un usuario y sus futbolistas
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS BuscarPlantillaPorNombre $$

CREATE PROCEDURE BuscarPlantillaPorNombre (
    IN unnombre VARCHAR(45)
)
BEGIN
    SELECT
        p.idPlantilla,
        p.nombreP,
        u.nombre,
        f.idFutbolista,
        f.nombre AS nombreFutbolista,
        f.apellido AS apellidoFutbolista,
        tf.tipoFutbolista,
        f.cotizacion,
        fp.futbolistaTitular

    FROM Usuario u

    INNER JOIN Plantilla p
        ON u.idUsuario = p.idUsuario

    LEFT JOIN Futbolista_Plantilla fp
        ON p.idPlantilla = fp.idPlantilla

    LEFT JOIN Futbolista f
        ON fp.idFutbolista = f.idFutbolista

    LEFT JOIN Tipo_Futbolista tf
        ON f.idTipoFutbolista = tf.idTipoFutbolista

    WHERE u.nombre = unnombre;

END $$

DELIMITER ;


-- =====================================================
-- PROCEDURE 11: CalcularPresupuestoConsumido
-- Calcula el presupuesto consumido y el disponible
-- =====================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS CalcularPresupuestoConsumido $$

CREATE PROCEDURE CalcularPresupuestoConsumido (
    IN unidPlantilla INT UNSIGNED
)
BEGIN
    SELECT
        p.idPlantilla,
        p.nombreP,

        IFNULL(SUM(f.cotizacion), 0) AS presupuestoConsumido,

        p.presupuesto AS presupuestoRestante,

        (
            p.presupuesto + IFNULL(SUM(f.cotizacion), 0)
        ) AS presupuestoInicialEstimado

    FROM Plantilla p

    LEFT JOIN Futbolista_Plantilla fp
        ON p.idPlantilla = fp.idPlantilla

    LEFT JOIN Futbolista f
        ON fp.idFutbolista = f.idFutbolista

    WHERE p.idPlantilla = unidPlantilla

    GROUP BY
        p.idPlantilla,
        p.nombreP,
        p.presupuesto;

END $$

DELIMITER ;
