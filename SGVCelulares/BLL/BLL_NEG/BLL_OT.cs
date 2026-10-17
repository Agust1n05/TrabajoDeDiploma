using BE;
using BLL.BLL_NEG;
using MAP.MAP_NEG;
using MAP.MAP_SER;
using Servicio;
using System.Linq;

namespace BLL.BLL_SER
{
    public class ClienteNoRegistradoException : Exception
    {
        public ClienteNoRegistradoException() : base("El DNI no está registrado. Debe registrar al cliente.") { }
    }

    public class BLL_OT
    {
        MAP_OT map_ot;
        BLL_Cliente bll_cliente;
        BLL_Equipo bll_equipo;
        BLL_Tecnico bll_tecnico;
        BLL_Bitacora bll_bitacora;

        public BLL_OT()
        {
            map_ot = new MAP_OT();
            bll_cliente = new BLL_Cliente();
            bll_equipo = new BLL_Equipo();
            bll_tecnico = new BLL_Tecnico();
            bll_bitacora = new BLL_Bitacora();
        }

        public List<BE_OT> Consultar() => map_ot.Consultar();

        // estadoFiltro null/vacío = trae todas las OT sin filtrar
        public List<BE_OT> ConsultarParaGrilla(string estadoFiltro = null)
        {
            var consulta = from ot in Consultar()
                           where string.IsNullOrEmpty(estadoFiltro) || ot.Estado == estadoFiltro
                           select new BE_OT
                           {
                               NroOT = ot.NroOT,
                               Estado = ot.Estado,
                               Dni = ot.Dni,
                               IdEquipo = ot.IdEquipo,
                               DniTecnico = ot.DniTecnico
                           };
            return consulta.ToList();
        }

        // CU-NEG-1: Gestionar recepción de equipo (pasos 2 a 8)
        public BE_OT RecibirEquipo(string dni, string marca, string modelo, string imei, string descripcionFalla)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe ingresar el DNI del cliente.");

           
            if (!bll_cliente.ExisteCliente(dni))
                throw new ClienteNoRegistradoException();

         
            // TODO Fase 2 (CU-NEG-1): este método se reescribe. Por ahora solo se adapta al nuevo
            // maestro de equipos (se reutiliza por IMEI); la falla todavía no se guarda en la OT.
            BE_Equipo equipo = bll_equipo.ConsultarPorImei(imei) ?? new BE_Equipo(0, marca, modelo, imei);
            if (equipo.IdEquipo == 0) bll_equipo.Agregar(equipo);

            BE_Tecnico tecnico = bll_tecnico.AsignarTecnico();

          
            BE_OT ot = new BE_OT(0, EstadosOT.Recibida, dni, equipo.IdEquipo.ToString(), tecnico.Dni);
            ot.NroOT = map_ot.Agregar(ot);

            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "OT",
                $"$\"Recepción OT N°{{ot.NroOT}} - Téc. {{tecnico.Dni}}\"", 2));

            return ot;
        }
    }
}