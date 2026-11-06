/* ============================================================
   DATOS DE PRUEBA - SGVCelulares (completa la base para probar RF01, RF02, maestros y seguridad)
   SQL SERVER - base bd_sgvcelulares

   Orden de ejecución, sobre una base recién creada:
     1. script_sgvcelulares.sql   (seguridad: crea/limpia la base)
     2. ScriptNegocio.sql         (negocio: OT 1 a 12, equipos 1 a 11)
     3. DatosPrueba.sql           (este script: OT 13 a 30, reclamos 1 a 8)

   Los números de OT, reclamo, equipo, diagnóstico, presupuesto y comprobante se insertan
   explícitos para que coincidan con la guía de pruebas. Si la base no está recién creada,
   el script no hace nada.

   IMPORTANTE: los registros insertados por script no tienen dígito verificador.
   En el primer login, el administrador ve la inconsistencia y debe elegir "Recalcular DV".

   Contraseñas de los usuarios nuevos: DNI + Apellido (igual que ScriptNegocio.sql).
   ============================================================ */

USE bd_sgvcelulares;
GO

IF EXISTS (SELECT 1 FROM Reclamo)
   OR (SELECT COUNT(*) FROM OrdenDeTrabajo) <> 12 OR (SELECT MAX(NroOT) FROM OrdenDeTrabajo) <> 12
   OR (SELECT COUNT(*) FROM Equipo) <> 11
   OR EXISTS (SELECT 1 FROM Cliente WHERE dni = '46333555')
BEGIN
    RAISERROR('DatosPrueba.sql: la base no está recién creada (correr antes script_sgvcelulares.sql y ScriptNegocio.sql). No se insertó nada.', 16, 1);
    SET NOEXEC ON;
END
GO

