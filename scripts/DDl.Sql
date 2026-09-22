

DROP DATABASE IF EXISTS bd_gran_dt;
CREATE DATABASE bd_gran_dt;
USE bd_gran_dt;




CREATE TABLE Tipo_Futbolista (
	idTipoFutbolista TINYINT UNSIGNED AUTO_INCREMENT,
	tipoFutbolista VARCHAR(45) NOT NULL,
	CONSTRAINT pk_TipoFutbolista PRIMARY KEY (idTipoFutbolista ASC),
	CONSTRAINT uq_TipoFutbolista_Tipo UNIQUE (tipoFutbolista ASC)
);

CREATE TABLE Equipo (
	idEquipo TINYINT UNSIGNED AUTO_INCREMENT,
	nombre VARCHAR(45) NOT NULL,
	CONSTRAINT pk_Equipo PRIMARY KEY (idEquipo ASC),
	CONSTRAINT uq_Equipo_Nombre UNIQUE (nombre ASC)
);

CREATE TABLE Futbolista (
	idFutbolista SMALLINT UNSIGNED AUTO_INCREMENT,
	nombre VARCHAR(45) NOT NULL,
	apellido VARCHAR(45) NOT NULL,
	apodo VARCHAR(45),
	fechaNac DATE,
	cotizacion DECIMAL(11,2),
	idEquipo TINYINT UNSIGNED,
	idTipoFutbolista TINYINT UNSIGNED,
	CONSTRAINT pk_Futbolista PRIMARY KEY (idFutbolista ASC),
	CONSTRAINT fk_Futbolista_Equipo FOREIGN KEY (idEquipo) REFERENCES Equipo (idEquipo) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_Futbolista_Tipo FOREIGN KEY (idTipoFutbolista) REFERENCES Tipo_Futbolista (idTipoFutbolista) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Usuario (
	idUsuario Smallint AUTO_INCREMENT,
	nombre VARCHAR(45) NOT NULL,
	apellido VARCHAR(45) NOT NULL,
	email VARCHAR(100) NOT NULL,
	fechaNac DATE,
	contraseña CHAR(64) NOT NULL,
	es_admin TINYINT DEFAULT 0,
	CONSTRAINT pk_Usuario PRIMARY KEY (idUsuario ASC),
	CONSTRAINT uq_Usuario_Email UNIQUE (email ASC)
);

CREATE TABLE Puntuacion (
	idPuntuacion INT UNSIGNED AUTO_INCREMENT,
	idFutbolista SMALLINT UNSIGNED NOT NULL,
	puntuacion FLOAT,
	cantFech SMALLINT,
	CONSTRAINT pk_Puntuacion PRIMARY KEY (idPuntuacion ASC),
	CONSTRAINT fk_Puntuacion_Futbolista FOREIGN KEY (idFutbolista) REFERENCES Futbolista (idFutbolista) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Plantilla (
	idPlantilla TINYINT UNSIGNED AUTO_INCREMENT,
	nombreP VARCHAR(45) NOT NULL,
	cantMaxMonto DECIMAL(11,2),
	cantMaxFutbolista TINYINT,
	idUsuario SMALLINT,
	idPuntuacion INT UNSIGNED,
	CONSTRAINT pk_Plantilla PRIMARY KEY (idPlantilla ASC),
	CONSTRAINT fk_Plantilla_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario (idUsuario) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_Plantilla_Puntuacion FOREIGN KEY (idPuntuacion) REFERENCES Puntuacion (idPuntuacion) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Futbolista_Plantilla (
	idFutbolistaPlantilla TINYINT UNSIGNED AUTO_INCREMENT,
	futbolistaTitular TINYINT DEFAULT 0,
	futbolistaSuplente TINYINT DEFAULT 0,
	validacionP TINYINT DEFAULT 0,
	validacionR TINYINT DEFAULT 0,
	idPlantilla TINYINT UNSIGNED NOT NULL,
	idFutbolista SMALLINT UNSIGNED NOT NULL,
	CONSTRAINT pk_Futbolista_Plantilla PRIMARY KEY (idFutbolistaPlantilla ASC),
	CONSTRAINT fk_FP_Futbolista FOREIGN KEY (idFutbolista) REFERENCES Futbolista (idFutbolista) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_FP_Plantilla FOREIGN KEY (idPlantilla) REFERENCES Plantilla (idPlantilla) ON DELETE NO ACTION ON UPDATE NO ACTION
);
