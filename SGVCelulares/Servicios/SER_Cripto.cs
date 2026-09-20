using System.Security.Cryptography;
using System.Text;

namespace Servicio
{
    public class SER_Cripto
    {
        public static string Encriptar(string texto)
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
