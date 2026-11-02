/* ============================================================
   SCRIPT DE NEGOCIO - RFN-01 Reparación de un celular
                       RFN-02 Reclamo y resolución de garantía
   SQL SERVER - base bd_sgvcelulares

   Orden:
     0. Limpieza (permite volver a ejecutar el script)
     1. Tablas de negocio
     2. Roles, familias, permisos y usuarios de RF01 y RF02
     3. Datos de prueba coherentes con los estados de la OT

   IMPORTANTE: los registros insertados por script no tienen
   dígito verificador. En el primer login, el administrador va a
   ver la inconsistencia y debe elegir "Recalcular DV".
   ============================================================ */

USE bd_sgvcelulares;
GO

/* ============================================================
   0. LIMPIEZA
   ============================================================ */

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
GO

DELETE FROM dvh WHERE NombreTabla IN ('cliente', 'tecnico', 'equipo', 'repuesto', 'ordendetrabajo',
                                      'diagnostico', 'detallerepuesto', 'presupuesto', 'comprobantecobro', 'factura', 'garantia',
                                      'reclamo', 'detallerepuestoreclamo');
DELETE FROM dvv WHERE NombreTabla IN ('cliente', 'tecnico', 'equipo', 'repuesto', 'ordendetrabajo',
                                      'diagnostico', 'detallerepuesto', 'presupuesto', 'comprobantecobro', 'factura', 'garantia',
                                      'reclamo', 'detallerepuestoreclamo');
GO

/* ============================================================
   1. TABLAS DE NEGOCIO
   ============================================================ */

/* ---------- Maestros (baja lógica con Activo) ---------- */

CREATE TABLE Cliente
(
    dni      VARCHAR(8)  NOT NULL,
    Nombre   VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Activo   BIT         NOT NULL CONSTRAINT DF_Cliente_Activo DEFAULT 1,

    CONSTRAINT PK_Cliente PRIMARY KEY (dni)
);

-- El técnico es una entidad propia, pero su DNI es el de su usuario del sistema.
CREATE TABLE Tecnico
(
    Dni        VARCHAR(8)  NOT NULL,
    Nombre     VARCHAR(50) NOT NULL,
    Apellido   VARCHAR(50) NOT NULL,
    Disponible BIT         NOT NULL CONSTRAINT DF_Tecnico_Disponible DEFAULT 1, -- entra en la asignación automática
    Activo     BIT         NOT NULL CONSTRAINT DF_Tecnico_Activo DEFAULT 1,     -- baja lógica

    CONSTRAINT PK_Tecnico PRIMARY KEY (Dni),
    CONSTRAINT FK_Tecnico_Usuario FOREIGN KEY (Dni) REFERENCES usuario(dni)
);

-- El equipo se reutiliza entre OT (se identifica por IMEI). No guarda dueño ni falla.
CREATE TABLE Equipo
(
    idEquipo INT IDENTITY(1,1) NOT NULL,
    marca    VARCHAR(50) NOT NULL,
    modelo   VARCHAR(50) NOT NULL,
    imei     CHAR(15)    NOT NULL,
    Activo   BIT         NOT NULL CONSTRAINT DF_Equipo_Activo DEFAULT 1,

    CONSTRAINT PK_Equipo PRIMARY KEY (idEquipo),
    CONSTRAINT UQ_Equipo_Imei UNIQUE (imei)
);

CREATE TABLE Repuesto
(
    Codigo          VARCHAR(20)   NOT NULL,
    Descripcion     VARCHAR(100)  NOT NULL,
    PrecioUnitario  DECIMAL(18,2) NOT NULL,
    stockDisponible INT           NOT NULL,
    Activo          BIT           NOT NULL CONSTRAINT DF_Repuesto_Activo DEFAULT 1,

    CONSTRAINT PK_Repuesto PRIMARY KEY (Codigo),
    CONSTRAINT CK_Repuesto_Precio CHECK (PrecioUnitario >= 0),
    CONSTRAINT CK_Repuesto_Stock  CHECK (stockDisponible >= 0)
);

/* ---------- Movimientos ---------- */