BEGIN TRANSACTION;
BEGIN TRY

    /* ---------- Usuarios: contraseña inicial = DNI + Apellido (SHA-256) ---------- */
    INSERT INTO usuario (dni, nombre, apellido, correo, nombreUsuario, password, bloqueo, activo, cantIntentos, idRol, idIdioma)
    SELECT v.dni, v.nombre, v.apellido, v.correo, v.apellido + v.dni,
           LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', v.dni + v.apellido), 2)),
           v.bloqueo, v.activo, v.intentos, ro.idRol, 'es-AR'
    FROM (VALUES
        ('36111444', 'Martina', 'Quiroga', 'mquiroga@santsung.com', 'Vendedor', 1, 1, 3),  -- Bloqueado: probar Desbloquear usuario
        ('29888777', 'Gustavo', 'Ibarra', 'gibarra@santsung.com', 'Vendedor', 0, 0, 0),  -- Desactivado: probar Activar usuario
        ('38222999', 'Ignacio', 'Ruiz', 'iruiz@santsung.com', 'Tecnico', 0, 1, 0),  -- Rol Técnico sin alta como técnico: probar alta en Gestión de Técnicos
        ('31555888', 'Julian', 'Sosa', 'jsosa@santsung.com', 'Tecnico', 0, 1, 0)   -- Técnico dado de baja: probar reactivarlo
    ) AS v(dni, nombre, apellido, correo, rol, bloqueo, activo, intentos)
    INNER JOIN rol ro ON ro.nombre = v.rol;

    INSERT INTO Tecnico (Dni, Nombre, Apellido, Disponible, Activo)
    VALUES ('31555888', 'Julian', 'Sosa', 1, 0);  -- dado de baja

    /* ---------- Clientes ---------- */
    INSERT INTO Cliente (dni, Nombre, Apellido, Telefono, Activo)
    VALUES
    ('46333555', 'Florencia', 'Acosta', '1155443322', 1),
    ('47222888', 'Joaquín', 'Benítez', '1166778899', 0),
    ('39555222', 'Rocío', 'Medina', '1133221100', 1),
    ('41222777', 'Facundo', 'Ortiz', '1144556677', 1),
    ('44888111', 'Paula', 'Giménez', '1177889900', 1),
    ('40777333', 'Ezequiel', 'Molina', '1122446688', 1),
    ('42999666', 'Milagros', 'Rojas', '1188664422', 1),
    ('38444777', 'Santiago', 'Luna', '1199112233', 1),
    ('45111333', 'Brenda', 'Silva', '1133557799', 1),
    ('37999222', 'Gonzalo', 'Peralta', '1144668800', 1),
    ('43666888', 'Micaela', 'Ríos', '1155779911', 1),
    ('40222555', 'Leandro', 'Ferreyra', '1166880022', 1),
    ('46999111', 'Tamara', 'Vázquez', '1177991133', 1);

    /* ---------- Repuestos ---------- */
    INSERT INTO Repuesto (Codigo, Descripcion, PrecioUnitario, stockDisponible, Activo)
    VALUES
    ('REP013', 'Batería Samsung Galaxy A54', 25000.00, 0, 1),
    ('REP014', 'Módulo de carga Motorola Edge 40', 12000.00, 1, 1),
    ('REP015', 'Lente de cámara iPhone 13', 9000.00, 4, 0),
    ('REP016', 'Pantalla Xiaomi Redmi Note 12', 70000.00, 3, 1);

    /* ---------- Equipos (30: sin OT, para recepcionar un IMEI existente; 31: dado de baja) ---------- */
    SET IDENTITY_INSERT Equipo ON;
    INSERT INTO Equipo (idEquipo, marca, modelo, imei, Activo)
    VALUES
    (12, 'Samsung', 'Galaxy A15', '351900000000012', 1),
    (13, 'Xiaomi', 'Redmi Note 12', '351900000000013', 1),
    (14, 'Xiaomi', 'Poco X5 Pro', '351900000000014', 1),
    (15, 'Motorola', 'Moto G84', '351900000000015', 1),
    (16, 'Apple', 'iPhone 12', '351900000000016', 1),
    (17, 'Samsung', 'Galaxy S23', '351900000000017', 1),
    (18, 'Samsung', 'Galaxy A54', '351900000000018', 1),
    (19, 'Apple', 'iPhone 14', '351900000000019', 1),
    (20, 'Xiaomi', 'Poco X5 Pro', '351900000000020', 1),
    (21, 'Motorola', 'Edge 40', '351900000000021', 1),
    (22, 'Samsung', 'Galaxy S22', '351900000000022', 1),
    (23, 'Apple', 'iPhone 13', '351900000000023', 1),
    (24, 'Motorola', 'Moto G84', '351900000000024', 1),
    (25, 'Samsung', 'Galaxy A54', '351900000000025', 1),
    (26, 'Apple', 'iPhone 12', '351900000000026', 1),
    (27, 'Samsung', 'Galaxy A34', '351900000000027', 1),
    (28, 'Xiaomi', 'Redmi Note 12', '351900000000028', 1),
    (29, 'Samsung', 'Galaxy S23', '351900000000029', 1),
    (30, 'Samsung', 'Galaxy A15', '351900000000030', 1),
    (31, 'Apple', 'iPhone 11', '351900000000031', 0);
    SET IDENTITY_INSERT Equipo OFF;

    /* ---------- Órdenes de trabajo ---------- */
    SET IDENTITY_INSERT OrdenDeTrabajo ON;
    INSERT INTO OrdenDeTrabajo (NroOT, Estado, dni, idEquipo, DniTecnico, DescripcionFalla)
    VALUES
    (13, 'Recibida', '39555222', 12, '35666777', 'Se reinicia solo'),  -- Diagnosticar (iniciar)
    (14, 'En diagnostico', '41222777', 13, '30111222', 'Pantalla con manchas'),  -- Diagnosticar con stock (REP016) → Presupuestado
    (15, 'En diagnostico', '44888111', 14, '33999888', 'No carga, el conector está flojo'),  -- Diagnosticar sin stock (REP008 = 0) → Sin reparar
    (16, 'Presupuestado', '40777333', 15, '35666777', 'La batería está hinchada'),  -- Presupuesto: aprobar
    (17, 'Presupuestado', '42999666', 16, '33999888', 'No se escucha el parlante'),  -- Presupuesto: rechazar
    (18, 'En reparacion', '38444777', 17, '30111222', 'No carga'),  -- Reparar con stock → Reparado
    (19, 'En reparacion', '45111333', 18, '32444555', 'La batería dura muy poco'),  -- Reparar sin stock (REP013 = 0): avisa y la OT sigue En reparacion
    (20, 'Reparado', '37999222', 19, '32444555', 'La batería está al 70%'),  -- Entregar con cobro
    (21, 'Sin reparar', '43666888', 20, '33999888', 'No reconoce el cargador'),  -- Entregar sin reparar
    (22, 'Entregado', '40222555', 21, '30111222', 'La cámara no enfoca'),  -- Registrar reclamo (garantía vigente)
    (23, 'Entregado', '42333444', 22, '35666777', 'Pantalla rota'),  -- Reclamo 1
    (24, 'Entregado', '36777888', 23, '30111222', 'Pantalla rota'),  -- Reclamo 2
    (25, 'Entregado', '44555666', 24, '32444555', 'La batería dura muy poco'),  -- Reclamo 3
    (26, 'Entregado', '46999111', 25, '33999888', 'La batería dura muy poco'),  -- Reclamo 4
    (27, 'Entregado', '38987654', 26, '35666777', 'No se escucha el audio'),  -- Reclamo 5
    (28, 'Entregado', '39222111', 27, '28777666', 'No carga'),  -- Reclamo 6
    (29, 'Entregado', '45666777', 28, '32444555', 'Pantalla con líneas'),  -- Reclamo 7
    (30, 'Entregado', '43111999', 29, '30111222', 'No carga');  -- Reclamo 8 (cerrado): se puede registrar otro
    SET IDENTITY_INSERT OrdenDeTrabajo OFF;

    /* ---------- Diagnósticos ---------- */
    SET IDENTITY_INSERT Diagnostico ON;
    INSERT INTO Diagnostico (IdDiagnostico, detalle, costo_mano_obra, NroOT)
    VALUES
    (10, 'Batería hinchada, hay que reemplazarla', 10000.00, 16),
    (11, 'Parlante inferior dañado', 9000.00, 17),
    (12, 'Flex de carga dañado', 9000.00, 18),
    (13, 'Batería agotada', 8000.00, 19),
    (14, 'Batería con poca capacidad', 10000.00, 20),
    (15, 'Conector USB-C dañado', 8000.00, 21),
    (16, 'Módulo de cámara defectuoso', 12000.00, 22),
    (17, 'Panel de pantalla roto', 20000.00, 23),
    (18, 'Pantalla rota por caída', 18000.00, 24),
    (19, 'Batería deteriorada', 10000.00, 25),
    (20, 'Batería agotada', 8000.00, 26),
    (21, 'Parlante inferior dañado', 9000.00, 27),
    (22, 'Conector de carga dañado', 8000.00, 28),
    (23, 'Panel de pantalla defectuoso', 15000.00, 29),
    (24, 'Flex de carga dañado', 9000.00, 30);
    SET IDENTITY_INSERT Diagnostico OFF;

    /* ---------- Repuestos de cada diagnóstico ---------- */
    INSERT INTO DetalleRepuesto (IdDiagnostico, Codigo, PrecioUnitario, cantidadRequerida, CantidadUtilizada)
    VALUES
    (10, 'REP003', 18000.00, 1, 0),
    (11, 'REP006', 8000.00, 1, 0),
    (12, 'REP011', 7000.00, 1, 0),
    (13, 'REP013', 25000.00, 1, 0),
    (14, 'REP010', 30000.00, 1, 1),
    (15, 'REP008', 5000.00, 1, 0),
    (16, 'REP007', 35000.00, 1, 1),
    (17, 'REP009', 95000.00, 1, 1),
    (18, 'REP002', 85000.00, 1, 1),
    (19, 'REP003', 18000.00, 1, 1),
    (20, 'REP013', 25000.00, 1, 1),
    (21, 'REP006', 8000.00, 1, 1),
    (22, 'REP001', 4500.00, 1, 1),
    (23, 'REP016', 70000.00, 1, 1),
    (24, 'REP011', 7000.00, 1, 1);

    /* ---------- Presupuestos (total = mano de obra + repuestos) ---------- */
    SET IDENTITY_INSERT Presupuesto ON;
    INSERT INTO Presupuesto (IdPresupuesto, TotalPresupuesto, EstadoPresupuesto, FechaRespuesta, NroOT)
    VALUES
    (9, 28000.00, 'Pendiente', NULL, 16),
    (10, 17000.00, 'Pendiente', NULL, 17),
    (11, 16000.00, 'Aprobado', '2026-11-02', 18),
    (12, 33000.00, 'Aprobado', '2026-11-01', 19),
    (13, 40000.00, 'Aprobado', '2026-10-28', 20),
    (14, 47000.00, 'Aprobado', '2026-10-15', 22),
    (15, 115000.00, 'Aprobado', '2026-09-28', 23),
    (16, 103000.00, 'Aprobado', '2026-09-18', 24),
    (17, 28000.00, 'Aprobado', '2026-09-24', 25),
    (18, 33000.00, 'Aprobado', '2026-09-23', 26),
    (19, 17000.00, 'Aprobado', '2026-09-08', 27),
    (20, 12500.00, 'Aprobado', '2026-05-10', 28),
    (21, 85000.00, 'Aprobado', '2026-10-03', 29),
    (22, 16000.00, 'Aprobado', '2026-08-29', 30);
    SET IDENTITY_INSERT Presupuesto OFF;

    /* ---------- Comprobantes de cobro ---------- */
    SET IDENTITY_INSERT ComprobanteCobro ON;
    INSERT INTO ComprobanteCobro (NroComprobante, NroOT, MontoCobrado, MedioPago, UltimosDigitos, FechaEntrega)
    VALUES
    (4, 22, 47000.00, 'Credito', '1881', '2026-10-22'),
    (5, 23, 115000.00, 'Debito', '4417', '2026-10-05'),
    (6, 24, 103000.00, 'Credito', '9032', '2026-09-25'),
    (7, 25, 28000.00, 'Debito', '2290', '2026-10-01'),
    (8, 26, 33000.00, 'Credito', '6614', '2026-09-30'),
    (9, 27, 17000.00, 'Debito', '7350', '2026-09-15'),
    (10, 28, 12500.00, 'Debito', '1204', '2026-05-17'),
    (11, 29, 85000.00, 'Credito', '5528', '2026-10-10'),
    (12, 30, 16000.00, 'Debito', '3361', '2026-09-05');
    SET IDENTITY_INSERT ComprobanteCobro OFF;

    /* ---------- Garantías (vencen 90 días después de la entrega) ---------- */
    INSERT INTO Garantia (NroOT, FechaVencimiento)
    VALUES
    (22, '2027-01-20'),   -- entrega 2026-10-22 + 90 días: vigente
    (23, '2027-01-03'),   -- entrega 2026-10-05 + 90 días: vigente
    (24, '2026-12-24'),   -- entrega 2026-09-25 + 90 días: vigente
    (25, '2026-12-30'),   -- entrega 2026-10-01 + 90 días: vigente
    (26, '2026-12-29'),   -- entrega 2026-09-30 + 90 días: vigente
    (27, '2026-12-14'),   -- entrega 2026-09-15 + 90 días: vigente
    (28, '2026-08-15'),   -- entrega 2026-05-17 + 90 días: VENCIDA
    (29, '2027-01-08'),   -- entrega 2026-10-10 + 90 días: vigente
    (30, '2026-12-04');   -- entrega 2026-09-05 + 90 días: vigente

    /* ---------- Reclamos por garantía ---------- */
    SET IDENTITY_INSERT Reclamo ON;
    INSERT INTO Reclamo (NroReclamo, NroOT, Motivo, Descripcion, FechaReclamo, Estado, DniTecnico, FallaCubierta, DetalleEvaluacion)
    VALUES
    (1, 23, 'La pantalla parpadea', 'Desde hace una semana la pantalla nueva parpadea con el brillo bajo.', '2026-11-03', 'En evaluacion', '35666777', NULL, NULL),  -- Evaluar: aprobar (Mariano)
    (2, 24, 'El táctil no responde en una zona', 'La parte inferior de la pantalla no responde al tacto.', '2026-11-02', 'En evaluacion', '30111222', NULL, NULL),  -- Evaluar: rechazar, tiene un golpe (Roberto)
    (3, 25, 'La batería se vuelve a descargar rápido', 'La batería reemplazada dura menos de medio día.', '2026-10-29', 'Aprobado', '32444555', 1, 'Batería reemplazada con defecto de fábrica. Falla cubierta por la garantía.'),  -- Reparar con stock (REP003) → Reparado (Federico)
    (4, 26, 'La batería se hinchó', 'La tapa trasera se levantó por la batería hinchada.', '2026-10-27', 'En espera de repuesto', '33999888', 1, 'Batería defectuosa. Falla cubierta; falta el repuesto REP013.'),  -- Cargar stock de REP013 y reparar (Pablo)
    (5, 27, 'El parlante volvió a fallar', 'El sonido sale distorsionado en las llamadas.', '2026-10-20', 'Reparado', '35666777', 1, 'Parlante reemplazado defectuoso. Falla cubierta.'),  -- Entregar → Resuelto
    (6, 28, 'No carga otra vez', 'El conector de carga dejó de funcionar.', '2026-11-04', 'Rechazado', NULL, NULL, NULL),  -- Entregar sin reparar (garantía vencida)
    (7, 29, 'Aparecieron manchas en la pantalla', 'Manchas oscuras en la pantalla nueva.', '2026-10-30', 'Rechazado', '32444555', 0, 'Ingreso de humedad en el equipo: el daño no está cubierto por la garantía.'),  -- Entregar sin reparar (falla no cubierta)
    (8, 30, 'Falso contacto en la carga', 'El cable tiene que quedar en una posición para cargar.', '2026-09-25', 'Resuelto', '30111222', 1, 'Flex de carga reemplazado con falla. Falla cubierta.');  -- Historial (reclamo cerrado)
    SET IDENTITY_INSERT Reclamo OFF;

    INSERT INTO DetalleRepuestoReclamo (NroReclamo, Codigo, CantidadUtilizada)
    VALUES (5, 'REP006', 1),
           (8, 'REP011', 1);

    COMMIT TRANSACTION;
    PRINT 'DATOS DE PRUEBA INSERTADOS: OT 13 a 30, reclamos 1 a 8. Entrar con admin y elegir "Recalcular DV".';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    PRINT 'ERROR AL INSERTAR LOS DATOS DE PRUEBA: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO

SET NOEXEC OFF;
GO
