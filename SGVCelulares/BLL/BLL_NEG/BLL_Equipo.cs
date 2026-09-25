using BE;
using MAP.MAP_SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.BLL_NEG
{
    public class BLL_Equipo
    {
        MAP_Equipo map_equipo;
        public BLL_Equipo() => map_equipo = new();

        // Paso 5 del CU: ningún campo obligatorio vacío + formato de IMEI (15 dígitos numéricos)
        public void ValidarDatos(string marca, string modelo, string imei, string descripcionFalla)
        {
            if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo)
                || string.IsNullOrWhiteSpace(imei) || string.IsNullOrWhiteSpace(descripcionFalla))
                throw new Exception("Todos los campos del equipo son obligatorios.");

            if (!Regex.IsMatch(imei, @"^\d{15}$"))
                throw new Exception("El IMEI debe tener 15 dígitos numéricos.");
        }

        public BE_Equipo Agregar(string marca, string modelo, string imei, string descripcionFalla)
        {
            ValidarDatos(marca, modelo, imei, descripcionFalla);

            // TODO: reemplazar por el criterio real de generación de IdEquipo
            string idEquipo = Guid.NewGuid().ToString("N").Substring(0, 20);

            BE_Equipo equipo = new BE_Equipo(idEquipo, marca, modelo, imei, descripcionFalla);
            map_equipo.Agregar(equipo);
            return equipo;
        }
    }
}
