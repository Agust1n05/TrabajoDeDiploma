using MAP;
using Servicio;
using System.Reflection;
using System.Text;

namespace BLL
{
    public class BLL_DV
    {
        MAP_DV map_dv;
        private const string SEPARADOR_HASH = "|";
        private const string SEPARADOR_ID = "-";
        public BLL_DV() 
        { 
            map_dv = new MAP_DV();
        }
        public void GenerarDVH(List<string> valores, string nombreTabla)
        {
            string id = string.Join(SEPARADOR_ID, valores);
            string hash = CalcularHash(valores.ToArray());

            SER_DVH ser_dvh = new SER_DVH(nombreTabla, id, hash);

            if (map_dv.ExisteDVH(nombreTabla, id))
                map_dv.ModificarDVH(ser_dvh);
            else
                map_dv.AgregarDVH(ser_dvh);
        }
        public void GenerarDVH(object o, string id, string nombreTabla)
        {
            string hash = CalcularHash(o);
            SER_DVH ser_dvh = new SER_DVH(nombreTabla, id, hash);
            if (map_dv.ExisteDVH(nombreTabla, id))
            {
                map_dv.ModificarDVH(ser_dvh);
            }
            else
            {
                map_dv.AgregarDVH(ser_dvh);
            }
        }
        public void GenerarDVV(string nombreTabla)
        {
            List<string> listaDVH = map_dv.ConsultarDVHValores(nombreTabla);

            if (listaDVH.Count == 0)
            {
                if (map_dv.ExisteDVV(nombreTabla))
                    map_dv.BorrarDVV(nombreTabla);
                return;
            }

            string hash = CalcularHash(listaDVH);
            SER_DVV ser_dvv = new SER_DVV(nombreTabla, hash);

            if (map_dv.ExisteDVV(nombreTabla))
                map_dv.ModificarDVV(ser_dvv);
            else
                map_dv.AgregarDVV(ser_dvv);
        }
        public void RecalcularDV()
        {
            var bll_usuario = new BLL_Usuario();
            var bll_rol = new BLL_Rol();
            var bll_familia = new BLL_Familia();

            // Usuarios
            var usuarios = bll_usuario.Consultar().OrderBy(u => u.Dni).ToList();
            BorrarDVHHuerfanos("usuario", usuarios.Select(u => u.Dni).ToList());
            foreach (var usuario in usuarios)
                GenerarDVH(usuario, usuario.Dni, "usuario");
            GenerarDVV("usuario");

            // Roles
            var roles = bll_rol.Consultar().OrderBy(r => r.Id).ToList();
            BorrarDVHHuerfanos("rol", roles.Select(r => r.Id.ToString()).ToList());
            foreach (var rol in roles)
                GenerarDVH(rol, rol.Id.ToString(), "rol");
            GenerarDVV("rol");

            // Familias
            var familias = bll_familia.Consultar().OrderBy(f => f.Id).ToList();
            BorrarDVHHuerfanos("familia", familias.Select(f => f.Id.ToString()).ToList());
            foreach (var familia in familias)
                GenerarDVH(familia, familia.Id.ToString(), "familia");
            GenerarDVV("familia");

            // Tablas intermedias
            RecalcularTablaIntermedia(bll_rol.ConsultarRolFamilia(), "rol_familia");
                   
            RecalcularTablaIntermedia(bll_familia.ConsultarFamiliaFamilia(), "familia_familia");
            RecalcularTablaIntermedia(bll_familia.ConsultarFamiliaPermiso(), "permiso_familia");
            RecalcularTablaIntermedia(bll_rol.ConsultarRolPermiso(), "rol_permiso");
        }

