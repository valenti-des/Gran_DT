USE bd_gran_dt;

INSERT INTO Tipo_Futbolista (idTipoFutbolista, tipoFutbolista) VALUES
(1, 'Arquero'),
(2, 'Defensor'),
(3, 'Mediocampista'),
(4, 'Delantero');

INSERT INTO Equipo (idEquipo, nombre) VALUES
(1, 'Deportivo Horizonte'),
(2, 'Club Puerto Norte');

INSERT INTO Futbolista
    (idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
VALUES
(1, 'Mateo', 'Arce', 'Mati', '1998-04-12', 12500000.00, 1, 1),
(2, 'Luca', 'Ferreyra', 'Lucho', '1997-09-03', 18750000.00, 1, 2),
(3, 'Bruno', 'Soria', NULL, '2000-01-21', 22100000.00, 1, 3),
(4, 'Nicolas', 'Vega', 'Nico', '1999-07-18', 29999999.99, 2, 4),
(5, 'Tomas', 'Roldan', NULL, '1996-11-30', 9900000.00, 2, 1),
(6, 'Alan', 'Paz', 'Al', '2001-02-14', 15100000.00, 2, 2);

INSERT INTO Usuario
    (idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin)
VALUES
(1, 'Usuario', 'Prueba', 'usuario1@example.test', '1995-05-10', SHA2('CLAVE_FICTICIA_USUARIO_1', 256), 0),
(2, 'Usuario', 'Demo', 'usuario2@example.test', '1992-08-22', SHA2('CLAVE_FICTICIA_USUARIO_2', 256), 0);

INSERT INTO Puntuacion (idPuntuacion, idFutbolista, puntuacion, cantFech, jornada) VALUES
(1, 1, 8.5, '2026-03-01', 1),
(2, 2, 7.0, '2026-03-01', 1),
(3, 3, 9.0, '2026-03-08', 2),
(4, 4, 6.5, '2026-03-08', 2);

INSERT INTO Plantilla (idPlantilla, nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario) VALUES
(1, 'Plantilla Horizonte', 100000000.00, 11, 1),
(2, 'Plantilla de prueba', 85000000.00, 11, 2);

INSERT INTO Futbolista_Plantilla
    (idFutbolistaPlantilla, futbolistaTitular, validacionP, validacionR, idPlantilla, idFutbolista)
VALUES
(1, 1, 1, 1, 1, 1),
(2, 0, 1, 1, 1, 2),
(3, 1, 1, 1, 1, 3),
(4, 0, 1, 1, 2, 4),
(5, 1, 1, 1, 2, 5);