CREATE TABLE OrdenDeTrabajo
(
    NroOT            INT IDENTITY(1,1) NOT NULL,
    Estado           VARCHAR(30)  NOT NULL,
    dni              VARCHAR(8)   NOT NULL,
    idEquipo         INT          NOT NULL,
    DniTecnico       VARCHAR(8)   NOT NULL,
    DescripcionFalla VARCHAR(200) NOT NULL,

    CONSTRAINT PK_OrdenDeTrabajo PRIMARY KEY (NroOT),
    CONSTRAINT FK_OrdenDeTrabajo_Cliente FOREIGN KEY (dni)        REFERENCES Cliente(dni),
    CONSTRAINT FK_OrdenDeTrabajo_Equipo  FOREIGN KEY (idEquipo)   REFERENCES Equipo(idEquipo),
    CONSTRAINT FK_OrdenDeTrabajo_Tecnico FOREIGN KEY (DniTecnico) REFERENCES Tecnico(Dni),
    CONSTRAINT CK_OrdenDeTrabajo_Estado CHECK (Estado IN
        ('Recibida', 'En diagnostico', 'Presupuestado', 'En reparacion', 'Reparado',
         'Entregado', 'Sin reparar', 'Entregado sin reparar'))
);

-- Un equipo no puede tener dos OT sin entregar (mismos estados que EstadosOT.Abiertas).
-- La recepción ya lo valida; el índice cubre el caso de dos recepciones simultáneas.
CREATE UNIQUE INDEX UX_OrdenDeTrabajo_EquipoAbierta ON OrdenDeTrabajo (idEquipo)
    WHERE Estado IN ('Recibida', 'En diagnostico', 'Presupuestado', 'En reparacion', 'Reparado', 'Sin reparar');

CREATE TABLE Diagnostico
(
    IdDiagnostico   INT IDENTITY(1,1) NOT NULL,
    detalle         VARCHAR(500)  NOT NULL,
    costo_mano_obra DECIMAL(18,2) NOT NULL,
    NroOT           INT           NOT NULL,

    CONSTRAINT PK_Diagnostico PRIMARY KEY (IdDiagnostico),
    CONSTRAINT UQ_Diagnostico_NroOT UNIQUE (NroOT),
    CONSTRAINT FK_Diagnostico_OrdenDeTrabajo FOREIGN KEY (NroOT) REFERENCES OrdenDeTrabajo(NroOT),
    CONSTRAINT CK_Diagnostico_ManoObra CHECK (costo_mano_obra >= 0)
);

CREATE TABLE DetalleRepuesto
(
    IdDiagnostico     INT           NOT NULL,
    Codigo            VARCHAR(20)   NOT NULL,
    PrecioUnitario    DECIMAL(18,2) NOT NULL,   -- precio vigente al diagnosticar (el presupuesto no cambia si después cambia el precio)
    cantidadRequerida INT           NOT NULL,
    CantidadUtilizada INT         NOT NULL CONSTRAINT DF_DetalleRepuesto_Utilizada DEFAULT 0,

    CONSTRAINT PK_DetalleRepuesto PRIMARY KEY (IdDiagnostico, Codigo),
    CONSTRAINT FK_DetalleRepuesto_Diagnostico FOREIGN KEY (IdDiagnostico) REFERENCES Diagnostico(IdDiagnostico),
    CONSTRAINT FK_DetalleRepuesto_Repuesto    FOREIGN KEY (Codigo)        REFERENCES Repuesto(Codigo),
    CONSTRAINT CK_DetalleRepuesto_Precio    CHECK (PrecioUnitario >= 0),
    CONSTRAINT CK_DetalleRepuesto_Requerida CHECK (cantidadRequerida > 0),
    CONSTRAINT CK_DetalleRepuesto_Utilizada CHECK (CantidadUtilizada >= 0)
);

CREATE TABLE Presupuesto
(
    IdPresupuesto     INT IDENTITY(1,1) NOT NULL,
    TotalPresupuesto  DECIMAL(18,2) NOT NULL,
    EstadoPresupuesto VARCHAR(20)   NOT NULL,
    FechaRespuesta    DATE          NULL,      -- NULL mientras está Pendiente
    NroOT             INT           NOT NULL,

    CONSTRAINT PK_Presupuesto PRIMARY KEY (IdPresupuesto),
    CONSTRAINT UQ_Presupuesto_NroOT UNIQUE (NroOT),
    CONSTRAINT FK_Presupuesto_OrdenDeTrabajo FOREIGN KEY (NroOT) REFERENCES OrdenDeTrabajo(NroOT),
    CONSTRAINT CK_Presupuesto_Estado CHECK (EstadoPresupuesto IN ('Pendiente', 'Aprobado', 'Rechazado'))
);

