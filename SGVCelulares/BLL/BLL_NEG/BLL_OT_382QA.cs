using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;
using System.Text.RegularExpressions;

namespace BLL.BLL_NEG
{
    /// <summary>El DNI ingresado en la recepción no pertenece a un cliente activo (se extiende a CU-NEG-6).</summary>
    public class ClienteNoRegistradoException_382QA : Exception
    {
        public string Dni { get; }

        public ClienteNoRegistradoException_382QA(string dni)
            : base($"El DNI {dni} no está registrado. Debe registrar al cliente.")
        {
            Dni = dni;
        }
    }

    /// <summary>
    /// Lógica de las órdenes de trabajo de PN-01.
    /// CU-NEG-1 Gestionar recepción de equipo: RecepcionarEquipo. (CU-NEG-4 está en BLL_Diagnostico_382QA.)
    /// </summary>
    public class BLL_OT_382QA
    {
        private const string Modulo = "Órdenes de trabajo";
        public const int LargoMaximoFalla = 200;

        MAP_OT_382QA map_ot;
        MAP_Reclamo_382QA map_reclamo;
        BLL_Cliente_382QA bll_cliente;
        BLL_Equipo_382QA bll_equipo;
        BLL_Tecnico_382QA bll_tecnico;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_OT_382QA()
        {
            map_ot = new MAP_OT_382QA();
            map_reclamo = new MAP_Reclamo_382QA();
            bll_cliente = new BLL_Cliente_382QA();
            bll_equipo = new BLL_Equipo_382QA();
            bll_tecnico = new BLL_Tecnico_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Todas las OT con cliente, equipo, técnico y presupuesto, de la más nueva a la más vieja.</summary>
        public List<BE_OTResumen_382QA> ConsultarResumen_382QA() => map_ot.ConsultarResumen_382QA();

        public BE_OTResumen_382QA? ConsultarResumenPorNro_382QA(int nroOT) => map_ot.ConsultarResumenPorNro_382QA(nroOT);

        /// <summary>
        /// OT que puede ver el usuario logueado: si es un técnico, solo las asignadas a él
        /// (ninguna si no está registrado como técnico activo); si no (vendedor, administrador), todas.
        /// </summary>
        public List<BE_OTResumen_382QA> ConsultarResumenSegunUsuario_382QA()
        {
            List<BE_OTResumen_382QA> ordenes = map_ot.ConsultarResumen_382QA();
            if (!bll_tecnico.SesionEsTecnico_382QA()) return ordenes;

            BE_Tecnico_382QA? tecnico = bll_tecnico.ConsultarTecnicoDeSesion_382QA();
            return tecnico == null ? new List<BE_OTResumen_382QA>() : ordenes.Where(o => o.DniTecnico == tecnico.Dni).ToList();
        }

        /// <summary>True si el usuario logueado tiene el rol Tecnico.</summary>
        public bool UsuarioEsTecnico_382QA() => bll_tecnico.SesionEsTecnico_382QA();

        /// <summary>
        /// OT sobre la que va a trabajar el técnico (CU-NEG-4 y CU-NEG-5). Tiene que existir y, si el usuario
        /// logueado es un técnico, estar asignada a él. Un usuario con permiso que no es técnico
        /// (ej. el administrador) puede trabajar sobre cualquier OT.
        /// </summary>
        public BE_OTResumen_382QA ObtenerOTAccesible_382QA(int nroOT)
        {
            BE_OTResumen_382QA ot = map_ot.ConsultarResumenPorNro_382QA(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (bll_tecnico.SesionEsTecnico_382QA())
            {
                BE_Tecnico_382QA tecnicoSesion = bll_tecnico.ConsultarTecnicoDeSesion_382QA()
                    ?? throw new Exception("Su usuario no está registrado como técnico activo.");
                if (ot.DniTecnico != tecnicoSesion.Dni)
                    throw new Exception($"La OT N° {nroOT} está asignada a otro técnico.");
            }

            return ot;
        }

        /// <summary>
        /// N° de la OT sin entregar que tiene el equipo, o 0 si no tiene ninguna.
        /// Se llama solo con un equipo ya registrado. Permite avisar en la recepción apenas se busca el equipo.
        /// </summary>
        public int ObtenerOTAbiertaDelEquipo_382QA(int idEquipo)
        {
            return map_ot.ObtenerOTDelEquipo_382QA(idEquipo, EstadosOT_382QA.Abiertas);
        }

        /// <summary>
        /// CU-NEG-1 Gestionar recepción de equipo.
        /// Valida todo antes de escribir en la base, así una validación que falla no deja datos a medias:
        ///   3. El cliente tiene que estar registrado y activo; si no, ClienteNoRegistradoException_382QA (CU-NEG-6).
        ///   5. Falla obligatoria y IMEI de 15 dígitos. Si el equipo ya existe, no puede tener una OT sin entregar
        ///      ni un reclamo por garantía abierto (PN-02).
        ///   7. Se busca el técnico disponible con menos OT en curso (si no hay, no se recibe el equipo).
        /// Después registra el equipo si es nuevo (o lo reactiva si estaba dado de baja),
        ///   6. genera la OT en estado "Recibida" asignada a ese técnico, y actualiza DV y bitácora.
        /// </summary>
        /// <param name="marca">Solo se usa si el IMEI no está registrado.</param>
        /// <param name="modelo">Solo se usa si el IMEI no está registrado.</param>
        /// <returns>La OT creada, con los datos para mostrar la confirmación (paso 8).</returns>
        public BE_OTResumen_382QA RecepcionarEquipo_382QA(string dni, string imei, string marca, string modelo, string descripcionFalla)
        {
            dni = dni?.Trim() ?? "";
            imei = imei?.Trim() ?? "";
            descripcionFalla = descripcionFalla?.Trim() ?? "";

            // Paso 3: cliente registrado
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe ingresar el DNI del cliente.");

            BE_Cliente_382QA cliente = bll_cliente.ConsultarPorDni_382QA(dni)
                ?? throw new ClienteNoRegistradoException_382QA(dni);

            // Paso 5: campos obligatorios y formato
            if (!Regex.IsMatch(imei, @"^\d{15}$"))
                throw new Exception("El IMEI debe tener 15 dígitos numéricos.");

            if (string.IsNullOrWhiteSpace(descripcionFalla))
                throw new Exception("La descripción de la falla es obligatoria.");

            if (descripcionFalla.Length > LargoMaximoFalla)
                throw new Exception($"La descripción de la falla no puede superar los {LargoMaximoFalla} caracteres.");

            BE_Equipo_382QA? equipo = bll_equipo.ConsultarPorImei_382QA(imei);
            if (equipo != null)
            {
                int otAbierta = map_ot.ObtenerOTDelEquipo_382QA(equipo.IdEquipo, EstadosOT_382QA.Abiertas);
                if (otAbierta > 0)
                    throw new Exception($"El equipo ya tiene la OT N° {otAbierta} sin entregar. No se puede recibir de nuevo.");

                int reclamoAbierto = map_reclamo.ObtenerReclamoDelEquipo_382QA(equipo.IdEquipo, EstadosReclamo_382QA.Abiertos);
                if (reclamoAbierto > 0)
                    throw new Exception($"El equipo tiene el reclamo por garantía N° {reclamoAbierto} abierto. No se puede recibir de nuevo.");
            }

            // Paso 7 (se resuelve antes de escribir): técnico disponible con menos OT en curso
            BE_Tecnico_382QA tecnico = bll_tecnico.AsignarTecnico_382QA();

            // Equipo nuevo, o dado de baja que se reactiva (BLL_Equipo_382QA valida marca y modelo)
            if (equipo == null)
            {
                equipo = new BE_Equipo_382QA(0, marca, modelo, imei);
                bll_equipo.Agregar_382QA(equipo);
            }

            // Paso 6: OT con número correlativo y estado "Recibida"
            var ot = new BE_OT_382QA(0, EstadosOT_382QA.Recibida, cliente.Dni, equipo.IdEquipo, tecnico.Dni, descripcionFalla);
            ot.NroOT = map_ot.Agregar_382QA(ot);

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, ot.NroOT);
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, "Recepcionar equipo", 2));

            return map_ot.ConsultarResumenPorNro_382QA(ot.NroOT)
                ?? throw new Exception($"La OT N° {ot.NroOT} se creó pero no se pudo consultar.");
        }
    }
}
