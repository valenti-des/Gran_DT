USE bd_gran_dt;

-- 1. Inserción de Tipos de Futbolista
INSERT INTO Tipo_Futbolista (tipoFutbolista) VALUES
('Arquero'),
('Defensor'),
('Mediocampista'),
('Delantero');

-- 2. Inserción de Equipos
INSERT INTO Equipo (nombre) VALUES
('River Plate'),             -- idEquipo: 1
('Boca Juniors'),            -- idEquipo: 2
('Independiente'),           -- idEquipo: 3
('Racing Club'),             -- idEquipo: 4
('Club Atlético Platense'); -- idEquipo: 5

-- 3. Inserción de Futbolistas
-- idTipoFutbolista: 1 = Arquero, 2 = Defensor, 3 = Mediocampista, 4 = Delantero
INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista) 
VALUES
-- ================= RIVER PLATE (idEquipo: 1) =================
('Franco', 'Armani', 'El Pulpo', '1986-10-16', 15000000.00, 1, 1),
('Jeremías', 'Ledesma', 'Conan', '1993-02-13', 12000000.00, 1, 1),
('Paulo', 'Díaz', 'Paulo', '1994-08-25', 25000000.00, 1, 2),
('Germán', 'Pezzella', 'El Bahiense', '1991-06-27', 20000000.00, 1, 2),
('Marcos', 'Acuña', 'El Huevo', '1991-10-28', 22000000.00, 1, 2),
('Fabricio', 'Bustos', 'El Tractor', '1996-04-28', 18000000.00, 1, 2),
('Enzo', 'Díaz', 'Enzo', '1995-09-24', 16000000.00, 1, 2),
('Enzo', 'Pérez', 'Enzo', '1986-02-22', 14000000.00, 1, 3),
('Rodrigo', 'Aliendro', 'Peti', '1991-02-16', 15000000.00, 1, 3),
('Manuel', 'Lanzini', 'Manu', '1993-02-15', 20000000.00, 1, 3),
('Ignacio', 'Fernández', 'Nacho', '1990-01-12', 18000000.00, 1, 3),
('Claudio', 'Echeverri', 'El Diablito', '2006-01-02', 30000000.00, 1, 3),
('Facundo', 'Colidio', 'Facu', '2000-01-04', 22000000.00, 1, 4),
('Miguel', 'Borja', 'El Colibrí', '1993-01-26', 28000000.00, 1, 4),
('Pablo', 'Solari', 'Pablito', '2001-03-22', 20000000.00, 1, 4),

-- ================= BOCA JUNIORS (idEquipo: 2) =================
('Sergio', 'Romero', 'Chiquito', '1987-02-22', 14000000.00, 2, 1),
('Leandro', 'Brey', 'Lea', '2002-09-21', 10000000.00, 2, 1),
('Marcos', 'Rojo', 'Marquitos', '1990-03-20', 18000000.00, 2, 2),
('Nicolás', 'Figal', 'Nico', '1994-04-03', 16000000.00, 2, 2),
('Luis', 'Advíncula', 'El Rayo', '1990-03-02', 17000000.00, 2, 2),
('Lautaro', 'Blanco', 'Lauti', '1999-02-19', 19000000.00, 2, 2),
('Aaron', 'Anselmino', 'Aaron', '2005-04-29', 25000000.00, 2, 2),
('Pol', 'Fernández', 'Pol', '1991-10-11', 15000000.00, 2, 3),
('Cristian', 'Medina', 'Maravilla', '2002-06-01', 26000000.00, 2, 3),
('Kevin', 'Zenón', 'Kevin', '2001-07-30', 25000000.00, 2, 3),
('Ignacio', 'Miramón', 'Nacho', '2003-06-12', 16000000.00, 2, 3),
('Edinson', 'Cavani', 'El Matador', '1987-02-14', 35000000.00, 2, 4),
('Miguel', 'Merentiel', 'La Bestia', '1996-02-24', 30000000.00, 2, 4),
('Exequiel', 'Zeballos', 'El Changuito', '2002-04-24', 20000000.00, 2, 4),
('Milton', 'Giménez', 'Milton', '1996-08-12', 18000000.00, 2, 4),

-- ================= INDEPENDIENTE (idEquipo: 3) =================
('Rodrigo', 'Rey', 'Rey', '1991-03-08', 16000000.00, 3, 1),
('Diego', 'Segovia', 'Diego', '2000-05-15', 8000000.00, 3, 1),
('Joaquín', 'Laso', 'Laso', '1990-07-04', 12000000.00, 3, 2),
('Kevin', 'Lomónaco', 'Kevin', '2002-01-08', 18000000.00, 3, 2),
('Federico', 'Vera', 'Fede', '1998-03-24', 14000000.00, 3, 2),
('Damián', 'Pérez', 'Damián', '1988-12-22', 10000000.00, 3, 2),
('Ivan', 'Marcone', 'El Capo', '1990-06-03', 15000000.00, 3, 3),
('Lucas', 'González', 'Saltita', '2000-06-03', 14000000.00, 3, 3),
('Felipe', 'Loyola', 'Pipe', '2000-11-09', 18000000.00, 3, 3),
('Federico', 'Mancuello', 'Mancu', '1989-03-26', 12000000.00, 3, 3),
('Gabriel', 'Ávalos', 'El Tanque', '1990-10-12', 20000000.00, 3, 4),
('Santiago', 'Hidalgo', 'Santi', '2005-02-17', 15000000.00, 3, 4),
('Alexis', 'Canelo', 'Canelo', '1992-02-03', 14000000.00, 3, 4),
('Matías', 'Giménez', 'Mati', '1999-03-06', 16000000.00, 3, 4),