-- Solo se guarda el tipo de tarjeta y los últimos 4 dígitos: nunca el número completo ni el CVV.
CREATE TABLE ComprobanteCobro
(
    NroComprobante  INT IDENTITY(1,1) NOT NULL,
    NroOT           INT           NOT NULL,
    MontoCobrado    DECIMAL(18,2) NOT NULL,
    MedioPago       VARCHAR(10)   NOT NULL,
    UltimosDigitos  CHAR(4)       NOT NULL,
    FechaEntrega    DATE          NOT NULL,

    CONSTRAINT PK_ComprobanteCobro PRIMARY KEY (NroComprobante),
    CONSTRAINT UQ_ComprobanteCobro_NroOT UNIQUE (NroOT),
    CONSTRAINT FK_ComprobanteCobro_OrdenDeTrabajo FOREIGN KEY (NroOT) REFERENCES OrdenDeTrabajo(NroOT),
    CONSTRAINT CK_ComprobanteCobro_MedioPago CHECK (MedioPago IN ('Debito', 'Credito'))
);

-- Una garantía por OT reparada y entregada (vence 90 días después de la entrega).
CREATE TABLE Garantia
(
    NroOT            INT  NOT NULL,
    FechaVencimiento DATE NOT NULL,

    CONSTRAINT PK_Garantia PRIMARY KEY (NroOT),
    CONSTRAINT FK_Garantia_OrdenDeTrabajo FOREIGN KEY (NroOT) REFERENCES OrdenDeTrabajo(NroOT)
);

/* ---------- RFN-02 Reclamo y resolución de garantía ---------- */

-- Reclamo por garantía. La FK a Garantia impide reclamar sobre una OT sin garantía.
-- Una OT puede tener varios reclamos, pero solo uno abierto a la vez.
CREATE TABLE Reclamo
(
    NroReclamo        INT IDENTITY(1,1) NOT NULL,
    NroOT             INT          NOT NULL,
    Motivo            VARCHAR(100) NOT NULL,
    Descripcion       VARCHAR(200) NOT NULL,
    FechaReclamo      DATE         NOT NULL,
    Estado            VARCHAR(30)  NOT NULL,
    DniTecnico        VARCHAR(8)   NULL,      -- NULL si se rechazó por garantía vencida
    FallaCubierta     BIT          NULL,      -- NULL hasta la evaluación técnica
    DetalleEvaluacion VARCHAR(500) NULL,

    CONSTRAINT PK_Reclamo PRIMARY KEY (NroReclamo),
    CONSTRAINT FK_Reclamo_Garantia FOREIGN KEY (NroOT)      REFERENCES Garantia(NroOT),
    CONSTRAINT FK_Reclamo_Tecnico  FOREIGN KEY (DniTecnico) REFERENCES Tecnico(Dni),
    CONSTRAINT CK_Reclamo_Estado CHECK (Estado IN
        ('Pendiente', 'En evaluacion', 'Rechazado', 'Aprobado', 'En espera de repuesto',
         'Reparado', 'Resuelto', 'Entregado sin reparar'))
);

-- Una OT no puede tener dos reclamos abiertos (mismos estados que EstadosReclamo.Abiertos).
-- El registro ya lo valida; el índice cubre el caso de dos registros simultáneos.
CREATE UNIQUE INDEX UX_Reclamo_OTAbierta ON Reclamo (NroOT)
    WHERE Estado IN ('Pendiente', 'En evaluacion', 'Rechazado', 'Aprobado', 'En espera de repuesto', 'Reparado');

-- Repuestos usados en la reparación sin cargo (se descuentan del stock).
CREATE TABLE DetalleRepuestoReclamo
(
    NroReclamo        INT         NOT NULL,
    Codigo            VARCHAR(20) NOT NULL,
    CantidadUtilizada INT         NOT NULL,

    CONSTRAINT PK_DetalleRepuestoReclamo PRIMARY KEY (NroReclamo, Codigo),
    CONSTRAINT FK_DetalleRepuestoReclamo_Reclamo  FOREIGN KEY (NroReclamo) REFERENCES Reclamo(NroReclamo),
    CONSTRAINT FK_DetalleRepuestoReclamo_Repuesto FOREIGN KEY (Codigo)     REFERENCES Repuesto(Codigo),
    CONSTRAINT CK_DetalleRepuestoReclamo_Utilizada CHECK (CantidadUtilizada > 0)
);
GO

