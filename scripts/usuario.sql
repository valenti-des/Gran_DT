
USE bd5_gran_dt;

-- =====================================================
-- 1. USUARIO COMÚN
-- =====================================================

CREATE USER IF NOT EXISTS 'usuarios'@'%'
IDENTIFIED BY '123456789';

-- SELECT

GRANT SELECT(nombre, apellido, apodo, fechaNac, cotizacion)
ON bd5_gran_dt.Futbolista TO 'usuarios'@'%';

GRANT SELECT(tipoFutbolista)
ON bd5_gran_dt.Tipo_Futbolista TO 'usuarios'@'%';

GRANT SELECT
ON bd5_gran_dt.Usuario TO 'usuarios'@'%';

GRANT SELECT(nombre)
ON bd5_gran_dt.Equipo TO 'usuarios'@'%';

GRANT SELECT
ON bd5_gran_dt.Plantilla TO 'usuarios'@'%';

GRANT SELECT(puntuacion, nroFecha)
ON bd5_gran_dt.Puntuacion TO 'usuarios'@'%';

GRANT SELECT
ON bd5_gran_dt.Futbolista_Plantilla TO 'usuarios'@'%';


-- UPDATE

GRANT UPDATE(nombre, apellido, email, fechaNac, contrasena)
ON bd5_gran_dt.Usuario TO 'usuarios'@'%';

GRANT UPDATE(futbolistaTitular)
ON bd5_gran_dt.Futbolista_Plantilla TO 'usuarios'@'%';

GRANT UPDATE(nombreP)
ON bd5_gran_dt.Plantilla TO 'usuarios'@'%';


-- INSERT

GRANT INSERT
ON bd5_gran_dt.Usuario TO 'usuarios'@'%';

GRANT INSERT
ON bd5_gran_dt.Futbolista_Plantilla TO 'usuarios'@'%';

GRANT INSERT
ON bd5_gran_dt.Plantilla TO 'usuarios'@'%';


-- =====================================================
-- 2. ADMINISTRADOR
-- =====================================================

CREATE USER IF NOT EXISTS 'admin'@'%'
IDENTIFIED BY '12345678';


-- SELECT

GRANT SELECT
ON bd5_gran_dt.Futbolista TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Tipo_Futbolista TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Usuario TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Equipo TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Plantilla TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Puntuacion TO 'admin'@'%';

GRANT SELECT
ON bd5_gran_dt.Futbolista_Plantilla TO 'admin'@'%';


-- UPDATE

GRANT UPDATE(nombre, apellido, email, fechaNac, contrasena)
ON bd5_gran_dt.Usuario TO 'admin'@'%';

GRANT UPDATE(futbolistaTitular)
ON bd5_gran_dt.Futbolista_Plantilla TO 'admin'@'%';

GRANT UPDATE(nombreP)
ON bd5_gran_dt.Plantilla TO 'admin'@'%';

GRANT UPDATE(nombre, apellido, apodo, fechaNac, cotizacion)
ON bd5_gran_dt.Futbolista TO 'admin'@'%';

GRANT UPDATE(puntuacion, nroFecha)
ON bd5_gran_dt.Puntuacion TO 'admin'@'%';


-- INSERT

GRANT INSERT
ON bd5_gran_dt.Usuario TO 'admin'@'%';

GRANT INSERT
ON bd5_gran_dt.Futbolista_Plantilla TO 'admin'@'%';

GRANT INSERT
ON bd5_gran_dt.Plantilla TO 'admin'@'%';

GRANT INSERT
ON bd5_gran_dt.Futbolista TO 'admin'@'%';

GRANT INSERT
ON bd5_gran_dt.Puntuacion TO 'admin'@'%';



