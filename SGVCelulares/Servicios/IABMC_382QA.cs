namespace Servicio
{
    public interface IABMC_382QA<T>
    {
        void Agregar_382QA(T obj);
        void Borrar_382QA(T obj);
        void Modificar_382QA(T obj);
        List<T> Consultar_382QA();
        T ConsultarPorId_382QA(T obj);
    }
}