/* ============================================================
   2. ROLES, FAMILIAS, PERMISOS Y USUARIOS DE RF01
   ============================================================ */

BEGIN TRANSACTION;
BEGIN TRY

    /* ---------- Permisos (los usa FrmMenu para mostrar cada botón) ---------- */
    INSERT INTO permiso (nombre)
    SELECT p.nombre
    FROM (VALUES ('Gestionar Clientes'), ('Gestionar Equipos'), ('Gestionar Tecnicos'), ('Gestionar Repuestos'),
                 ('Recepcionar Equipo'), ('Gestionar Presupuesto'), ('Entregar Equipo'),
                 ('Diagnosticar Equipo'), ('Reparar Equipo'),
                 ('Registrar Reclamo'), ('Evaluar Reclamo')) AS p(nombre)
    WHERE NOT EXISTS (SELECT 1 FROM permiso x WHERE x.nombre = p.nombre);

    /* ---------- Familias ---------- */
    INSERT INTO familia (nombre)
    SELECT f.nombre
    FROM (VALUES ('Gestion maestros vendedor'), ('Gestion OT vendedor'), ('Gestion OT tecnico'),
                 ('Gestion reclamos vendedor'), ('Gestion reclamos tecnico')) AS f(nombre)
    WHERE NOT EXISTS (SELECT 1 FROM familia x WHERE x.nombre = f.nombre);

    INSERT INTO permiso_familia (idFamilia, idPermiso)
    SELECT fa.idFamilia, pe.idPermiso
    FROM (VALUES ('Gestion maestros vendedor', 'Gestionar Clientes'),
                 ('Gestion maestros vendedor', 'Gestionar Equipos'),
                 ('Gestion OT vendedor',       'Recepcionar Equipo'),
                 ('Gestion OT vendedor',       'Gestionar Presupuesto'),
                 ('Gestion OT vendedor',       'Entregar Equipo'),
                 ('Gestion OT tecnico',        'Diagnosticar Equipo'),
                 ('Gestion OT tecnico',        'Reparar Equipo'),
                 ('Gestion reclamos vendedor', 'Registrar Reclamo'),
                 ('Gestion reclamos tecnico',  'Evaluar Reclamo')) AS v(familia, permiso)
    INNER JOIN familia fa ON fa.nombre = v.familia
    INNER JOIN permiso pe ON pe.nombre = v.permiso
    WHERE NOT EXISTS (SELECT 1 FROM permiso_familia x WHERE x.idFamilia = fa.idFamilia AND x.idPermiso = pe.idPermiso);

    /* ---------- Roles ---------- */
    INSERT INTO rol (nombre)
    SELECT r.nombre
    FROM (VALUES ('Vendedor'), ('Tecnico')) AS r(nombre)
    WHERE NOT EXISTS (SELECT 1 FROM rol x WHERE x.nombre = r.nombre);

    INSERT INTO rol_familia (idRol, idFamilia)
    SELECT ro.idRol, fa.idFamilia
    FROM (VALUES ('Vendedor', 'Gestion maestros vendedor'),
                 ('Vendedor', 'Gestion OT vendedor'),
                 ('Tecnico',  'Gestion OT tecnico'),
                 ('Vendedor', 'Gestion reclamos vendedor'),
                 ('Tecnico',  'Gestion reclamos tecnico')) AS v(rol, familia)
    INNER JOIN rol ro     ON ro.nombre = v.rol
    INNER JOIN familia fa ON fa.nombre = v.familia
    WHERE NOT EXISTS (SELECT 1 FROM rol_familia x WHERE x.idRol = ro.idRol AND x.idFamilia = fa.idFamilia);

    -- El administrador recibe todos los permisos nuevos de forma directa.
    INSERT INTO rol_permiso (idRol, idPermiso)
    SELECT ro.idRol, pe.idPermiso
    FROM rol ro
    CROSS JOIN permiso pe
    WHERE ro.nombre IN ('Administrador', 'Admin')
      AND pe.nombre IN ('Gestionar Clientes', 'Gestionar Equipos', 'Gestionar Tecnicos', 'Gestionar Repuestos',
                        'Recepcionar Equipo', 'Gestionar Presupuesto', 'Entregar Equipo',
                        'Diagnosticar Equipo', 'Reparar Equipo',
                        'Registrar Reclamo', 'Evaluar Reclamo')
      AND NOT EXISTS (SELECT 1 FROM rol_permiso x WHERE x.idRol = ro.idRol AND x.idPermiso = pe.idPermiso);

    /* ---------- Usuarios de prueba ----------
       Convención del sistema: usuario = Apellido + DNI, contraseña inicial = DNI + Apellido (SHA-256).
       Apellidos sin tilde para que el hash de SQL coincida con el de SER_Cripto (UTF-8). */
    INSERT INTO usuario (dni, nombre, apellido, correo, nombreUsuario, password, bloqueo, activo, cantIntentos, idRol, idIdioma)
    SELECT v.dni, v.nombre, v.apellido, v.correo, v.apellido + v.dni,
           LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', v.dni + v.apellido), 2)),
           0, 1, 0, ro.idRol, 'es-AR'
    FROM (VALUES ('31222333', 'Lucas',    'Fernandez', 'lfernandez@santsung.com', 'Vendedor'),
                 ('30111222', 'Roberto',  'Garcia',    'rgarcia@santsung.com',    'Tecnico'),
                 ('32444555', 'Federico', 'Morales',   'fmorales@santsung.com',   'Tecnico'),
                 ('28777666', 'Hernan',   'Castro',    'hcastro@santsung.com',    'Tecnico'),
                 ('35666777', 'Mariano',  'Vega',      'mvega@santsung.com',      'Tecnico'),
                 ('33999888', 'Pablo',    'Navarro',   'pnavarro@santsung.com',   'Tecnico')) AS v(dni, nombre, apellido, correo, rol)
    INNER JOIN rol ro ON ro.nombre = v.rol
    WHERE NOT EXISTS (SELECT 1 FROM usuario x WHERE x.dni = v.dni);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERROR AL CARGAR ROLES, PERMISOS Y USUARIOS: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO

