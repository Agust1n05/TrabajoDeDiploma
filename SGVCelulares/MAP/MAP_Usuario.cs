using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Usuario : IABMC<SER_Usuario>
    {
        DAO_Usuario dao_usuario;
        MAP_Rol map_rol;
        public MAP_Usuario()
        {
            dao_usuario = new DAO_Usuario();
            map_rol = new MAP_Rol();
        }
        public void Agregar(SER_Usuario usuario) => dao_usuario.Agregar(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.NombreUsuario, usuario.Password, usuario.Bloqueo, usuario.Activo, usuario.CantIntentos, usuario.Rol.Id, usuario.IdIdioma);
        public void Borrar(SER_Usuario usuario) => throw new NotImplementedException();
        public void Modificar(SER_Usuario usuario) => dao_usuario.Modificar(usuario.Dni, usuario.Nombre, usuario.Apellido, usuario.NombreUsuario);
        public void ModificarPassword(SER_Usuario usuario, string claveNueva) => dao_usuario.ModificarPassword(usuario.Dni, claveNueva);
        public List<SER_Usuario> Consultar()
        {
            DataTable tabla = dao_usuario.Consultar();
            var lista = new List<SER_Usuario>();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear(fila));
            }
            return lista;
        }
        public SER_Usuario? ConsultarPorId(SER_Usuario usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorId(usuario.Dni);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }
        public SER_Usuario? ConsultarPorNombreUsuario(SER_Usuario usuario)
        {
            DataTable tabla = dao_usuario.ConsultarPorNombreUsuario(usuario.NombreUsuario);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }
        private SER_Usuario Mapear(DataRow fila)
        {
            return new SER_Usuario(
                Convert.ToString(fila[0]),
                Convert.ToString(fila[1]),
                Convert.ToString(fila[2]),
                Convert.ToString(fila[3]),
                Convert.ToString(fila[4]),
                Convert.ToString(fila[5]),
                Convert.ToBoolean(fila[6]),
                Convert.ToBoolean(fila[7]),
                Convert.ToInt16(fila[8]),
                map_rol.ObtenerRolPorId(Convert.ToInt16(fila[9])),
                Convert.ToString(fila[10])
            );
        }
        public bool ValidarDniRepetido(SER_Usuario usuario) => dao_usuario.ValidarRepetido(usuario.Dni);
        public bool ExisteNombreUsuario(SER_Usuario usuario) => dao_usuario.ExisteNombreUsuario(usuario.NombreUsuario);
        public bool ValidarCorreoRepetido(SER_Usuario usuario) => dao_usuario.ValidarCorreoRepetido(usuario.Correo);
        public void SumarCantidadIntento(SER_Usuario usuario) => dao_usuario.SumarCantidadIntento(usuario.Dni);
        public void Bloquear(SER_Usuario usuario) => dao_usuario.Bloquear(usuario.Dni);
        public void ReiniciarIntentos(SER_Usuario usuario) => dao_usuario.ReiniciarIntentos(usuario.Dni);
        public void Desbloquear(SER_Usuario usuario) => dao_usuario.Desbloquear(usuario.Dni);
        public void CambiarEstadoActivo(SER_Usuario usuario) => dao_usuario.CambiarEstadoActivo(usuario.Dni, usuario.Activo);
        public void ModificarIdioma(SER_Usuario usuario) => dao_usuario.ModificarIdioma(usuario.Dni, usuario.IdIdioma);
    }
}
