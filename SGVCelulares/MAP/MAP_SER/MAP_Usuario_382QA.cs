using DAL;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Usuario_382QA : IABMC_382QA<SER_Usuario_382QA>
    {
        DAO_Usuario_382QA dao_usuario;
        MAP_Rol_382QA map_rol;
        public MAP_Usuario_382QA()
        {
            dao_usuario = new DAO_Usuario_382QA();
            map_rol = new MAP_Rol_382QA();
        }
        public void Agregar_382QA(SER_Usuario_382QA usuario) => dao_usuario.Agregar_382QA(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.NombreUsuario, usuario.Password, usuario.Bloqueo, usuario.Activo, usuario.CantIntentos, usuario.Rol.Id, usuario.IdIdioma);
        public void Borrar_382QA(SER_Usuario_382QA usuario) => throw new NotImplementedException();
        public void Modificar_382QA(SER_Usuario_382QA usuario) => dao_usuario.Modificar_382QA(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.NombreUsuario);
        public void ModificarPassword_382QA(SER_Usuario_382QA usuario, string claveNueva) => dao_usuario.ModificarPassword_382QA(usuario.Dni, claveNueva);
        public List<SER_Usuario_382QA> Consultar_382QA()
        {
            DataTable tabla = dao_usuario.Consultar_382QA();
            var lista = new List<SER_Usuario_382QA>();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear_382QA(fila));
            }
            return lista;
        }
        public SER_Usuario_382QA? ConsultarPorId_382QA(SER_Usuario_382QA usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorId_382QA(usuario.Dni);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }
        public SER_Usuario_382QA? ConsultarPorNombreUsuario_382QA(SER_Usuario_382QA usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorNombreUsuario_382QA(usuario.NombreUsuario);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }
        private SER_Usuario_382QA Mapear_382QA(DataRow fila)
        {
            return new SER_Usuario_382QA(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToString(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToBoolean(fila[6]),
                Convert.ToBoolean(fila[7]),
                Convert.ToInt16(fila[8]),
                map_rol.ObtenerRolPorId_382QA(Convert.ToInt16(fila[9])),
                Convert.ToString(fila[10])
            );
        }
        public bool ValidarDniRepetido_382QA(SER_Usuario_382QA usuario) => dao_usuario.ValidarRepetido_382QA(usuario.Dni);
        public bool ExisteNombreUsuario_382QA(SER_Usuario_382QA usuario) => dao_usuario.ExisteNombreUsuario_382QA(usuario.NombreUsuario);
        public bool ValidarCorreoRepetido_382QA(SER_Usuario_382QA usuario) => dao_usuario.ValidarCorreoRepetido_382QA(usuario.Correo);
        public void SumarCantidadIntento_382QA(SER_Usuario_382QA usuario) => dao_usuario.SumarCantidadIntento_382QA(usuario.Dni);
        public void Bloquear_382QA(SER_Usuario_382QA usuario) => dao_usuario.Bloquear_382QA(usuario.Dni);
        public void ReiniciarIntentos_382QA(SER_Usuario_382QA usuario) => dao_usuario.ReiniciarIntentos_382QA(usuario.Dni);
        public void Desbloquear_382QA(SER_Usuario_382QA usuario) => dao_usuario.Desbloquear_382QA(usuario.Dni);
        public void CambiarEstadoActivo_382QA(SER_Usuario_382QA usuario) => dao_usuario.CambiarEstadoActivo_382QA(usuario.Dni, usuario.Activo);
        public void ModificarIdioma_382QA(SER_Usuario_382QA usuario) => dao_usuario.ModificarIdioma_382QA(usuario.Dni, usuario.IdIdioma);
    }
}