/* ============================================================
   3. DATOS DE PRUEBA
   Cada OT tiene solo los datos que corresponden a su estado:

   OT  Estado                 Técnico   Qué tiene
   1   Recibida               Roberto   -
   2   En diagnostico         Federico  -
   3   Presupuestado          Roberto   diagnóstico + presupuesto Pendiente
   4   En reparacion          Mariano   diagnóstico + presupuesto Aprobado
   5   Reparado               Pablo     diagnóstico + presupuesto Aprobado + cantidades utilizadas
   6   Entregado              Federico  todo + comprobante de cobro + garantía VIGENTE
   7   Entregado sin reparar  Roberto   diagnóstico + presupuesto Rechazado
   8   Sin reparar            Mariano   diagnóstico sin stock (REP008 tiene stock 0), sin presupuesto
   9   Entregado              Pablo     todo + comprobante de cobro + garantía VENCIDA
   10  Presupuestado          Federico  diagnóstico con 2 repuestos + presupuesto Pendiente
   11  Recibida               Pablo     reutiliza el equipo 9 (mismo IMEI, mismo cliente)
   12  Entregado              Hernán    todo + comprobante de cobro + garantía VENCIDA

   Casos de PN-02 (reclamo por garantía):
     OT 6  garantía vigente               → el reclamo queda "En evaluacion" con técnico asignado
     OT 12 garantía vencida               → el reclamo queda "Rechazado"
     OT 9  su equipo está en el taller (OT 11 sin entregar) → no se puede registrar el reclamo

   OT en curso por técnico (Recibida, En diagnostico, Presupuestado, En reparacion):
   Roberto 2 · Federico 2 · Mariano 1 · Pablo 1 · Hernán no disponible
   ============================================================ */