        private void RecalcularTablaIntermedia(List<object[]> registros, string nombreTabla)
        {
            var idsReales = registros.Select(r => $"{r[0]}{SEPARADOR_ID}{r[1]}").ToList();
            BorrarDVHHuerfanos(nombreTabla, idsReales);

            foreach (var reg in registros)
                GenerarDVH(new List<string>() { reg[0].ToString(), reg[1].ToString() }, nombreTabla);

            GenerarDVV(nombreTabla);
        }
        private void BorrarDVHHuerfanos(string nombreTabla, List<string> idsReales)
        {
            List<string> idsDVH = map_dv.ConsultarDVHIds(nombreTabla);

            foreach (var id in idsDVH)
            {
                if (!idsReales.Contains(id))
                    BorrarDVH(nombreTabla, id);
            }
        }
        public void BorrarDVH(string nombreTabla, string id)
        {
            if (map_dv.ExisteDVH(nombreTabla, id))
            {
                map_dv.BorrarDVH(nombreTabla, id);
            }
        }
        // Para FrmRepararInconsistencia: junta todo, no corta nunca
        public List<SER_Inconsistencia> ObtenerInconsistencias()
        {
            return VerificarTodasLasTablas();
        }

        // Para el resto del sistema (ej. login): mantiene el comportamiento actual de cortar con excepción
        public bool VerificarIntegridad()
        {
            var inconsistencias = VerificarTodasLasTablas();

            if (inconsistencias.Count > 0)
                throw new Exception("Se detecto inconsistencia en la base de datos. \n Redirigiendo a formulario de reparación.");

            return true;
        }
        private List<SER_Inconsistencia> VerificarTodasLasTablas()
        {
            var inconsistencias = new List<SER_Inconsistencia>();

            var bll_usuario = new BLL_Usuario();
            var bll_rol = new BLL_Rol();
            var bll_familia = new BLL_Familia();

            var usuarios = bll_usuario.Consultar().OrderBy(u => u.Dni).ToList();
            var roles = bll_rol.Consultar().OrderBy(r => r.Id).ToList();
            var familias = bll_familia.Consultar().OrderBy(f => f.Id).ToList();
            var rolFamilia = OrdenarCompuesta(bll_rol.ConsultarRolFamilia());
            var rolPermiso = OrdenarCompuesta(bll_rol.ConsultarRolPermiso());
            var familiaFamilia = OrdenarCompuesta(bll_familia.ConsultarFamiliaFamilia());
            var familiaPermiso = OrdenarCompuesta(bll_familia.ConsultarFamiliaPermiso());

            Func<object[], string> getIdCompuesto = x => $"{x[0]}{SEPARADOR_ID}{x[1]}";

            VerificarTabla("usuario", usuarios, u => u.Dni, inconsistencias);
            VerificarTabla("rol", roles, r => Convert.ToString(r.Id), inconsistencias);
            VerificarTabla("familia", familias, f => Convert.ToString(f.Id), inconsistencias);
            VerificarTabla("rol_familia", rolFamilia, getIdCompuesto, inconsistencias);
            VerificarTabla("rol_permiso", rolPermiso, getIdCompuesto, inconsistencias);
            VerificarTabla("familia_familia", familiaFamilia, getIdCompuesto, inconsistencias);
            VerificarTabla("permiso_familia", familiaPermiso, getIdCompuesto, inconsistencias);

            return inconsistencias;
        }
        private void VerificarTabla<T>(string nombreTabla, List<T> registros, Func<T, string> getId, List<SER_Inconsistencia> inconsistencias)
        {
            int cantidadDVH = map_dv.ConsultarDVHValores(nombreTabla).Count;

            if (registros.Count == 0 && cantidadDVH == 0 && !map_dv.ExisteDVV(nombreTabla))
                return;

            if (cantidadDVH > registros.Count)
            {
                inconsistencias.Add(new SER_Inconsistencia(nombreTabla, "-",
                    $"La tabla '{nombreTabla}' tiene más entradas en DVH ({cantidadDVH}) que registros reales ({registros.Count}). Ocurrió una ELIMINACIÓN directa en BD."));
                return;
            }

            var dvhCalculados = new List<string>();
            bool huboErrorRegistro = false;

            foreach (var reg in registros)
            {
                string dvhCalc = CalcularHash(reg);
                SER_DVH dvh = map_dv.ObtenerDVH(nombreTabla, getId(reg));

                if (dvh == null)
                {
                    inconsistencias.Add(new SER_Inconsistencia(nombreTabla, getId(reg),
                        $"No se encontró el DVH para el registro con Datos (INSERCIÓN):{Environment.NewLine}{MostrarDatos(reg, nombreTabla)}"));
                    huboErrorRegistro = true;
                    continue;
                }

                if (dvhCalc != dvh.Valor)
                {
                    inconsistencias.Add(new SER_Inconsistencia(nombreTabla, getId(reg),
                        $"No coincide El DVH para el registro con Datos (MODIFICACIÓN):{Environment.NewLine}{MostrarDatos(reg, nombreTabla)}."));
                    huboErrorRegistro = true;
                    continue;
                }

                dvhCalculados.Add(dvhCalc);
            }

            if (huboErrorRegistro) return; // el DVV va a fallar como consecuencia directa; reportarlo también sería ruido

            SER_DVV dvv = map_dv.ObtenerDVV(nombreTabla);
            if (dvv == null)
            {
                inconsistencias.Add(new SER_Inconsistencia(nombreTabla, "(tabla completa)",
                    $"La tabla '{nombreTabla}' tiene registros con DVH válido, pero no existe un DVV generado para la tabla.{Environment.NewLine}Posible eliminación directa del DVV en la base de datos."));
                return;
            }

            string dvvCalc = CalcularHash(dvhCalculados);
            if (dvvCalc != dvv.Valor)
            {
                inconsistencias.Add(new SER_Inconsistencia(nombreTabla, "(tabla completa)",
                    $"El DVV para la tabla '{nombreTabla}' no coincide con el valor calculado.{Environment.NewLine}..."));
            }
        }
        private string Concatenar(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj is object[] valores)
            {
                return string.Join(SEPARADOR_HASH, valores.Select(v => v?.ToString() ?? string.Empty));
            }

