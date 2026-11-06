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
    public class BLL_Equipo_382QA : IABMC_382QA<BE_Equipo_382QA>
    {
        private const string Modulo = "Equipos";

        MAP_Equipo_382QA map_equipo;
        MAP_Reclamo_382QA map_reclamo;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Equipo_382QA()
        {
            map_equipo = new MAP_Equipo_382QA();
            map_reclamo = new MAP_Reclamo_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Equipos activos, ordenados por marca y modelo.</summary>
        public List<BE_Equipo_382QA> Consultar_382QA()
        {
            return map_equipo.Consultar_382QA()
                .Where(e => e.Activo)
                .OrderBy(e => e.Marca).ThenBy(e => e.Modelo)
                .ToList();
        }

        /// <summary>Equipo por id, esté activo o dado de baja.</summary>
        public BE_Equipo_382QA ConsultarPorId_382QA(BE_Equipo_382QA equipo) => map_equipo.ConsultarPorId_382QA(equipo.IdEquipo)!;

        /// <summary>Equipo activo por IMEI, o null si no existe o está dado de baja (lo usa CU-NEG-1).</summary>
        public BE_Equipo_382QA? ConsultarPorImei_382QA(string imei)
        {
            BE_Equipo_382QA? equipo = map_equipo.ConsultarPorImei_382QA(imei?.Trim() ?? "");
            return equipo != null && equipo.Activo ? equipo : null;
        }

        public bool EstaDadoDeBaja_382QA(string imei)
        {
            BE_Equipo_382QA? equipo = map_equipo.ConsultarPorImei_382QA(imei?.Trim() ?? "");
            return equipo != null && !equipo.Activo;
        }

        /// <summary>
        /// Alta de equipo. Si el IMEI pertenece a un equipo dado de baja, lo reactiva con los datos nuevos.
        /// Al terminar, equipo.IdEquipo tiene el id del registro.
        /// </summary>
        public void Agregar_382QA(BE_Equipo_382QA equipo)
        {
            Normalizar_382QA(equipo);
            Validar_382QA(equipo);

            BE_Equipo_382QA? existente = map_equipo.ConsultarPorImei_382QA(equipo.Imei);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un equipo con el IMEI {equipo.Imei}.");

            if (existente != null)
            {
                equipo.IdEquipo = existente.IdEquipo;
                map_equipo.Modificar_382QA(equipo);
                map_equipo.CambiarActivo_382QA(equipo.IdEquipo, true);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaEquipo, equipo.IdEquipo);
                RegistrarBitacora_382QA("Reactivar equipo", 1);
            }
            else
            {
                equipo.IdEquipo = map_equipo.Agregar_382QA(equipo);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaEquipo, equipo.IdEquipo);
                RegistrarBitacora_382QA("Crear equipo", 2);
            }
        }

        public void Modificar_382QA(BE_Equipo_382QA equipo)
        {
            Normalizar_382QA(equipo);
            Validar_382QA(equipo);
            ObtenerActivo_382QA(equipo.IdEquipo);

            BE_Equipo_382QA? mismoImei = map_equipo.ConsultarPorImei_382QA(equipo.Imei);
            if (mismoImei != null && mismoImei.IdEquipo != equipo.IdEquipo)
                throw new Exception($"El IMEI {equipo.Imei} ya pertenece a otro equipo.");

            map_equipo.Modificar_382QA(equipo);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaEquipo, equipo.IdEquipo);
            RegistrarBitacora_382QA("Modificar equipo", 2);
        }

        /// <summary>Baja lógica. No se permite si el equipo tiene órdenes de trabajo sin entregar o un reclamo abierto.</summary>
        public void Borrar_382QA(BE_Equipo_382QA equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo.");
            BE_Equipo_382QA existente = ObtenerActivo_382QA(equipo.IdEquipo);

            int otAbiertas = map_equipo.ContarOT_382QA(existente.IdEquipo, EstadosOT_382QA.Abiertas);
            if (otAbiertas > 0)
                throw new Exception($"No se puede eliminar el equipo: tiene {otAbiertas} orden(es) de trabajo sin entregar.");

            int reclamoAbierto = map_reclamo.ObtenerReclamoDelEquipo_382QA(existente.IdEquipo, EstadosReclamo_382QA.Abiertos);
            if (reclamoAbierto > 0)
                throw new Exception($"No se puede eliminar el equipo: tiene el reclamo por garantía N° {reclamoAbierto} abierto.");

            map_equipo.CambiarActivo_382QA(existente.IdEquipo, false);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaEquipo, existente.IdEquipo);
            RegistrarBitacora_382QA("Eliminar equipo", 1);
        }

        private BE_Equipo_382QA ObtenerActivo_382QA(int idEquipo)
        {
            BE_Equipo_382QA? equipo = map_equipo.ConsultarPorId_382QA(idEquipo);
            if (equipo == null || !equipo.Activo)
                throw new Exception("El equipo no existe o fue dado de baja.");
            return equipo;
        }

        private void Normalizar_382QA(BE_Equipo_382QA equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo.");
            equipo.Marca = equipo.Marca?.Trim() ?? "";
            equipo.Modelo = equipo.Modelo?.Trim() ?? "";
            equipo.Imei = equipo.Imei?.Trim() ?? "";
        }

        private void Validar_382QA(BE_Equipo_382QA equipo)
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

        private void RegistrarBitacora_382QA(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
