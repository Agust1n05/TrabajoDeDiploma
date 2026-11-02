/* ============================================================
   SCRIPT BASE - SGVCelulares (seguridad del sistema)
   SQL SERVER - base bd_sgvcelulares

   Orden:
     0. Base de datos y limpieza (permite volver a ejecutar el script)
     1. Tablas de seguridad (idioma, DV, composite, usuario, bitácora)
     2. Datos iniciales (permisos, idiomas, rol Administrador, usuario admin)
     3. Consultas de verificación (comentadas)

   Ejecutar este script ANTES de ScriptNegocio.sql. La limpieza
   también borra las tablas de negocio (Tecnico referencia a usuario),
   así que después hay que volver a correr ScriptNegocio.sql.

   IMPORTANTE: los registros insertados por script no tienen
   dígito verificador. En el primer login, el administrador va a
   ver la inconsistencia y debe elegir "Recalcular DV".

   Usuario inicial:  admin / Admin!123
   ============================================================ */

IF DB_ID('bd_sgvcelulares') IS NULL
    CREATE DATABASE bd_sgvcelulares;
GO

USE bd_sgvcelulares;
GO

/* ============================================================
   0. LIMPIEZA
   Se borra de las tablas hijas a las padres para respetar las FK.
   ============================================================ */

/* ---------- Negocio (las vuelve a crear ScriptNegocio.sql) ---------- */
DROP TABLE IF EXISTS DetalleRepuestoReclamo;
DROP TABLE IF EXISTS Reclamo;
DROP TABLE IF EXISTS Garantia;
DROP TABLE IF EXISTS ComprobanteCobro;
DROP TABLE IF EXISTS Factura;          -- nombre anterior de ComprobanteCobro
DROP TABLE IF EXISTS Presupuesto;
DROP TABLE IF EXISTS DetalleRepuesto;
DROP TABLE IF EXISTS Diagnostico;
DROP TABLE IF EXISTS OrdenDeTrabajo;
DROP TABLE IF EXISTS Repuesto;
DROP TABLE IF EXISTS Equipo;
DROP TABLE IF EXISTS Tecnico;
DROP TABLE IF EXISTS Cliente;

/* ---------- Seguridad ---------- */
DROP TABLE IF EXISTS bitacora;
DROP TABLE IF EXISTS usuario;
DROP TABLE IF EXISTS rol_permiso;
DROP TABLE IF EXISTS permiso_familia;
DROP TABLE IF EXISTS familia_familia;
DROP TABLE IF EXISTS rol_familia;
DROP TABLE IF EXISTS permiso;
DROP TABLE IF EXISTS familia;
DROP TABLE IF EXISTS rol;
DROP TABLE IF EXISTS dvh;
DROP TABLE IF EXISTS dvv;
DROP TABLE IF EXISTS idioma;
GO

/* ============================================================
   1. TABLAS DE SEGURIDAD
   ============================================================ */

/* ---------- Idioma ---------- */

CREATE TABLE idioma
(
    idIdioma VARCHAR(8)  NOT NULL,
    nombre   VARCHAR(50) NOT NULL,

    CONSTRAINT PK_idioma PRIMARY KEY (idIdioma)
);

/* ---------- Dígitos verificadores ---------- */

CREATE TABLE dvv
(
    nombreTabla VARCHAR(50) NOT NULL,
    valor       VARCHAR(64) NOT NULL,

    CONSTRAINT PK_DVV PRIMARY KEY (nombreTabla)
);

CREATE TABLE dvh
(
    nombreTabla VARCHAR(50) NOT NULL,
    idRegistro  VARCHAR(50) NOT NULL,
    valor       VARCHAR(64) NOT NULL,

    CONSTRAINT PK_DVH PRIMARY KEY (nombreTabla, idRegistro)
);

/* ---------- Composite (rol -> familias -> permisos) ---------- */

CREATE TABLE rol
(
    idRol  INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(50) NOT NULL,

    CONSTRAINT pk_rol PRIMARY KEY (idRol)
);

CREATE TABLE familia
(
    idFamilia INT IDENTITY(1,1) NOT NULL,
    nombre    VARCHAR(50) NOT NULL,

    CONSTRAINT pk_familia PRIMARY KEY (idFamilia)
);

CREATE TABLE permiso
(
    idPermiso INT IDENTITY(1,1) NOT NULL,
    nombre    VARCHAR(50) NOT NULL,

    CONSTRAINT pk_permiso PRIMARY KEY (idPermiso)
);

CREATE TABLE rol_familia
(
    idRol     INT NOT NULL,
    idFamilia INT NOT NULL,

    CONSTRAINT pk_rol_familia PRIMARY KEY (idRol, idFamilia),
    CONSTRAINT fk_rol_familia_rol     FOREIGN KEY (idRol)     REFERENCES rol(idRol),
    CONSTRAINT fk_rol_familia_familia FOREIGN KEY (idFamilia) REFERENCES familia(idFamilia)
);

CREATE TABLE familia_familia
(
    idFamiliaPadre INT NOT NULL,
    idFamiliaHija  INT NOT NULL,

    CONSTRAINT pk_familia_familia PRIMARY KEY (idFamiliaPadre, idFamiliaHija),
    CONSTRAINT fk_ff_padre FOREIGN KEY (idFamiliaPadre) REFERENCES familia(idFamilia),
    CONSTRAINT fk_ff_hija  FOREIGN KEY (idFamiliaHija)  REFERENCES familia(idFamilia)
);