BEGIN TRANSACTION;
BEGIN TRY

    INSERT INTO Cliente (dni, Nombre, Apellido, Telefono, Activo)
    VALUES
    ('40111222', 'Juan',      'Pérez',     '1123456789', 1),
    ('42333444', 'Lucía',     'Gómez',     '1156789012', 1),
    ('38987654', 'Martín',    'Rodríguez', '1145678234', 1),
    ('44555666', 'Sofía',     'Fernández', '1167890123', 1),
    ('36777888', 'Carlos',    'López',     '1134567890', 1),
    ('41888999', 'Valentina', 'Martínez',  '1178901234', 1),
    ('39222111', 'Nicolás',   'Sánchez',   '1143215678', 1),
    ('45666777', 'Camila',    'Romero',    '1165432109', 1),
    ('37444555', 'Diego',     'Torres',    '1122334455', 1),
    ('43111999', 'Agustina',  'Díaz',      '1187654321', 1);

    INSERT INTO Tecnico (Dni, Nombre, Apellido, Disponible, Activo)
    VALUES
    -- Nombre y apellido iguales a los de su usuario (el ABM los copia del usuario).
    ('30111222', 'Roberto',  'Garcia',  1, 1),
    ('32444555', 'Federico', 'Morales', 1, 1),
    ('28777666', 'Hernan',   'Castro',  0, 1),   -- de licencia: no recibe OT nuevas
    ('35666777', 'Mariano',  'Vega',    1, 1),
    ('33999888', 'Pablo',    'Navarro', 1, 1);

    -- idEquipo se genera solo: quedan del 1 al 11 en este orden.
    INSERT INTO Equipo (marca, modelo, imei, Activo)
    VALUES
    ('Samsung',  'Galaxy S23',    '356789012345671', 1),
    ('Apple',    'iPhone 13',     '357890123456782', 1),
    ('Motorola', 'Moto G84',      '358901234567893', 1),
    ('Xiaomi',   'Redmi Note 12', '359012345678904', 1),
    ('Samsung',  'Galaxy A54',    '360123456789015', 1),
    ('Apple',    'iPhone 12',     '361234567890126', 1),
    ('Motorola', 'Edge 40',       '362345678901237', 1),
    ('Xiaomi',   'Poco X5 Pro',   '363456789012348', 1),
    ('Samsung',  'Galaxy S22',    '364567890123459', 1),
    ('Apple',    'iPhone 14',     '365678901234560', 1),
    ('Samsung',  'Galaxy A34',    '366789012345671', 1);

    INSERT INTO Repuesto (Codigo, Descripcion, PrecioUnitario, stockDisponible, Activo)
    VALUES
    ('REP001', 'Conector de carga USB-C genérico',   4500.00, 15, 1),
    ('REP002', 'Pantalla iPhone 13',                85000.00,  3, 1),
    ('REP003', 'Batería Motorola Moto G84',         18000.00, 10, 1),
    ('REP004', 'Placa de encendido Xiaomi',         32000.00,  4, 1),
    ('REP005', 'Cámara trasera Samsung Galaxy A54', 27000.00,  5, 1),
    ('REP006', 'Parlante inferior iPhone 12',        8000.00, 12, 1),
    ('REP007', 'Módulo de cámara Motorola Edge 40', 35000.00,  6, 1),
    ('REP008', 'Conector USB-C Xiaomi Poco',         5000.00,  0, 1),   -- sin stock (OT 8)
    ('REP009', 'Pantalla Samsung Galaxy S22',       95000.00,  2, 1),
    ('REP010', 'Batería iPhone 14',                 30000.00,  8, 1),
    ('REP011', 'Flex de carga Samsung Galaxy S23',   7000.00,  9, 1),
    ('REP012', 'Vidrio templado genérico',           2500.00, 30, 1);

    INSERT INTO OrdenDeTrabajo (Estado, dni, idEquipo, DniTecnico, DescripcionFalla)
    VALUES
    ('Recibida',              '40111222',  1, '30111222', 'No carga'),
    ('En diagnostico',        '42333444',  2, '32444555', 'Pantalla rota por caída'),
    ('Presupuestado',         '38987654',  3, '30111222', 'La batería dura muy poco'),
    ('En reparacion',         '44555666',  4, '35666777', 'No enciende'),
    ('Reparado',              '36777888',  5, '33999888', 'La cámara trasera no enfoca'),
    ('Entregado',             '41888999',  6, '32444555', 'No se escucha el audio en llamadas'),
    ('Entregado sin reparar', '39222111',  7, '30111222', 'La cámara se cierra sola'),
    ('Sin reparar',           '45666777',  8, '35666777', 'No reconoce el cargador'),
    ('Entregado',             '37444555',  9, '33999888', 'Pantalla con líneas verticales'),
    ('Presupuestado',         '43111999', 10, '32444555', 'Se apaga con 30% de batería'),
    ('Recibida',              '37444555',  9, '33999888', 'Vuelve a mostrar líneas en la pantalla'),
    ('Entregado',             '42333444', 11, '28777666', 'No carga');

    -- IdDiagnostico se genera solo: quedan del 1 al 9 en este orden.
    INSERT INTO Diagnostico (detalle, costo_mano_obra, NroOT)
    VALUES
    ('Batería deteriorada, no retiene la carga',             10000.00,  3),  -- 1
    ('Placa de encendido dañada',                            25000.00,  4),  -- 2
    ('Módulo de cámara trasera defectuoso',                  15000.00,  5),  -- 3
    ('Parlante inferior dañado',                              9000.00,  6),  -- 4
    ('Módulo de cámara y flex dañados',                      11000.00,  7),  -- 5
    ('Conector USB-C dañado, se requieren 2 unidades',       13000.00,  8),  -- 6
    ('Panel de pantalla defectuoso',                         20000.00,  9),  -- 7
    ('Batería con baja capacidad y conector de carga flojo', 10000.00, 10),  -- 8
    ('Conector de carga dañado',                              8000.00, 12);  -- 9

    INSERT INTO DetalleRepuesto (IdDiagnostico, Codigo, PrecioUnitario, cantidadRequerida, CantidadUtilizada)
    VALUES
    (1, 'REP003', 18000.00, 1, 0),
    (2, 'REP004', 32000.00, 1, 0),
    (3, 'REP005', 27000.00, 1, 1),
    (4, 'REP006',  8000.00, 1, 1),
    (5, 'REP007', 35000.00, 1, 0),
    (6, 'REP008',  5000.00, 2, 0),
    (7, 'REP009', 95000.00, 1, 1),
    (8, 'REP010', 30000.00, 1, 0),
    (8, 'REP001',  4500.00, 1, 0),
    (9, 'REP001',  4500.00, 1, 1);

    -- Total = mano de obra + suma(precio unitario del detalle x cantidad requerida)
    INSERT INTO Presupuesto (TotalPresupuesto, EstadoPresupuesto, FechaRespuesta, NroOT)
    VALUES
    ( 28000.00, 'Pendiente', NULL,          3),  -- 10000 + 18000
    ( 57000.00, 'Aprobado',  '2026-10-10',  4),  -- 25000 + 32000
    ( 42000.00, 'Aprobado',  '2026-10-05',  5),  -- 15000 + 27000
    ( 17000.00, 'Aprobado',  '2026-09-15',  6),  --  9000 +  8000
    ( 46000.00, 'Rechazado', '2026-09-25',  7),  -- 11000 + 35000
    (115000.00, 'Aprobado',  '2026-05-02',  9),  -- 20000 + 95000
    ( 44500.00, 'Pendiente', NULL,         10),  -- 10000 + 30000 + 4500
    ( 12500.00, 'Aprobado',  '2026-06-02', 12);  --  8000 +  4500

    INSERT INTO ComprobanteCobro (NroOT, MontoCobrado, MedioPago, UltimosDigitos, FechaEntrega)
    VALUES
    (6,  17000.00, 'Debito',  '4532', '2026-09-20'),
    (9, 115000.00, 'Credito', '8812', '2026-05-10'),
    (12, 12500.00, 'Debito',  '7741', '2026-06-10');

    INSERT INTO Garantia (NroOT, FechaVencimiento)
    VALUES
    (6, '2026-12-19'),   -- entrega 2026-09-20 + 90 días: vigente
    (9, '2026-08-08'),   -- entrega 2026-05-10 + 90 días: vencida
    (12, '2026-09-08');  -- entrega 2026-06-10 + 90 días: vencida

    COMMIT TRANSACTION;
    PRINT 'DATOS DE NEGOCIO INSERTADOS CORRECTAMENTE';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERROR AL INSERTAR LOS DATOS DE NEGOCIO: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO
