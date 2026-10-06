DROP DATABASE IF EXISTS bd5_gran_dt;
CREATE DATABASE bd5_gran_dt; 
USE bd5_gran_dt;

-- -----------------------------------------------------
-- Tabla: Tipo_Futbolista
-- -----------------------------------------------------
CREATE TABLE Tipo_Futbolista (
    idTipoFutbolista TINYINT UNSIGNED AUTO_INCREMENT,
    tipoFutbolista VARCHAR(45) NOT NULL,
    CONSTRAINT pk_TipoFutbolista PRIMARY KEY (idTipoFutbolista),
    CONSTRAINT uq_TipoFutbolista_Tipo UNIQUE (tipoFutbolista)
);

-- -----------------------------------------------------
-- Tabla: Equipo (Máx 32 equipos)
-- -----------------------------------------------------
CREATE TABLE Equipo (
    idEquipo TINYINT UNSIGNED AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    CONSTRAINT pk_Equipo PRIMARY KEY (idEquipo),
    CONSTRAINT uq_Equipo_Nombre UNIQUE (nombre)
);

-- -----------------------------------------------------
-- Tabla: Futbolista (Máx ~1500 jugadores)
-- -----------------------------------------------------
CREATE TABLE Futbolista (
    idFutbolista SMALLINT UNSIGNED AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    apodo VARCHAR(45) NULL,
    fechaNac DATE NULL,
    cotizacion DECIMAL(10,2) NOT NULL DEFAULT 0.00, -- Soporta hasta $99.999.999,99
    idEquipo TINYINT UNSIGNED NULL,
    idTipoFutbolista TINYINT UNSIGNED NOT NULL,
    CONSTRAINT pk_Futbolista PRIMARY KEY (idFutbolista),
    CONSTRAINT fk_Futbolista_Equipo FOREIGN KEY (idEquipo) 
        REFERENCES Equipo (idEquipo) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT fk_Futbolista_Tipo FOREIGN KEY (idTipoFutbolista) 
        REFERENCES Tipo_Futbolista (idTipoFutbolista) ON DELETE RESTRICT ON UPDATE CASCADE
);

-- -----------------------------------------------------
-- Tabla: Usuario (Soporta hasta 2000 usuarios)
-- -----------------------------------------------------
CREATE TABLE Usuario (
    idUsuario SMALLINT UNSIGNED AUTO_INCREMENT,
    nombreUsuario VARCHAR(45) NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    email VARCHAR(100) NOT NULL,
    fechaNac DATE NULL,
    contrasena CHAR(64) NOT NULL, -- Longitud fija de 64 caracteres (SHA-256)
    es_admin TINYINT(1) NOT NULL DEFAULT 0,
    CONSTRAINT pk_Usuario PRIMARY KEY (idUsuario),
    CONSTRAINT uq_Usuario_NombreUsuario UNIQUE (nombreUsuario),
    CONSTRAINT uq_Usuario_Email UNIQUE (email)
);

-- -----------------------------------------------------
-- Tabla: Puntuacion
-- -----------------------------------------------------
CREATE TABLE Puntuacion (
    idPuntuacion INT UNSIGNED AUTO_INCREMENT,
    idFutbolista SMALLINT UNSIGNED NOT NULL,
    puntuacion DECIMAL(3,1) NOT NULL, -- Notas del 1 al 10 (ej. 7.5)
    nroFecha TINYINT UNSIGNED NOT NULL, -- Número entero menor a 50
    CONSTRAINT pk_Puntuacion PRIMARY KEY (idPuntuacion),
    CONSTRAINT fk_Puntuacion_Futbolista FOREIGN KEY (idFutbolista) 
        REFERENCES Futbolista (idFutbolista) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT uq_Puntuacion_Futbolista_Fecha UNIQUE (idFutbolista, nroFecha)
);

-- -----------------------------------------------------
-- Tabla: Plantilla
-- -----------------------------------------------------
CREATE TABLE Plantilla (
    idPlantilla INT UNSIGNED AUTO_INCREMENT,
    nombreP VARCHAR(45) NOT NULL,
    cantMaxMonto DECIMAL(10,2) NOT NULL DEFAULT 99999999.99,
    cantMaxFutbolista TINYINT UNSIGNED NOT NULL DEFAULT 20,
    idUsuario SMALLINT UNSIGNED NOT NULL,
    CONSTRAINT pk_Plantilla PRIMARY KEY (idPlantilla),
    CONSTRAINT fk_Plantilla_Usuario FOREIGN KEY (idUsuario) 
        REFERENCES Usuario (idUsuario) ON DELETE CASCADE ON UPDATE CASCADE
);

-- -----------------------------------------------------
-- Tabla: Futbolista_Plantilla (Relación Muchos a Muchos)
-- -----------------------------------------------------
CREATE TABLE Futbolista_Plantilla (
    idPlantilla INT UNSIGNED NOT NULL,
    idFutbolista SMALLINT UNSIGNED NOT NULL,
    futbolistaTitular TINYINT(1) NOT NULL DEFAULT 0, -- 1 = Titular, 0 = Suplente
    CONSTRAINT pk_Futbolista_Plantilla PRIMARY KEY (idPlantilla, idFutbolista),
    CONSTRAINT fk_FP_Plantilla FOREIGN KEY (idPlantilla) 
        REFERENCES Plantilla (idPlantilla) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_FP_Futbolista FOREIGN KEY (idFutbolista) 
        REFERENCES Futbolista (idFutbolista) ON DELETE RESTRICT ON UPDATE CASCADE
);