CREATE TABLE permiso_familia
(
    idFamilia INT NOT NULL,
    idPermiso INT NOT NULL,

    CONSTRAINT pk_permiso_familia PRIMARY KEY (idFamilia, idPermiso),
    CONSTRAINT fk_permiso_familia_familia FOREIGN KEY (idFamilia) REFERENCES familia(idFamilia),
    CONSTRAINT fk_permiso_familia_permiso FOREIGN KEY (idPermiso) REFERENCES permiso(idPermiso)
);

CREATE TABLE rol_permiso
(
    idRol     INT NOT NULL,
    idPermiso INT NOT NULL,

    CONSTRAINT pk_rol_permiso PRIMARY KEY (idRol, idPermiso),
    CONSTRAINT fk_rol_permiso_rol     FOREIGN KEY (idRol)     REFERENCES rol(idRol),
    CONSTRAINT fk_rol_permiso_permiso FOREIGN KEY (idPermiso) REFERENCES permiso(idPermiso)
);

/* ---------- Usuario y bitácora ---------- */

CREATE TABLE usuario
(
    dni           VARCHAR(8)  NOT NULL,
    nombre        VARCHAR(50) NOT NULL,
    apellido      VARCHAR(50) NOT NULL,
    correo        VARCHAR(50) NOT NULL,
    nombreUsuario VARCHAR(50) NOT NULL,
    password      VARCHAR(64) NOT NULL,   -- SHA-256 en hexadecimal
    bloqueo       BIT         NOT NULL,
    activo        BIT         NOT NULL,
    cantIntentos  TINYINT     NOT NULL,
    idRol         INT         NOT NULL,
    idIdioma      VARCHAR(8)  NOT NULL,

    CONSTRAINT PK_usuario PRIMARY KEY (dni),
    CONSTRAINT FK_usuario_rol    FOREIGN KEY (idRol)    REFERENCES rol(idRol),
    CONSTRAINT FK_usuario_idioma FOREIGN KEY (idIdioma) REFERENCES idioma(idIdioma)
);

CREATE TABLE bitacora
(
    idBitacora INT IDENTITY(1,1) NOT NULL,
    dni        VARCHAR(8)  NOT NULL,
    fecha      DATETIME    NOT NULL,
    modulo     VARCHAR(50) NOT NULL,
    evento     VARCHAR(50) NOT NULL,
    criticidad TINYINT     NOT NULL,

    CONSTRAINT PK_bitacora PRIMARY KEY (idBitacora),
    CONSTRAINT FK_bitacora_usuario FOREIGN KEY (dni) REFERENCES usuario(dni)
);
GO

/* ============================================================
   2. DATOS INICIALES
   ============================================================ */

BEGIN TRANSACTION;
BEGIN TRY

    /* ---------- Idiomas ---------- */
    INSERT INTO idioma (idIdioma, nombre)
    VALUES
    ('es-AR', 'Español (Argentina)'),
    ('en-US', 'English (United States)');

    /* ---------- Permisos de seguridad (los de negocio los agrega ScriptNegocio.sql) ---------- */
    INSERT INTO permiso (nombre)
    VALUES
    ('Crear Usuario'),
    ('Modificar Usuario'),
    ('Desbloquear Usuario'),
    ('Activar Usuario'),
    ('Desactivar Usuario'),
    ('Consultar Bitácora'),
    ('Exportar Bitácora'),
    ('Crear Rol'),
    ('Crear Familia'),
    ('Realizar Backup'),
    ('Realizar Restore'),
    ('Eliminar Rol'),
    ('Eliminar Familia');

    /* ---------- Rol Administrador con todos los permisos ---------- */
    INSERT INTO rol (nombre)
    VALUES ('Administrador');

    INSERT INTO rol_permiso (idRol, idPermiso)
    SELECT ro.idRol, pe.idPermiso
    FROM rol ro
    CROSS JOIN permiso pe
    WHERE ro.nombre = 'Administrador';

    /* ---------- Usuario administrador ---------- */
    INSERT INTO usuario (dni, nombre, apellido, correo, nombreUsuario, password, bloqueo, activo, cantIntentos, idRol, idIdioma)
    SELECT '12345678', 'Bruno', 'Roca', 'admin@sgv.com', 'admin',
           LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Admin!123'), 2)),
           0, 1, 0, ro.idRol, 'es-AR'
    FROM rol ro
    WHERE ro.nombre = 'Administrador';

    COMMIT TRANSACTION;
    PRINT 'DATOS DE SEGURIDAD INSERTADOS CORRECTAMENTE';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERROR AL INSERTAR LOS DATOS DE SEGURIDAD: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO

/* ============================================================
   3. CONSULTAS DE VERIFICACIÓN (seleccionar y ejecutar a mano)
   ============================================================

SELECT * FROM idioma;
SELECT * FROM usuario;
SELECT * FROM bitacora;
SELECT * FROM permiso;
SELECT * FROM rol;
SELECT * FROM familia;

-- Composite: una familia determinada
SELECT idFamilia, nombre FROM familia WHERE idFamilia = 1;

-- Composite: permisos de una familia
SELECT p.idPermiso, p.nombre
FROM permiso p
INNER JOIN permiso_familia pf ON p.idPermiso = pf.idPermiso
WHERE pf.idFamilia = 1;

-- Composite: familias hijas de una familia padre
SELECT f.idFamilia, f.nombre
FROM familia f
INNER JOIN familia_familia ff ON f.idFamilia = ff.idFamiliaHija
WHERE ff.idFamiliaPadre = 1;

-- Prueba de inconsistencia de DV (insertar fuera del sistema y volver a loguearse)
INSERT INTO rol (nombre) VALUES ('test');

   ============================================================ */
