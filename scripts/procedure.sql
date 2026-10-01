DELIMITER $$
USE bd_gran_dt $$
DELIMITER $$
drop procedure if exists AltaUsuario $$
create procedure AltaUsuario ( out unidUsuario smallint, unnombre varchar(45), unapellido varchar(45) , unemail varchar(100), unfechaNac date, uncontraseña char(64), unes_admin tinyint )
begin
	INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin )
	Values (unnombre, unapellido , unemail, unfechaNac, uncontraseña, unes_admin);
	SET unidUsuario = LAST_INSERT_ID();
	
end $$
-- call AltaUsuario ( 5, "fernando", "alcala", "fernando@gmail.com", '2008-08-17', "messi123", false);  
DELIMITER $$
drop procedure if exists AltaEquipo $$
create procedure AltaEquipo (out unidEquipo tinyint unsigned, unnombre varchar(45) )
begin
	INSERT INTO Equipo ( nombre )
	Values ( unnombre);
	
		SET unidEquipo = LAST_INSERT_ID();
		
end $$
-- call AltaEquipo (4, "river");  "Para llamar al procedimiento"
DELIMITER $$
drop procedure if exists AltaFutbolista $$
create procedure AltaFutbolista (unidFutbolista smallint unsigned, unnombre varchar(45), unapellido varchar(45), unapodo varchar(45), unfechaNac date, uncotizacion decimal (11,2), unidEquipo tinyint unsigned, unidTipoFutbolista tinyint unsigned )
begin
	INSERT INTO futbolista (idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista )
	Values (unidFutbolista, unnombre, unapellido, unapodo, unfechaNac, uncotizacion, unidEquipo, unidTipoFutbolista);
	
end $$
-- call AltaFutbolista (10, "lionel", "messi", "pulga", '1999-05-05', 15.5, 4, null);
DELIMITER $$
drop procedure if exists AltaTipoFutbolista $$
create procedure AltaTipoFutbolista (out unidTipoFutbolista tinyint unsigned, unTipoFutbolista varchar(45) )
begin
	INSERT INTO tipo_futbolista (TipoFutbolista )
	Values (unTipoFutbolista);
	
	SET unidTipoFutbolista = LAST_INSERT_ID();
	
end $$
-- call AltaTipoFutbolista (4, "Delanteros"); "Para llamar al procedimiento"
-- delete from tipo_futbolista where (idTipoFutbolista = 4 and TipoFutbolista = "Delantero"); "Para Eliminar Datos"
DELIMITER $$
drop procedure if exists AltaPlantilla $$
create procedure AltaPlantilla (out unidPlantilla tinyint unsigned, unnombreP varchar(45), uncantMaxMonto Decimal(11,2), uncantMaxFutbolista tinyint, unidUsuario smallint, unidPuntuacion int unsigned )
begin
	INSERT INTO tipo_futbolista (nombreP , cantMaxMonto , cantMaxFutbolista , idUsuario , idPuntuacion )
	Values ( unnombreP , uncantMaxMonto , uncantMaxFutbolista , unidUsuario , unidPuntuacion );
	
	SET unidPlantilla = LAST_INSERT_ID();
	
end $$
-- call AltaPlantilla(5,"Plantilla1", 24, 12, 8,)

DELIMITER $$
drop procedure if exists AltaFutbolistaP $$
create procedure AltaFutbolistaP (out unidFutbolistaPlantilla tinyint unsigned, unfutbolistaTitular tinyint, unfutbolistaSuplente tinyint, unvalidacionP tinyint, unvalidacionR tinyint, unidPlantilla tinyint unsigned , unidFutbolista smallint unsigned)
begin
	INSERT INTO Futbolista_Plantilla (futbolistaTitular, futbolistaSuplente, validacionP, validacionR, idPlantilla , idFutbolista )
	Values (unfutbolistaTitular, unfutbolistaSuplente, unvalidacionP, unvalidacionR, unidPlantilla, unidFutbolista);
	
	SET unidFutbolistaPlantilla = LAST_INSERT_ID(); 
	
end $$ 

-- call AltaFutbolistaP ( )