-- ================= RACING CLUB (idEquipo: 4) =================
('Gabriel', 'Arias', 'Gabi', '1987-09-13', 15000000.00, 4, 1),
('Facundo', 'Cambeses', 'Facu', '1997-04-09', 11000000.00, 4, 1),
('Leonardo', 'Sigali', 'El Oso', '1987-05-29', 12000000.00, 4, 2),
('Agustín', 'García Basso', 'Agus', '1992-04-25', 17000000.00, 4, 2),
('Marco', 'Di Cesare', 'Marco', '2002-01-30', 20000000.00, 4, 2),
('Gastón', 'Martirena', 'Gasti', '2000-01-05', 18000000.00, 4, 2),
('Gabriel', 'Rojas', 'Gabi', '1997-06-22', 16000000.00, 4, 2),
('Bruno', 'Zuculini', 'Zucu', '1993-04-02', 15000000.00, 4, 3),
('Juan Fernando', 'Quintero', 'Juanfer', '1993-01-18', 25000000.00, 4, 3),
('Agustín', 'Almendra', 'Agus', '2000-02-11', 18000000.00, 4, 3),
('Santiago', 'Sosa', 'Santi', '1999-05-03', 22000000.00, 4, 3),
('Adrian', 'Martínez', 'Maravilla', '1992-07-07', 28000000.00, 4, 4),
('Roger', 'Martínez', 'Roger', '1994-06-23', 24000000.00, 4, 4),
('Luciano', 'Vietto', 'Lucho', '1993-12-05', 20000000.00, 4, 4),
('Johan', 'Carbonero', 'Johan', '1999-07-20', 19000000.00, 4, 4),

-- ================= PLATENSE (idEquipo: 5) =================
('Juan Pablo', 'Cozzani', 'Juampi', '1998-05-09', 12000000.00, 5, 1),
('Joaquín', 'Blázquez', 'Joaco', '2001-01-28', 9000000.00, 5, 1),
('Gastón', 'Suso', 'Gaston', '1991-03-12', 13000000.00, 5, 2),
('Ignacio', me='Vázquez', apodo='Nacho', fechaNac='1997-06-15', cotizacion=14000000.00, 5, 2),
('Sasha', 'Marcich', 'Sasha', '1998-05-29', 12000000.00, 5, 2),
('Bautista', 'Barros Schelotto', 'Bauti', '2000-01-13', 13000000.00, 5, 2),
('Carlos', 'Villalba', 'Carlitos', '1998-07-19', 14000000.00, 5, 3),
('Fernando', 'Juárez', 'Ajito', '1998-08-23', 12000000.00, 5, 3),
('Leonel', 'Picco', 'Leo', '1998-10-22', 15000000.00, 5, 3),
('Franco', 'Mineria', 'Franco', '2002-11-04', 11000000.00, 5, 3),
('Mateo', 'Pellegrino', 'Flaco', '2001-10-22', 18000000.00, 5, 4),
('Ronaldo', 'Martínez', 'Roni', '1996-04-25', 15000000.00, 5, 4),
('Augusto', 'Lotti', 'Lotti', '1996-06-10', 16000000.00, 5, 4),
('Ignacio', 'Schor', 'El Pájaro', '2000-08-04', 14000000.00, 5, 4);

-- 4. Inserción de Usuarios
INSERT INTO Usuario (
    nombre,
    apellido,
    email,
    fechaNac,
    contraseña,
    es_admin
) VALUES
('Valengiu', 'Sigma', 'valengiu0708@gmail.com', '2008-08-07', '1234', 0),
('Admin', 'Sistema', 'admin@gran_dt.com', '1990-01-01', 'admin123', 1);

-- 5. Inserción de Puntuaciones
INSERT INTO Puntuacion (idFutbolista, puntuacion, cantFech) VALUES
(1, 8.4, 1),
(8, 7.8, 2),
(13, 9.1, 3),
(27, 10.0, 4),
(54, 8.5, 2);

-- 6. Inserción de Plantillas
INSERT INTO Plantilla (
    nombreP,
    cantMaxMonto,
    cantMaxFutbolista,
    idUsuario,
    idPuntuacion
) VALUES
('Plantilla Inicial', 500000000.00, 11, 1, 1),
('Equipo de Prueba', 350000000.00, 11, 2, 4);

-- 7. Inserción en Futbolista_Plantilla
INSERT INTO Futbolista_Plantilla (
    futbolistaTitular, futbolistaSuplente, validacionP, validacionR, idPlantilla, idFutbolista
) VALUES
(1, 0, 1, 0, 1, 1),   -- Armani en Plantilla 1
(0, 1, 0, 1, 1, 8),   -- Enzo Pérez en Plantilla 1
(1, 0, 1, 0, 1, 13),  -- Colidio en Plantilla 1
(0, 1, 0, 1, 1, 27),  -- Cavani en Plantilla 1
(1, 0, 1, 0, 2, 54),  -- Mateo Pellegrino (Platense) en Plantilla 2
(1, 0, 0, 0, 2, 8);   -- Enzo Pérez en Plantilla 2