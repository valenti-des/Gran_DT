USE bd_gran_dt;

CREATE USER IF NOT EXISTS 'administrador'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_PASSWORD_ADMIN_DE_EJEMPLO';
CREATE USER IF NOT EXISTS 'invitado'@'localhost'
    IDENTIFIED BY 'REEMPLAZAR_CON_PASSWORD_INVITADO_DE_EJEMPLO';

REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'administrador'@'localhost';
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'invitado'@'localhost';

GRANT ALL PRIVILEGES ON bd_gran_dt.* TO 'administrador'@'localhost';
GRANT SELECT ON bd_gran_dt.* TO 'invitado'@'localhost';

SHOW GRANTS FOR 'administrador'@'localhost';
SHOW GRANTS FOR 'invitado'@'localhost';
