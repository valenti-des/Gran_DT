USE bd_gran_dt;

-- Alinea los límites y claves de la consigna con el esquema de la actividad anterior.
ALTER TABLE Plantilla DROP CHECK ck_Plantilla_Cantidad;
ALTER TABLE Plantilla ADD CONSTRAINT ck_Plantilla_Cantidad CHECK (cantMaxFutbolista BETWEEN 1 AND 20);
ALTER TABLE Usuario MODIFY `contraseña` VARCHAR(128) NOT NULL;

-- La regla de negocio es una puntuación por futbolista y jornada, independientemente de cantFech.
ALTER TABLE Puntuacion
    ADD UNIQUE KEY uq_Puntuacion_Futbolista_Jornada (idFutbolista, jornada);
