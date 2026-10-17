using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;
using System.Text.RegularExpressions;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// ABM de equipos con baja lógica. El IMEI identifica al equipo y no se puede repetir.
    /// </summary>
    public class BLL_Equipo : IABMC<BE_Equipo>
    {
        private const string Modulo = "Equipos";

        MAP_Equipo map_equipo;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Equipo()
        {
            map_equipo = new MAP_Equipo();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Equipos activos, ordenados por marca y modelo.</summary>
        public List<BE_Equipo> Consultar()
        {
            return map_equipo.Consultar()
                .Where(e => e.Activo)
                .OrderBy(e => e.Marca).ThenBy(e => e.Modelo)
                .ToList();
        }

        /// <summary>Equipo por id, esté activo o dado de baja.</summary>
        public BE_Equipo ConsultarPorId(BE_Equipo equipo) => map_equipo.ConsultarPorId(equipo.IdEquipo)!;

        /// <summary>Equipo activo por IMEI, o null si no existe o está dado de baja (lo usa CU-NEG-1).</summary>
        public BE_Equipo? ConsultarPorImei(string imei)
        {
            BE_Equipo? equipo = map_equipo.ConsultarPorImei(imei?.Trim() ?? "");
            return equipo != null && equipo.Activo ? equipo : null;
        }

        public bool EstaDadoDeBaja(string imei)
        {
            BE_Equipo? equipo = map_equipo.ConsultarPorImei(imei?.Trim() ?? "");
            return equipo != null && !equipo.Activo;
        }

        /// <summary>
        /// Alta de equipo. Si el IMEI pertenece a un equipo dado de baja, lo reactiva con los datos nuevos.
        /// Al terminar, equipo.IdEquipo tiene el id del registro.
        /// </summary>
        public void Agregar(BE_Equipo equipo)
        {
            Normalizar(equipo);
            Validar(equipo);

            BE_Equipo? existente = map_equipo.ConsultarPorImei(equipo.Imei);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un equipo con el IMEI {equipo.Imei}.");

            if (existente != null)
            {
                equipo.IdEquipo = existente.IdEquipo;
                map_equipo.Modificar(equipo);
                map_equipo.CambiarActivo(equipo.IdEquipo, true);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaEquipo, equipo.IdEquipo);
                RegistrarBitacora("Reactivar equipo", 3);
            }
            else
            {
                equipo.IdEquipo = map_equipo.Agregar(equipo);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaEquipo, equipo.IdEquipo);
                RegistrarBitacora("Crear equipo", 2);
            }
        }

        public void Modificar(BE_Equipo equipo)
        {
            Normalizar(equipo);
            Validar(equipo);
            ObtenerActivo(equipo.IdEquipo);

            BE_Equipo? mismoImei = map_equipo.ConsultarPorImei(equipo.Imei);
            if (mismoImei != null && mismoImei.IdEquipo != equipo.IdEquipo)
                throw new Exception($"El IMEI {equipo.Imei} ya pertenece a otro equipo.");

            map_equipo.Modificar(equipo);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaEquipo, equipo.IdEquipo);
            RegistrarBitacora("Modificar equipo", 2);
        }

        /// <summary>Baja lógica. No se permite si el equipo tiene órdenes de trabajo sin entregar.</summary>
        public void Borrar(BE_Equipo equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo.");
            BE_Equipo existente = ObtenerActivo(equipo.IdEquipo);

            int otAbiertas = map_equipo.ContarOT(existente.IdEquipo, EstadosOT.Abiertas);
            if (otAbiertas > 0)
                throw new Exception($"No se puede eliminar el equipo: tiene {otAbiertas} orden(es) de trabajo sin entregar.");

            map_equipo.CambiarActivo(existente.IdEquipo, false);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaEquipo, existente.IdEquipo);
            RegistrarBitacora("Eliminar equipo", 3);
        }

        private BE_Equipo ObtenerActivo(int idEquipo)
        {
            BE_Equipo? equipo = map_equipo.ConsultarPorId(idEquipo);
            if (equipo == null || !equipo.Activo)
                throw new Exception("El equipo no existe o fue dado de baja.");
            return equipo;
        }

        private void Normalizar(BE_Equipo equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo.");
            equipo.Marca = equipo.Marca?.Trim() ?? "";
            equipo.Modelo = equipo.Modelo?.Trim() ?? "";
            equipo.Imei = equipo.Imei?.Trim() ?? "";
        }

        private void Validar(BE_Equipo equipo)
        {
            if (string.IsNullOrWhiteSpace(equipo.Marca))
                throw new Exception("La marca es obligatoria.");

            if (equipo.Marca.Length > 50)
                throw new Exception("La marca no puede superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(equipo.Modelo))
                throw new Exception("El modelo es obligatorio.");

            if (equipo.Modelo.Length > 50)
                throw new Exception("El modelo no puede superar los 50 caracteres.");

            if (!Regex.IsMatch(equipo.Imei, @"^\d{15}$"))
                throw new Exception("El IMEI debe tener 15 dígitos numéricos.");
        }

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