            if (obj is IEnumerable<string> listaStr)
            {
                return string.Join(SEPARADOR_HASH, listaStr);
            }

            PropertyInfo[] propiedades = obj.GetType().GetProperties().OrderBy(p => p.Name).ToArray();

            return string.Join(SEPARADOR_HASH,
                propiedades.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty));
        }
        private string MostrarDatos(object obj, string nombreTabla = null)
        {
            if (obj is object[] valores)
            {
                string[] nombresCampos = ObtenerNombresCampos(nombreTabla);

                var lineas = valores.Select((v, i) =>
                {
                    string nombreCampo = (nombresCampos != null && i < nombresCampos.Length)
                        ? nombresCampos[i]
                        : $"Campo{i}";
                    return $"{nombreCampo}: {v?.ToString() ?? "null"}";
                });

                return string.Join(Environment.NewLine, lineas);
            }

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = obj.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) ||
                            !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType))
                .ToArray();

            foreach (PropertyInfo propiedad in propiedades)
            {
                object valor = propiedad.GetValue(obj);
                sb.Append($"{propiedad.Name}: {valor}").Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }

        private string[] ObtenerNombresCampos(string nombreTabla)
        {
            switch (nombreTabla)
            {
                case "rol_familia": return new[] { "IdRol", "IdFamilia" };
                case "rol_permiso": return new[] { "IdRol", "IdPermiso" };
                case "familia_familia": return new[] { "IdFamiliaPadre", "IdFamiliaHija" };
                case "permiso_familia": return new[] { "IdFamilia", "IdPermiso" };
                default: return null;
            }
        }

        private List<object[]> OrdenarCompuesta(List<object[]> lista)
        {
            return lista.OrderBy(x => Convert.ToInt32(x[0]))
                        .ThenBy(x => Convert.ToInt32(x[1]))
                        .ToList();
        }
      
        public string CalcularHash(object o) => SER_Cripto.Encriptar(Concatenar(o));
    }
}
