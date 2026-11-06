namespace Servicio
{
    public class SER_Idioma_382QA
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Dictionary<string, string> Textos { get; set; }
        public SER_Idioma_382QA()
        {
            Textos = new Dictionary<string, string>();
        }

        public SER_Idioma_382QA(string codigo)
        {
            Codigo = codigo;
            Textos = new Dictionary<string, string>();
        }

        public SER_Idioma_382QA(string codigo, Dictionary<string, string> textos)
        {
            Codigo = codigo;
            Textos = textos ?? new Dictionary<string, string>();
        }
        public SER_Idioma_382QA(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
            Textos = new Dictionary<string, string>();
        }

    }
}
