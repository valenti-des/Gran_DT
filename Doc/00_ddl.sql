CREATE DATABASE IF NOT EXISTS bd_gran_dt
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE bd_gran_dt;

CREATE TABLE IF NOT EXISTS Tipo_Futbolista (
    idTipoFutbolista TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
    tipoFutbolista VARCHAR(45) NOT NULL,
    PRIMARY KEY (idTipoFutbolista),
    UNIQUE KEY uq_TipoFutbolista_Tipo (tipoFutbolista)
);

CREATE TABLE IF NOT EXISTS Equipo (
    idEquipo TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    PRIMARY KEY (idEquipo),
    UNIQUE KEY uq_Equipo_Nombre (nombre)
);

CREATE TABLE IF NOT EXISTS Futbolista (
    idFutbolista SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    apodo VARCHAR(45),
    fechaNac DATE,
    cotizacion DECIMAL(10,2) NOT NULL,
    idEquipo TINYINT UNSIGNED,
    idTipoFutbolista TINYINT UNSIGNED,
    PRIMARY KEY (idFutbolista),
    CONSTRAINT ck_Futbolista_Cotizacion CHECK (cotizacion BETWEEN 0 AND 99999999.99),
    CONSTRAINT fk_Futbolista_Equipo FOREIGN KEY (idEquipo)
        REFERENCES Equipo (idEquipo),
    CONSTRAINT fk_Futbolista_Tipo FOREIGN KEY (idTipoFutbolista)
        REFERENCES Tipo_Futbolista (idTipoFutbolista)
);

CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario SMALLINT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    email VARCHAR(100) NOT NULL,
    fechaNac DATE,
    contraseña CHAR(64) NOT NULL,
    es_admin TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (idUsuario),
    UNIQUE KEY uq_Usuario_Email (email)
);

CREATE TABLE IF NOT EXISTS Puntuacion (
    idPuntuacion INT UNSIGNED NOT NULL AUTO_INCREMENT,
    idFutbolista SMALLINT UNSIGNED NOT NULL,
    puntuacion FLOAT NOT NULL,
    cantFech DATE NOT NULL,
    jornada TINYINT UNSIGNED NOT NULL DEFAULT 1,
    PRIMARY KEY (idFutbolista, cantFech),
    UNIQUE KEY uq_Puntuacion_Id (idPuntuacion),
    CONSTRAINT ck_Puntuacion_Jornada CHECK (jornada BETWEEN 1 AND 49),
    CONSTRAINT ck_Puntuacion_Valor CHECK (puntuacion BETWEEN 1 AND 10),
    CONSTRAINT fk_Puntuacion_Futbolista FOREIGN KEY (idFutbolista)
        REFERENCES Futbolista (idFutbolista)
);

CREATE TABLE IF NOT EXISTS Plantilla (
    idPlantilla TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombreP VARCHAR(45) NOT NULL,
    cantMaxMonto DECIMAL(11,2) NOT NULL,
    cantMaxFutbolista TINYINT UNSIGNED NOT NULL,
    idUsuario SMALLINT NOT NULL,
    PRIMARY KEY (idPlantilla),
    CONSTRAINT ck_Plantilla_Monto CHECK (cantMaxMonto >= 0),
    CONSTRAINT ck_Plantilla_Cantidad CHECK (cantMaxFutbolista BETWEEN 1 AND 15),
    CONSTRAINT fk_Plantilla_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario)
);

CREATE TABLE IF NOT EXISTS Futbolista_Plantilla (
    idFutbolistaPlantilla TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
    futbolistaTitular TINYINT(1) NOT NULL DEFAULT 0,
    validacionP TINYINT(1) NOT NULL DEFAULT 0,
    validacionR TINYINT(1) NOT NULL DEFAULT 0,
    idPlantilla TINYINT UNSIGNED NOT NULL,
    idFutbolista SMALLINT UNSIGNED NOT NULL,
    PRIMARY KEY (idFutbolistaPlantilla),
    UNIQUE KEY uq_Futbolista_Plantilla (idPlantilla, idFutbolista),
    CONSTRAINT ck_Futbolista_Plantilla_Titular CHECK (futbolistaTitular IN (0, 1)),
    CONSTRAINT fk_FP_Futbolista FOREIGN KEY (idFutbolista)
        REFERENCES Futbolista (idFutbolista),
    CONSTRAINT fk_FP_Plantilla FOREIGN KEY (idPlantilla)
        REFERENCES Plantilla (idPlantilla)
);
