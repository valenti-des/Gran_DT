DROP DATABASE IF EXISTS 5to_gran_dt;
CREATE DATABASE IF NOT EXISTS 5to_gran_dt;
USE 5to_gran_dt;

CREATE TABLE Equipo (
    idEquipo INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(45) NOT null unique
);



create table Jugador_Tipo(
    idTipo smallint unsigned primary key,
    tipo varchar(30) not  null
);



CREATE TABLE Jugador (
    idjugador INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    opodo varchar (20) null,
    fecha_de_nacimiento date not null,
    cotizacion int unsigned,
    idtipo smallint unsigned,
    idequipo int not null,
    CONSTRAINT fk_idTipo_jugador
        FOREIGN KEY (idTipo) REFERENCES Jugador_Tipo(idTipo),
     CONSTRAINT fk_equipo
        FOREIGN KEY (idEquipo) REFERENCES Equipo(idEquipo)
);




CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    fecha_de_nacimiento date not null,
    email varchar (45) not null unique,
    contracena char (64) not null
);


CREATE TABLE Plantilla (
    idplantilla INT AUTO_INCREMENT PRIMARY KEY,
    idusuario int not null,
    presupuesto int unsigned,
    cantidad_jugador smallint unsigned,
    CONSTRAINT fk_idUsuario_plantilla
        FOREIGN KEY (idUsuario) REFERENCES usuario(idUsuario)
    
);


CREATE TABLE PlantillaJugador (
    idPlantilla INT not null,
    idJugador int not null,
    Titular bool not null,
    CONSTRAINT fk_idPLantilla_jugador
        FOREIGN KEY (idPlantilla) REFERENCES Plantilla(idPlantilla)  ON DELETE Restrict ON UPDATE Restrict,
    
    CONSTRAINT fk_idJugador_plantilla
        FOREIGN KEY (idJugador) REFERENCES Jugador (idJugador)  ON DELETE Restrict ON UPDATE Restrict
);

