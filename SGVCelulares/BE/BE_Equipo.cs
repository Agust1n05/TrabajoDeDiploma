namespace BE
{
    /// <summary>
    /// Entidad de negocio Equipo (celular recibido). Usada en CU-NEG-1 (Gestionar recepción de equipo).
    /// DER: Equipo(*PK idEquipo varchar(50), marca varchar(50), modelo varchar(50),
    ///             imei varchar(50), descripcionFalla varchar(50))
    /// </summary>
    public class BE_Equipo
    {
        public string IdEquipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imei { get; set; }
        public string DescripcionFalla { get; set; }

        public BE_Equipo()
        {
        }

        public BE_Equipo(string idEquipo, string marca, string modelo, string imei, string descripcionFalla)
        {
            IdEquipo = idEquipo;
            Marca = marca;
            Modelo = modelo;
            Imei = imei;
            DescripcionFalla = descripcionFalla;
        }
    }
}
