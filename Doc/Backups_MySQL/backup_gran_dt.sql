-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: localhost    Database: bd5_gran_dt
-- ------------------------------------------------------
-- Server version	8.0.45

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `equipo`
--

DROP TABLE IF EXISTS `equipo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipo` (
  `idEquipo` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idEquipo`),
  UNIQUE KEY `uq_Equipo_Nombre` (`nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipo`
--

LOCK TABLES `equipo` WRITE;
/*!40000 ALTER TABLE `equipo` DISABLE KEYS */;
INSERT INTO `equipo` VALUES (2,'Boca Juniors'),(5,'Club Atlético Platense'),(6,'fernandoyvalen'),(3,'Independiente'),(4,'Racing Club'),(1,'River Plate');
/*!40000 ALTER TABLE `equipo` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `TRG_ValidarEquipo_Insert` BEFORE INSERT ON `equipo` FOR EACH ROW BEGIN
    DECLARE totalEquipos INT;

    SELECT COUNT(*) INTO totalEquipos
    FROM Equipo;

    IF totalEquipos >= 32 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Se alcanzo el limite maximo de 32 equipos permitidos.';
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `futbolista`
--

DROP TABLE IF EXISTS `futbolista`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `futbolista` (
  `idFutbolista` smallint unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `apellido` varchar(45) NOT NULL,
  `apodo` varchar(45) DEFAULT NULL,
  `fechaNac` date NOT NULL,
  `cotizacion` decimal(10,2) NOT NULL DEFAULT '0.00',
  `idEquipo` tinyint unsigned NOT NULL,
  `idTipoFutbolista` tinyint unsigned NOT NULL,
  PRIMARY KEY (`idFutbolista`),
  KEY `fk_Futbolista_Equipo` (`idEquipo`),
  KEY `fk_Futbolista_Tipo` (`idTipoFutbolista`),
  CONSTRAINT `fk_Futbolista_Equipo` FOREIGN KEY (`idEquipo`) REFERENCES `equipo` (`idEquipo`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_Futbolista_Tipo` FOREIGN KEY (`idTipoFutbolista`) REFERENCES `tipo_futbolista` (`idTipoFutbolista`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `futbolista`
--

LOCK TABLES `futbolista` WRITE;
/*!40000 ALTER TABLE `futbolista` DISABLE KEYS */;
INSERT INTO `futbolista` VALUES (1,'Franco','Armani','El Pulpo','1986-10-16',15000000.00,1,1),(2,'Jeremías','Ledesma','Conan','1993-02-13',12000000.00,1,1),(3,'Paulo','Díaz','Paulo','1994-08-25',25000000.00,1,2),(4,'Germán','Pezzella','El Bahiense','1991-06-27',20000000.00,1,2),(5,'Marcos','Acuña','El Huevo','1991-10-28',22000000.00,1,2),(6,'Fabricio','Bustos','El Tractor','1996-04-28',18000000.00,1,2),(7,'Enzo','Díaz','Enzo','1995-09-24',16000000.00,1,2),(8,'Enzo','Pérez','Enzo','1986-02-22',14000000.00,1,3),(9,'Rodrigo','Aliendro','Peti','1991-02-16',15000000.00,1,3),(10,'Manuel','Lanzini','Manu','1993-02-15',20000000.00,1,3),(11,'Ignacio','Fernández','Nacho','1990-01-12',18000000.00,1,3),(12,'Claudio','Echeverri','El Diablito','2006-01-02',30000000.00,1,3),(13,'Facundo','Colidio','Facu','2000-01-04',22000000.00,1,4),(14,'Miguel','Borja','El Colibrí','1993-01-26',28000000.00,1,4),(15,'Pablo','Solari','Pablito','2001-03-22',20000000.00,1,4),(16,'Sergio','Romero','Chiquito','1987-02-22',14000000.00,2,1),(17,'Leandro','Brey','Lea','2002-09-21',10000000.00,2,1),(18,'Marcos','Rojo','Marquitos','1990-03-20',18000000.00,2,2),(19,'Nicolás','Figal','Nico','1994-04-03',16000000.00,2,2),(20,'Luis','Advíncula','El Rayo','1990-03-02',17000000.00,2,2),(21,'Lautaro','Blanco','Lauti','1999-02-19',19000000.00,2,2),(22,'Aaron','Anselmino','Aaron','2005-04-29',25000000.00,2,2),(23,'Pol','Fernández','Pol','1991-10-11',15000000.00,2,3),(24,'Cristian','Medina','Maravilla','2002-06-01',26000000.00,2,3),(25,'Kevin','Zenón','Kevin','2001-07-30',25000000.00,2,3),(26,'Ignacio','Miramón','Nacho','2003-06-12',16000000.00,2,3),(27,'Edinson','Cavani','El Matador','1987-02-14',35000000.00,2,4),(28,'Miguel','Merentiel','La Bestia','1996-02-24',30000000.00,2,4),(29,'Exequiel','Zeballos','El Changuito','2002-04-24',20000000.00,2,4),(30,'Milton','Giménez','Milton','1996-08-12',18000000.00,2,4),(31,'Rodrigo','Rey','Rey','1991-03-08',16000000.00,3,1),(32,'Diego','Segovia','Diego','2000-05-15',8000000.00,3,1),(33,'Joaquín','Laso','Laso','1990-07-04',12000000.00,3,2),(34,'Kevin','Lomónaco','Kevin','2002-01-08',18000000.00,3,2),(35,'Federico','Vera','Fede','1998-03-24',14000000.00,3,2),(36,'Damián','Pérez','Damián','1988-12-22',10000000.00,3,2),(37,'Ivan','Marcone','El Capo','1990-06-03',15000000.00,3,3),(38,'Lucas','González','Saltita','2000-06-03',14000000.00,3,3),(39,'Felipe','Loyola','Pipe','2000-11-09',18000000.00,3,3),(40,'Federico','Mancuello','Mancu','1989-03-26',12000000.00,3,3),(41,'Gabriel','Ávalos','El Tanque','1990-10-12',20000000.00,3,4),(42,'Santiago','Hidalgo','Santi','2005-02-17',15000000.00,3,4),(43,'Alexis','Canelo','Canelo','1992-02-03',14000000.00,3,4),(44,'Matías','Giménez','Mati','1999-03-06',16000000.00,3,4),(45,'Gabriel','Arias','Gabi','1987-09-13',15000000.00,4,1),(46,'Facundo','Cambeses','Facu','1997-04-09',11000000.00,4,1),(47,'Leonardo','Sigali','El Oso','1987-05-29',12000000.00,4,2),(48,'Agustín','García Basso','Agus','1992-04-25',17000000.00,4,2),(49,'Marco','Di Cesare','Marco','2002-01-30',20000000.00,4,2),(50,'Gastón','Martirena','Gasti','2000-01-05',18000000.00,4,2),(51,'Gabriel','Rojas','Gabi','1997-06-22',16000000.00,4,2),(52,'Bruno','Zuculini','Zucu','1993-04-02',15000000.00,4,3),(53,'Juan Fernando','Quintero','Juanfer','1993-01-18',25000000.00,4,3),(54,'Agustín','Almendra','Agus','2000-02-11',18000000.00,4,3),(55,'Santiago','Sosa','Santi','1999-05-03',22000000.00,4,3),(56,'Adrian','Martínez','Maravilla','1992-07-07',28000000.00,4,4),(57,'Roger','Martínez','Roger','1994-06-23',24000000.00,4,4),(58,'Luciano','Vietto','Lucho','1993-12-05',20000000.00,4,4),(59,'Johan','Carbonero','Johan','1999-07-20',19000000.00,4,4),(60,'Juan Pablo','Cozzani','Juampi','1998-05-09',12000000.00,5,1),(61,'Joaquín','Blázquez','Joaco','2001-01-28',9000000.00,5,1),(62,'Gastón','Suso','Gaston','1991-03-12',13000000.00,5,2),(63,'Ignacio','Vázquez','Nacho','1997-06-15',14000000.00,5,2),(64,'Sasha','Marcich','Sasha','1998-05-29',12000000.00,5,2),(65,'Bautista','Barros Schelotto','Bauti','2000-01-13',13000000.00,5,2),(66,'Carlos','Villalba','Carlitos','1998-07-19',14000000.00,5,3),(67,'Fernando','Juárez','Ajito','1998-08-23',12000000.00,5,3),(68,'Leonel','Picco','Leo','1998-10-22',15000000.00,5,3),(69,'Franco','Mineria','Franco','2002-11-04',11000000.00,5,3),(70,'Mateo','Pellegrino','Flaco','2001-10-22',18000000.00,5,4),(71,'Ronaldo','Martínez','Roni','1996-04-25',15000000.00,5,4),(72,'Augusto','Lotti','Lotti','1996-06-10',16000000.00,5,4),(73,'Ignacio','Schor','El Pájaro','2000-08-04',14000000.00,5,4);
/*!40000 ALTER TABLE `futbolista` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `TRG_ValidarFutbolista_Insert` BEFORE INSERT ON `futbolista` FOR EACH ROW BEGIN
    DECLARE totalJugadores INT;

    IF NEW.cotizacion < 0 OR NEW.cotizacion > 99999999.99 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La cotizacion debe ser positiva y no puede superar los $99.999.999,99.';
    END IF;

    SELECT COUNT(*) INTO totalJugadores
    FROM Futbolista;

    IF totalJugadores >= 1500 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Se alcanzo el limite maximo de 1500 futbolistas en el sistema.';
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `futbolista_plantilla`
--

DROP TABLE IF EXISTS `futbolista_plantilla`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `futbolista_plantilla` (
  `idPlantilla` int unsigned NOT NULL,
  `idFutbolista` smallint unsigned NOT NULL,
  `futbolistaTitular` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idPlantilla`,`idFutbolista`),
  KEY `fk_FP_Futbolista` (`idFutbolista`),
  CONSTRAINT `fk_FP_Futbolista` FOREIGN KEY (`idFutbolista`) REFERENCES `futbolista` (`idFutbolista`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_FP_Plantilla` FOREIGN KEY (`idPlantilla`) REFERENCES `plantilla` (`idPlantilla`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `futbolista_plantilla`
--

LOCK TABLES `futbolista_plantilla` WRITE;
/*!40000 ALTER TABLE `futbolista_plantilla` DISABLE KEYS */;
INSERT INTO `futbolista_plantilla` VALUES (1,1,1),(1,8,0),(1,13,1),(1,27,0),(2,8,1),(2,68,1),(8,1,1);
/*!40000 ALTER TABLE `futbolista_plantilla` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `TRG_ValidarCapacidadPlantilla_Insert` BEFORE INSERT ON `futbolista_plantilla` FOR EACH ROW BEGIN
    DECLARE totalEnPlantilla INT;
    DECLARE maxPermitido TINYINT UNSIGNED DEFAULT 20;
    DECLARE presupuestoDisponible DECIMAL(10,2);
    DECLARE costoFutbolista DECIMAL(10,2);

    SELECT presupuesto INTO presupuestoDisponible
    FROM Plantilla
    WHERE idPlantilla = NEW.idPlantilla
    FOR UPDATE;

    SELECT COUNT(*) INTO totalEnPlantilla
    FROM Futbolista_Plantilla
    WHERE idPlantilla = NEW.idPlantilla;

    SELECT cotizacion INTO costoFutbolista
    FROM Futbolista
    WHERE idFutbolista = NEW.idFutbolista;

    IF totalEnPlantilla >= maxPermitido THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La plantilla no puede superar los 20 futbolistas.';
    END IF;

    IF presupuestoDisponible < costoFutbolista THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: Presupuesto insuficiente para incorporar este futbolista.';
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AfInsrtPlantillaJugadores` AFTER INSERT ON `futbolista_plantilla` FOR EACH ROW BEGIN

    UPDATE Plantilla
    SET presupuesto = presupuesto - (
            SELECT cotizacion
            FROM Futbolista
            WHERE idFutbolista = NEW.idFutbolista
        ),
        cantFutbolista = cantFutbolista + 1

    WHERE idPlantilla = NEW.idPlantilla;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AfDeletePlantillaFutbolista` AFTER DELETE ON `futbolista_plantilla` FOR EACH ROW BEGIN

    UPDATE Plantilla
    SET presupuesto = presupuesto + (
            SELECT cotizacion
            FROM Futbolista
            WHERE idFutbolista = OLD.idFutbolista
        ),
        cantFutbolista = cantFutbolista - 1

    WHERE idPlantilla = OLD.idPlantilla;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `plantilla`
--

DROP TABLE IF EXISTS `plantilla`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `plantilla` (
  `idPlantilla` int unsigned NOT NULL AUTO_INCREMENT,
  `nombreP` varchar(45) NOT NULL,
  `presupuesto` decimal(10,2) NOT NULL,
  `cantFutbolista` tinyint unsigned NOT NULL,
  `idUsuario` smallint unsigned NOT NULL,
  PRIMARY KEY (`idPlantilla`),
  KEY `fk_Plantilla_Usuario` (`idUsuario`),
  CONSTRAINT `fk_Plantilla_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plantilla`
--

LOCK TABLES `plantilla` WRITE;
/*!40000 ALTER TABLE `plantilla` DISABLE KEYS */;
INSERT INTO `plantilla` VALUES (1,'Plantilla de Valengiu',4000000.00,4,1),(2,'Plantilla de Admin',61000000.00,2,2),(3,'Plantilla de pepe',90000000.00,0,3),(4,'sixseven67',90000000.00,0,1),(5,'sixseven67',90000000.00,0,3),(6,'sixseven67',90000000.00,0,3),(7,'pasio',90000000.00,0,1),(8,'pasion',75000000.00,1,1);
/*!40000 ALTER TABLE `plantilla` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `puntuacion`
--

DROP TABLE IF EXISTS `puntuacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `puntuacion` (
  `idPuntuacion` int unsigned NOT NULL AUTO_INCREMENT,
  `idFutbolista` smallint unsigned NOT NULL,
  `puntuacion` decimal(3,1) DEFAULT NULL,
  `nroFecha` tinyint unsigned DEFAULT NULL,
  PRIMARY KEY (`idPuntuacion`),
  UNIQUE KEY `uq_Puntuacion_Futbolista_Fecha` (`idFutbolista`,`nroFecha`),
  CONSTRAINT `fk_Puntuacion_Futbolista` FOREIGN KEY (`idFutbolista`) REFERENCES `futbolista` (`idFutbolista`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `puntuacion_chk_1` CHECK (((`puntuacion` >= 1.0) and (`puntuacion` <= 10.0))),
  CONSTRAINT `puntuacion_chk_2` CHECK ((`nroFecha` < 50))
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `puntuacion`
--

LOCK TABLES `puntuacion` WRITE;
/*!40000 ALTER TABLE `puntuacion` DISABLE KEYS */;
INSERT INTO `puntuacion` VALUES (1,1,8.4,1),(2,8,7.8,1),(3,13,9.1,2),(4,27,10.0,3),(5,54,8.5,1);
/*!40000 ALTER TABLE `puntuacion` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `TRG_ValidarPuntuacion_Insert` BEFORE INSERT ON `puntuacion` FOR EACH ROW BEGIN
    DECLARE existe INT;

    IF NEW.puntuacion IS NOT NULL
       AND (NEW.puntuacion < 1.0 OR NEW.puntuacion > 10.0) THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: La puntuacion debe ser un decimal entre 1 y 10.';

    END IF;

    IF NEW.nroFecha IS NOT NULL AND NEW.nroFecha >= 50 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: El numero de fecha debe ser menor a 50.';

    END IF;

    IF NEW.nroFecha IS NOT NULL THEN

        SELECT COUNT(*) INTO existe
        FROM Puntuacion
        WHERE idFutbolista = NEW.idFutbolista
          AND nroFecha = NEW.nroFecha;

        IF existe > 0 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Error: El futbolista ya tiene una puntuacion registrada para esta fecha.';
        END IF;

    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `tipo_futbolista`
--

DROP TABLE IF EXISTS `tipo_futbolista`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipo_futbolista` (
  `idTipoFutbolista` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `tipoFutbolista` varchar(45) NOT NULL,
  PRIMARY KEY (`idTipoFutbolista`),
  UNIQUE KEY `uq_TipoFutbolista_Tipo` (`tipoFutbolista`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipo_futbolista`
--

LOCK TABLES `tipo_futbolista` WRITE;
/*!40000 ALTER TABLE `tipo_futbolista` DISABLE KEYS */;
INSERT INTO `tipo_futbolista` VALUES (1,'Arquero'),(2,'Defensor'),(4,'Delantero'),(3,'Mediocampista');
/*!40000 ALTER TABLE `tipo_futbolista` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `idUsuario` smallint unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `apellido` varchar(45) NOT NULL,
  `email` varchar(100) NOT NULL,
  `fechaNac` date DEFAULT NULL,
  `contrasena` char(64) NOT NULL,
  `es_admin` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `uq_Usuario_Email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'Valengiu','Sigma','valengiu0708@gmail.com','2008-08-07','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4',0),(2,'Admin','Sistema','admin@gran_dt.com','1990-01-01','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',1),(3,'pepe','capo','capo@gmail.com',NULL,'$2a$11$M9r4rGp7yIGUaFaUohgoRuJ8syFPBsK5kswD0wGVTug.8EYzCtRny',0);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `TRG_ValidarUsuario_Insert` BEFORE INSERT ON `usuario` FOR EACH ROW BEGIN
    DECLARE totalUsuarios INT;

    SELECT COUNT(*) INTO totalUsuarios
    FROM Usuario;

    IF totalUsuarios >= 2000 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Error: El sistema no admite mas de 2000 usuarios.';
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AftInsertUsuario` AFTER INSERT ON `usuario` FOR EACH ROW BEGIN

    INSERT INTO Plantilla (
        nombreP,
        presupuesto,
        cantFutbolista,
        idUsuario
    )
    VALUES (
        CONCAT('Plantilla de ', NEW.nombre),
        90000000,
        0,
        NEW.idUsuario
    );

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Dumping routines for database 'bd5_gran_dt'
--
/*!50003 DROP PROCEDURE IF EXISTS `AgregarFutbolistaAPlantilla` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AgregarFutbolistaAPlantilla`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaEquipo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaEquipo`(
    OUT unidEquipo TINYINT UNSIGNED,
    IN unnombre VARCHAR(45)
)
BEGIN
    INSERT INTO Equipo (nombre)
    VALUES (unnombre);

    SET unidEquipo = LAST_INSERT_ID();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaFutbolista` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaFutbolista`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaPlantilla` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaPlantilla`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaPuntuacion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaPuntuacion`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaTipoFutbolista` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaTipoFutbolista`(
    OUT unidTipoFutbolista TINYINT UNSIGNED,
    IN unTipoFutbolista VARCHAR(45)
)
BEGIN
    INSERT INTO Tipo_Futbolista (tipoFutbolista)
    VALUES (unTipoFutbolista);

    SET unidTipoFutbolista = LAST_INSERT_ID();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AltaUsuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AltaUsuario`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `BuscarPlantillaPorNombre` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `BuscarPlantillaPorNombre`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CalcularPresupuestoConsumido` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CalcularPresupuestoConsumido`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `LoginUsuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `LoginUsuario`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ObtenerPuntajePlantillaPorFecha` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerPuntajePlantillaPorFecha`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-11-24  8:34:33
