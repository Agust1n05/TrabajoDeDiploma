using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;
using System.Text.RegularExpressions;

namespace BLL.BLL_NEG
{
    /// <summary>El DNI ingresado en la recepción no pertenece a un cliente activo (se extiende a CU-NEG-6).</summary>
    public class ClienteNoRegistradoException : Exception
    {
        public string Dni { get; }

        public ClienteNoRegistradoException(string dni)
            : base($"El DNI {dni} no está registrado. Debe registrar al cliente.")
        {
            Dni = dni;
        }
    }

    /// <summary>
    /// Lógica de las órdenes de trabajo de PN-01.
    /// CU-NEG-1 Gestionar recepción de equipo: RecepcionarEquipo. (CU-NEG-4 está en BLL_Diagnostico.)
    /// </summary>
    public class BLL_OT
    {
        private const string Modulo = "Órdenes de trabajo";
        public const int LargoMaximoFalla = 200;

        MAP_OT map_ot;
        BLL_Cliente bll_cliente;
        BLL_Equipo bll_equipo;
        BLL_Tecnico bll_tecnico;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_OT()
        {
            map_ot = new MAP_OT();
            bll_cliente = new BLL_Cliente();
            bll_equipo = new BLL_Equipo();
            bll_tecnico = new BLL_Tecnico();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Todas las OT con cliente, equipo, técnico y presupuesto, de la más nueva a la más vieja.</summary>
        public List<BE_OTResumen> ConsultarResumen() => map_ot.ConsultarResumen();

        public BE_OTResumen? ConsultarResumenPorNro(int nroOT) => map_ot.ConsultarResumenPorNro(nroOT);

        /// <summary>
        /// OT que puede ver el usuario logueado: si es un técnico, solo las asignadas a él
        /// (ninguna si no está registrado como técnico activo); si no (vendedor, administrador), todas.
        /// </summary>
        public List<BE_OTResumen> ConsultarResumenSegunUsuario()
        {
            List<BE_OTResumen> ordenes = map_ot.ConsultarResumen();
            if (!bll_tecnico.SesionEsTecnico()) return ordenes;

            BE_Tecnico? tecnico = bll_tecnico.ConsultarTecnicoDeSesion();
            return tecnico == null ? new List<BE_OTResumen>() : ordenes.Where(o => o.DniTecnico == tecnico.Dni).ToList();
        }

        /// <summary>True si el usuario logueado tiene el rol Tecnico.</summary>
        public bool UsuarioEsTecnico() => bll_tecnico.SesionEsTecnico();

        /// <summary>
        /// OT sobre la que va a trabajar el técnico (CU-NEG-4 y CU-NEG-5). Tiene que existir y, si el usuario
        /// logueado es un técnico, estar asignada a él. Un usuario con permiso que no es técnico
        /// (ej. el administrador) puede trabajar sobre cualquier OT.
        /// </summary>
        public BE_OTResumen ObtenerOTAccesible(int nroOT)
        {
            BE_OTResumen ot = map_ot.ConsultarResumenPorNro(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (bll_tecnico.SesionEsTecnico())
            {
                BE_Tecnico tecnicoSesion = bll_tecnico.ConsultarTecnicoDeSesion()
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
        public int ObtenerOTAbiertaDelEquipo(int idEquipo)
        {
            return map_ot.ObtenerOTDelEquipo(idEquipo, EstadosOT.Abiertas);
        }

        /// <summary>
        /// CU-NEG-1 Gestionar recepción de equipo.
        /// Valida todo antes de escribir en la base, así una validación que falla no deja datos a medias:
        ///   3. El cliente tiene que estar registrado y activo; si no, ClienteNoRegistradoException (CU-NEG-6).
        ///   5. Falla obligatoria y IMEI de 15 dígitos. Si el equipo ya existe, no puede tener una OT sin entregar.
        ///   7. Se busca el técnico disponible con menos OT en curso (si no hay, no se recibe el equipo).
        /// Después registra el equipo si es nuevo (o lo reactiva si estaba dado de baja),
        ///   6. genera la OT en estado "Recibida" asignada a ese técnico, y actualiza DV y bitácora.
        /// </summary>
        /// <param name="marca">Solo se usa si el IMEI no está registrado.</param>
        /// <param name="modelo">Solo se usa si el IMEI no está registrado.</param>
        /// <returns>La OT creada, con los datos para mostrar la confirmación (paso 8).</returns>
        public BE_OTResumen RecepcionarEquipo(string dni, string imei, string marca, string modelo, string descripcionFalla)
        {
            dni = dni?.Trim() ?? "";
            imei = imei?.Trim() ?? "";
            descripcionFalla = descripcionFalla?.Trim() ?? "";

            // Paso 3: cliente registrado
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe ingresar el DNI del cliente.");

            BE_Cliente cliente = bll_cliente.ConsultarPorDni(dni)
                ?? throw new ClienteNoRegistradoException(dni);

            // Paso 5: campos obligatorios y formato
            if (!Regex.IsMatch(imei, @"^\d{15}$"))
                throw new Exception("El IMEI debe tener 15 dígitos numéricos.");

            if (string.IsNullOrWhiteSpace(descripcionFalla))
                throw new Exception("La descripción de la falla es obligatoria.");

            if (descripcionFalla.Length > LargoMaximoFalla)
                throw new Exception($"La descripción de la falla no puede superar los {LargoMaximoFalla} caracteres.");

            BE_Equipo? equipo = bll_equipo.ConsultarPorImei(imei);
            if (equipo != null)
            {
                int otAbierta = map_ot.ObtenerOTDelEquipo(equipo.IdEquipo, EstadosOT.Abiertas);
                if (otAbierta > 0)
                    throw new Exception($"El equipo ya tiene la OT N° {otAbierta} sin entregar. No se puede recibir de nuevo.");
            }

            // Paso 7 (se resuelve antes de escribir): técnico disponible con menos OT en curso
            BE_Tecnico tecnico = bll_tecnico.AsignarTecnico();

            // Equipo nuevo, o dado de baja que se reactiva (BLL_Equipo valida marca y modelo)
            if (equipo == null)
            {
                equipo = new BE_Equipo(0, marca, modelo, imei);
                bll_equipo.Agregar(equipo);
            }

            // Paso 6: OT con número correlativo y estado "Recibida"
            var ot = new BE_OT(0, EstadosOT.Recibida, cliente.Dni, equipo.IdEquipo, tecnico.Dni, descripcionFalla);
            ot.NroOT = map_ot.Agregar(ot);

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, ot.NroOT);
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, "Recepcionar equipo", 2));

            return map_ot.ConsultarResumenPorNro(ot.NroOT)
                ?? throw new Exception($"La OT N° {ot.NroOT} se creó pero no se pudo consultar.");
        }
    }
}
