namespace Servicio
{
    public interface IABMC<T>
    {
        void Agregar(T obj);
        void Borrar(T obj);
        void Modificar(T obj);
        List<T> Consultar();
        T ConsultarPorId(T obj);
    }
}
