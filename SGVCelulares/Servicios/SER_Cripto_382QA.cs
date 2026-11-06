using System.Security.Cryptography;
using System.Text;

namespace Servicio
{
    public class SER_Cripto_382QA
    {
        public static string Encriptar_382QA(string texto)
        {
            byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytesHash = sha.ComputeHash(bytesTexto);
                StringBuilder sb = new StringBuilder();
                foreach(byte b in bytesHash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
