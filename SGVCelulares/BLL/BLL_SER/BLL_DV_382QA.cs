using MAP.MAP_SER;
using Servicio;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace BLL.BLL_SER
{
    public class BLL_DV_382QA
    {
        MAP_DV_382QA map_dv;
        private const string SEPARADOR_HASH = "|";
        private const string SEPARADOR_ID = "-";

        // ---------------- Tablas de negocio (RF01) ----------------
        // Nombre de cada tabla en dvh/dvv y las columnas de su clave primaria.
        public const string TablaCliente = "cliente";
        public const string TablaTecnico = "tecnico";
        public const string TablaEquipo = "equipo";
        public const string TablaRepuesto = "repuesto";
        public const string TablaOrdenTrabajo = "ordendetrabajo";
        public const string TablaDiagnostico = "diagnostico";
        public const string TablaDetalleRepuesto = "detallerepuesto";
        public const string TablaPresupuesto = "presupuesto";
        public const string TablaComprobanteCobro = "comprobantecobro";
        public const string TablaGarantia = "garantia";
        // ---------------- Tablas de negocio (RF02) ----------------
        public const string TablaReclamo = "reclamo";
        public const string TablaDetalleRepuestoReclamo = "detallerepuestoreclamo";

        private static readonly Dictionary<string, string[]> TablasNegocio = new()
        {
            { TablaCliente,         new[] { "dni" } },
            { TablaTecnico,         new[] { "Dni" } },
            { TablaEquipo,          new[] { "idEquipo" } },
            { TablaRepuesto,        new[] { "Codigo" } },
            { TablaOrdenTrabajo,    new[] { "NroOT" } },
            { TablaDiagnostico,     new[] { "IdDiagnostico" } },
            { TablaDetalleRepuesto, new[] { "IdDiagnostico", "Codigo" } },
            { TablaPresupuesto,     new[] { "IdPresupuesto" } },
            { TablaComprobanteCobro, new[] { "NroComprobante" } },
            { TablaGarantia,        new[] { "NroOT" } },
            { TablaReclamo,         new[] { "NroReclamo" } },
            { TablaDetalleRepuestoReclamo, new[] { "NroReclamo", "Codigo" } }
        };
        public BLL_DV_382QA() 
        { 
            map_dv = new MAP_DV_382QA();
        }
        public void GenerarDVH_382QA(List<string> valores, string nombreTabla)
        {
            string id = string.Join(SEPARADOR_ID, valores);
            string hash = CalcularHash_382QA(valores.ToArray());

            SER_DVH_382QA ser_dvh = new SER_DVH_382QA(nombreTabla, id, hash);

            if (map_dv.ExisteDVH_382QA(nombreTabla, id))
                map_dv.ModificarDVH_382QA(ser_dvh);
            else
                map_dv.AgregarDVH_382QA(ser_dvh);
        }
        public void GenerarDVH_382QA(object o, string id, string nombreTabla)
        {
            string hash = CalcularHash_382QA(o);
            SER_DVH_382QA ser_dvh = new SER_DVH_382QA(nombreTabla, id, hash);
            if (map_dv.ExisteDVH_382QA(nombreTabla, id))
            {
                map_dv.ModificarDVH_382QA(ser_dvh);
            }
            else
            {
                map_dv.AgregarDVH_382QA(ser_dvh);
            }
        }
        public void GenerarDVV_382QA(string nombreTabla)
        {
            List<string> listaDVH = map_dv.ConsultarDVHValores_382QA(nombreTabla);

            if (listaDVH.Count == 0)
            {
                if (map_dv.ExisteDVV_382QA(nombreTabla))
                    map_dv.BorrarDVV_382QA(nombreTabla);
                return;
            }

            string hash = CalcularHash_382QA(listaDVH);
            SER_DVV_382QA ser_dvv = new SER_DVV_382QA(nombreTabla, hash);

            if (map_dv.ExisteDVV_382QA(nombreTabla))
                map_dv.ModificarDVV_382QA(ser_dvv);
            else
                map_dv.AgregarDVV_382QA(ser_dvv);
        }
        public void RecalcularDV_382QA()
        {
            var bll_usuario = new BLL_Usuario_382QA();
            var bll_rol = new BLL_Rol_382QA();
            var bll_familia = new BLL_Familia_382QA();

            // Usuarios
            var usuarios = bll_usuario.Consultar_382QA().OrderBy(u => u.Dni).ToList();
            BorrarDVHHuerfanos_382QA("usuario", usuarios.Select(u => u.Dni).ToList());
            foreach (var usuario in usuarios)
                GenerarDVH_382QA(usuario, usuario.Dni, "usuario");
            GenerarDVV_382QA("usuario");

            // Roles
            var roles = bll_rol.Consultar_382QA().OrderBy(r => r.Id).ToList();
            BorrarDVHHuerfanos_382QA("rol", roles.Select(r => r.Id.ToString()).ToList());
            foreach (var rol in roles)
                GenerarDVH_382QA(rol, rol.Id.ToString(), "rol");
            GenerarDVV_382QA("rol");

            // Familias
            var familias = bll_familia.Consultar_382QA().OrderBy(f => f.Id).ToList();
            BorrarDVHHuerfanos_382QA("familia", familias.Select(f => f.Id.ToString()).ToList());
            foreach (var familia in familias)
                GenerarDVH_382QA(familia, familia.Id.ToString(), "familia");
            GenerarDVV_382QA("familia");

            // Tablas intermedias
            RecalcularTablaIntermedia_382QA(bll_rol.ConsultarRolFamilia_382QA(), "rol_familia");
                   
            RecalcularTablaIntermedia_382QA(bll_familia.ConsultarFamiliaFamilia_382QA(), "familia_familia");
            RecalcularTablaIntermedia_382QA(bll_familia.ConsultarFamiliaPermiso_382QA(), "permiso_familia");
            RecalcularTablaIntermedia_382QA(bll_rol.ConsultarRolPermiso_382QA(), "rol_permiso");

            // Tablas de negocio
            foreach (string tablaNegocio in TablasNegocio.Keys)
                RecalcularTablaNegocio_382QA(tablaNegocio);
        }

        private void RecalcularTablaIntermedia_382QA(List<object[]> registros, string nombreTabla)
        {
            var idsReales = registros.Select(r => $"{r[0]}{SEPARADOR_ID}{r[1]}").ToList();
            BorrarDVHHuerfanos_382QA(nombreTabla, idsReales);

            foreach (var reg in registros)
                GenerarDVH_382QA(new List<string>() { reg[0].ToString(), reg[1].ToString() }, nombreTabla);

            GenerarDVV_382QA(nombreTabla);
        }
        private void BorrarDVHHuerfanos_382QA(string nombreTabla, List<string> idsReales)
        {
            List<string> idsDVH = map_dv.ConsultarDVHIds_382QA(nombreTabla);

            foreach (var id in idsDVH)
            {
                if (!idsReales.Contains(id))
                    BorrarDVH_382QA(nombreTabla, id);
            }
        }
        public void BorrarDVH_382QA(string nombreTabla, string id)
        {
            if (map_dv.ExisteDVH_382QA(nombreTabla, id))
            {
                map_dv.BorrarDVH_382QA(nombreTabla, id);
            }
        }
        // Para FrmRepararInconsistencia_382QA: junta todo, no corta nunca
        public List<SER_Inconsistencia_382QA> ObtenerInconsistencias_382QA()
        {
            return VerificarTodasLasTablas_382QA();
        }

        // Para el resto del sistema (ej. login): mantiene el comportamiento actual de cortar con excepción
        public bool VerificarIntegridad_382QA()
        {
            var inconsistencias = VerificarTodasLasTablas_382QA();

            if (inconsistencias.Count > 0)
                throw new Exception("Se detecto inconsistencia en la base de datos. \n Redirigiendo a formulario de reparación.");

            return true;
        }
        private List<SER_Inconsistencia_382QA> VerificarTodasLasTablas_382QA()
        {
            var inconsistencias = new List<SER_Inconsistencia_382QA>();

            var bll_usuario = new BLL_Usuario_382QA();
            var bll_rol = new BLL_Rol_382QA();
            var bll_familia = new BLL_Familia_382QA();

            var usuarios = bll_usuario.Consultar_382QA().OrderBy(u => u.Dni).ToList();
            var roles = bll_rol.Consultar_382QA().OrderBy(r => r.Id).ToList();
            var familias = bll_familia.Consultar_382QA().OrderBy(f => f.Id).ToList();
            var rolFamilia = OrdenarCompuesta_382QA(bll_rol.ConsultarRolFamilia_382QA());
            var rolPermiso = OrdenarCompuesta_382QA(bll_rol.ConsultarRolPermiso_382QA());
            var familiaFamilia = OrdenarCompuesta_382QA(bll_familia.ConsultarFamiliaFamilia_382QA());
            var familiaPermiso = OrdenarCompuesta_382QA(bll_familia.ConsultarFamiliaPermiso_382QA());

            Func<object[], string> getIdCompuesto = x => $"{x[0]}{SEPARADOR_ID}{x[1]}";

            VerificarTabla_382QA("usuario", usuarios, u => u.Dni, inconsistencias);
            VerificarTabla_382QA("rol", roles, r => Convert.ToString(r.Id), inconsistencias);
            VerificarTabla_382QA("familia", familias, f => Convert.ToString(f.Id), inconsistencias);
            VerificarTabla_382QA("rol_familia", rolFamilia, getIdCompuesto, inconsistencias);
            VerificarTabla_382QA("rol_permiso", rolPermiso, getIdCompuesto, inconsistencias);
            VerificarTabla_382QA("familia_familia", familiaFamilia, getIdCompuesto, inconsistencias);
            VerificarTabla_382QA("permiso_familia", familiaPermiso, getIdCompuesto, inconsistencias);

            foreach (string tablaNegocio in TablasNegocio.Keys)
                VerificarTablaNegocio_382QA(tablaNegocio, inconsistencias);

            return inconsistencias;
        }
        private void VerificarTabla_382QA<T>(string nombreTabla, List<T> registros, Func<T, string> getId, List<SER_Inconsistencia_382QA> inconsistencias)
        {
            int cantidadDVH = map_dv.ConsultarDVHValores_382QA(nombreTabla).Count;

            if (registros.Count == 0 && cantidadDVH == 0 && !map_dv.ExisteDVV_382QA(nombreTabla))
                return;

            if (cantidadDVH > registros.Count)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "-",
                    $"La tabla '{nombreTabla}' tiene más entradas en DVH ({cantidadDVH}) que registros reales ({registros.Count}). Ocurrió una ELIMINACIÓN directa en BD."));
                return;
            }

            var dvhCalculados = new List<string>();
            bool huboErrorRegistro = false;

            foreach (var reg in registros)
            {
                string dvhCalc = CalcularHash_382QA(reg);
                SER_DVH_382QA dvh = map_dv.ObtenerDVH_382QA(nombreTabla, getId(reg));

                if (dvh == null)
                {
                    inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, getId(reg),
                        $"No se encontró el DVH para el registro con Datos (INSERCIÓN):{Environment.NewLine}{MostrarDatos_382QA(reg, nombreTabla)}"));
                    huboErrorRegistro = true;
                    continue;
                }

                if (dvhCalc != dvh.Valor)
                {
                    inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, getId(reg),
                        $"No coincide El DVH para el registro con Datos (MODIFICACIÓN):{Environment.NewLine}{MostrarDatos_382QA(reg, nombreTabla)}."));
                    huboErrorRegistro = true;
                    continue;
                }

                dvhCalculados.Add(dvhCalc);
            }

            if (huboErrorRegistro) return; // el DVV va a fallar como consecuencia directa; reportarlo también sería ruido

            SER_DVV_382QA dvv = map_dv.ObtenerDVV_382QA(nombreTabla);
            if (dvv == null)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "(tabla completa)",
                    $"La tabla '{nombreTabla}' tiene registros con DVH válido, pero no existe un DVV generado para la tabla.{Environment.NewLine}Posible eliminación directa del DVV en la base de datos."));
                return;
            }

            string dvvCalc = CalcularHash_382QA(dvhCalculados);
            if (dvvCalc != dvv.Valor)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "(tabla completa)",
                    $"El DVV para la tabla '{nombreTabla}' no coincide con el valor calculado.{Environment.NewLine}..."));
            }
        }
        private string Concatenar_382QA(object obj)
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

            // Solo propiedades simples: el ToString() de un objeto o lista (Rol, Componentes) es el nombre del tipo, no su contenido.
            PropertyInfo[] propiedades = obj.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) || p.PropertyType.IsValueType)
                .OrderBy(p => p.Name).ToArray();

            return string.Join(SEPARADOR_HASH,
                propiedades.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty));
        }
        private string MostrarDatos_382QA(object obj, string nombreTabla = null)
        {
            if (obj is object[] valores)
            {
                string[] nombresCampos = ObtenerNombresCampos_382QA(nombreTabla);

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

        private string[] ObtenerNombresCampos_382QA(string nombreTabla)
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

        private List<object[]> OrdenarCompuesta_382QA(List<object[]> lista)
        {
            return lista.OrderBy(x => Convert.ToInt32(x[0]))
                        .ThenBy(x => Convert.ToInt32(x[1]))
                        .ToList();
        }

        // =====================================================================
        // DÍGITOS VERIFICADORES DE LAS TABLAS DE NEGOCIO
        // DVH: hash de todos los valores de la fila leída de la base, en el orden de sus columnas.
        // DVV: hash de los DVH de la tabla, ordenados por IdRegistro (orden ordinal).
        // =====================================================================

        /// <summary>
        /// Recalcula el DVH de un registro de negocio y el DVV de su tabla.
        /// Se llama después de cada alta o modificación. Si el registro ya no existe, borra su DVH.
        /// Ejemplo: ActualizarDVNegocio(BLL_DV_382QA.TablaCliente, cliente.Dni)
        ///          ActualizarDVNegocio(BLL_DV_382QA.TablaDetalleRepuesto, idDiagnostico, codigo)
        /// </summary>
        public void ActualizarDVNegocio_382QA(string nombreTabla, params object[] valoresId)
        {
            string[] columnasId = ObtenerColumnasId_382QA(nombreTabla);
            if (valoresId.Length != columnasId.Length)
                throw new ArgumentException($"La tabla '{nombreTabla}' necesita {columnasId.Length} valor(es) de clave.");

            string[] idNormalizado = valoresId.Select(Normalizar_382QA).ToArray();
            string idRegistro = string.Join(SEPARADOR_ID, idNormalizado);

            SER_FilaNegocio_382QA? fila = map_dv.ConsultarFilaNegocio_382QA(nombreTabla, columnasId, idNormalizado);
            if (fila == null)
                BorrarDVH_382QA(nombreTabla, idRegistro);
            else
                GuardarDVH_382QA(nombreTabla, idRegistro, CalcularHashFila_382QA(fila));

            GenerarDVVNegocio_382QA(nombreTabla);
        }

        private void RecalcularTablaNegocio_382QA(string nombreTabla)
        {
            string[] columnasId = ObtenerColumnasId_382QA(nombreTabla);
            List<SER_FilaNegocio_382QA> filas = map_dv.ConsultarFilasNegocio_382QA(nombreTabla);
            var idsReales = new HashSet<string>();

            foreach (SER_FilaNegocio_382QA fila in filas)
            {
                string idRegistro = ObtenerIdFila_382QA(fila, columnasId);
                idsReales.Add(idRegistro);
                GuardarDVH_382QA(nombreTabla, idRegistro, CalcularHashFila_382QA(fila));
            }

            foreach (string idGuardado in map_dv.ConsultarDVHNegocio_382QA(nombreTabla).Keys)
            {
                if (!idsReales.Contains(idGuardado))
                    map_dv.BorrarDVH_382QA(nombreTabla, idGuardado);
            }

            GenerarDVVNegocio_382QA(nombreTabla);
        }

        private void GenerarDVVNegocio_382QA(string nombreTabla)
        {
            Dictionary<string, string> dvh = map_dv.ConsultarDVHNegocio_382QA(nombreTabla);

            if (dvh.Count == 0)
            {
                if (map_dv.ExisteDVV_382QA(nombreTabla))
                    map_dv.BorrarDVV_382QA(nombreTabla);
                return;
            }

            SER_DVV_382QA ser_dvv = new SER_DVV_382QA(nombreTabla, CalcularDVVNegocio_382QA(dvh));
            if (map_dv.ExisteDVV_382QA(nombreTabla))
                map_dv.ModificarDVV_382QA(ser_dvv);
            else
                map_dv.AgregarDVV_382QA(ser_dvv);
        }

        private void VerificarTablaNegocio_382QA(string nombreTabla, List<SER_Inconsistencia_382QA> inconsistencias)
        {
            string[] columnasId = ObtenerColumnasId_382QA(nombreTabla);
            List<SER_FilaNegocio_382QA> filas = map_dv.ConsultarFilasNegocio_382QA(nombreTabla);
            Dictionary<string, string> dvhGuardados = map_dv.ConsultarDVHNegocio_382QA(nombreTabla);

            if (filas.Count == 0 && dvhGuardados.Count == 0 && !map_dv.ExisteDVV_382QA(nombreTabla))
                return;

            if (dvhGuardados.Count > filas.Count)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "-",
                    $"La tabla '{nombreTabla}' tiene más entradas en DVH ({dvhGuardados.Count}) que registros reales ({filas.Count}). Ocurrió una ELIMINACIÓN directa en BD."));
                return;
            }

            var dvhCalculados = new Dictionary<string, string>();
            bool huboErrorRegistro = false;

            foreach (SER_FilaNegocio_382QA fila in filas)
            {
                string idRegistro = ObtenerIdFila_382QA(fila, columnasId);
                string dvhCalc = CalcularHashFila_382QA(fila);

                if (!dvhGuardados.TryGetValue(idRegistro, out string? dvhGuardado))
                {
                    inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, idRegistro,
                        $"No se encontró el DVH para el registro con Datos (INSERCIÓN):{Environment.NewLine}{MostrarFila_382QA(fila)}"));
                    huboErrorRegistro = true;
                    continue;
                }

                if (dvhCalc != dvhGuardado)
                {
                    inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, idRegistro,
                        $"No coincide El DVH para el registro con Datos (MODIFICACIÓN):{Environment.NewLine}{MostrarFila_382QA(fila)}."));
                    huboErrorRegistro = true;
                    continue;
                }

                dvhCalculados[idRegistro] = dvhCalc;
            }

            if (huboErrorRegistro) return; // el DVV va a fallar como consecuencia directa

            SER_DVV_382QA dvv = map_dv.ObtenerDVV_382QA(nombreTabla);
            if (dvv == null)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "(tabla completa)",
                    $"La tabla '{nombreTabla}' tiene registros con DVH válido, pero no existe un DVV generado para la tabla.{Environment.NewLine}Posible eliminación directa del DVV en la base de datos."));
                return;
            }

            if (CalcularDVVNegocio_382QA(dvhCalculados) != dvv.Valor)
            {
                inconsistencias.Add(new SER_Inconsistencia_382QA(nombreTabla, "(tabla completa)",
                    $"El DVV para la tabla '{nombreTabla}' no coincide con el valor calculado.{Environment.NewLine}..."));
            }
        }

        private void GuardarDVH_382QA(string nombreTabla, string idRegistro, string hash)
        {
            SER_DVH_382QA ser_dvh = new SER_DVH_382QA(nombreTabla, idRegistro, hash);
            if (map_dv.ExisteDVH_382QA(nombreTabla, idRegistro))
                map_dv.ModificarDVH_382QA(ser_dvh);
            else
                map_dv.AgregarDVH_382QA(ser_dvh);
        }

        private string CalcularHashFila_382QA(SER_FilaNegocio_382QA fila)
        {
            return CalcularHash_382QA(fila.Valores.Select(Normalizar_382QA).ToArray());
        }

        private string CalcularDVVNegocio_382QA(Dictionary<string, string> dvhPorId)
        {
            List<string> valoresOrdenados = dvhPorId
                .OrderBy(d => d.Key, StringComparer.Ordinal)
                .Select(d => d.Value)
                .ToList();
            return CalcularHash_382QA(valoresOrdenados);
        }

        private string ObtenerIdFila_382QA(SER_FilaNegocio_382QA fila, string[] columnasId)
        {
            return string.Join(SEPARADOR_ID, columnasId.Select(c => Normalizar_382QA(fila[c])));
        }

        private string[] ObtenerColumnasId_382QA(string nombreTabla)
        {
            if (!TablasNegocio.TryGetValue(nombreTabla, out string[]? columnas))
                throw new ArgumentException($"La tabla '{nombreTabla}' no está registrada como tabla de negocio.");
            return columnas;
        }

        /// <summary>
        /// Convierte un valor a texto sin depender de la configuración regional de la PC,
        /// para que el hash sea el mismo en cualquier equipo (ej. 4500.00 y no 4500,00).
        /// </summary>
        private static string Normalizar_382QA(object? valor)
        {
            if (valor == null || valor is DBNull) return string.Empty;
            if (valor is DateTime fecha) return fecha.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            if (valor is string texto) return texto.TrimEnd();
            if (valor is IFormattable formateable) return formateable.ToString(null, CultureInfo.InvariantCulture);
            return valor.ToString() ?? string.Empty;
        }

        private string MostrarFila_382QA(SER_FilaNegocio_382QA fila)
        {
            return string.Join(Environment.NewLine,
                fila.Columnas.Select((columna, i) => $"{columna}: {Normalizar_382QA(fila.Valores[i])}"));
        }

        public string CalcularHash_382QA(object o) => SER_Cripto_382QA.Encriptar_382QA(Concatenar_382QA(o));
    }
}
