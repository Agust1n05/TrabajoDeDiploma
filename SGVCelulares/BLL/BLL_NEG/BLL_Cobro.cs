using BE;
using System.Text.RegularExpressions;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// Cobro de la entrega (CU-NEG-3 Gestionar entrega, pasos 7 y 8). Solo tarjeta y sin integración con una entidad bancaria:
    /// se valida el formato de los datos y, si es correcto, el cobro se da por aprobado.
    /// Los datos de la tarjeta no se guardan; el comprobante de cobro lleva solo el tipo y los últimos 4 dígitos.
    /// </summary>
    public class BLL_Cobro
    {
        public const int LargoMaximoTitular = 50;

        /// <summary>
        /// Valida el formato de la tarjeta y devuelve el comprobante de cobro a registrar (todavía sin número).
        /// Si hay datos inválidos, lanza una excepción con todos los errores juntos.
        /// </summary>
        public BE_ComprobanteCobro RealizarCobro(int nroOT, decimal monto, BE_DatosTarjeta? tarjeta, DateTime fecha)
        {
            if (monto <= 0)
                throw new Exception("El monto a cobrar tiene que ser mayor a cero.");

            List<string> errores = ValidarTarjeta(tarjeta, fecha);
            if (errores.Count > 0)
                throw new Exception("Revisá los datos de la tarjeta:" + Environment.NewLine +
                                    string.Join(Environment.NewLine, errores.Select(e => "• " + e)));

            // Cobro aprobado (sin entidad bancaria): solo se conserva el tipo y los últimos 4 dígitos
            return new BE_ComprobanteCobro(0, nroOT, monto, tarjeta!.Tipo!, tarjeta.UltimosDigitos, fecha.Date);
        }

        /// <summary>Errores de formato de la tarjeta (lista vacía si está todo bien).</summary>
        public List<string> ValidarTarjeta(BE_DatosTarjeta? tarjeta, DateTime fecha)
        {
            var errores = new List<string>();
            if (tarjeta == null)
            {
                errores.Add("Faltan los datos de la tarjeta.");
                return errores;
            }

            if (!MediosPago.Todos.Contains(tarjeta.Tipo))
                errores.Add("Elegí si la tarjeta es de débito o de crédito.");

            string numero = tarjeta.NumeroLimpio;
            if (!Regex.IsMatch(numero, @"^\d{13,19}$"))
                errores.Add("El número de tarjeta tiene que tener entre 13 y 19 dígitos.");
            else if (!CumpleLuhn(numero))
                errores.Add("El número de tarjeta no es válido (verificá que esté bien copiado).");

            string titular = tarjeta.Titular?.Trim() ?? "";
            if (titular.Length == 0)
                errores.Add("El nombre del titular es obligatorio.");
            else if (titular.Length > LargoMaximoTitular || !Regex.IsMatch(titular, @"^[\p{L} .'-]+$"))
                errores.Add($"El nombre del titular solo puede tener letras y espacios (máximo {LargoMaximoTitular}).");

            Match venc = Regex.Match(tarjeta.Vencimiento?.Trim() ?? "", @"^(0[1-9]|1[0-2])/(\d{2})$");
            if (!venc.Success)
                errores.Add("El vencimiento tiene que tener el formato MM/AA.");
            else
            {
                int mes = int.Parse(venc.Groups[1].Value);
                int anio = 2000 + int.Parse(venc.Groups[2].Value);
                var ultimoDiaValido = new DateTime(anio, mes, DateTime.DaysInMonth(anio, mes));
                if (ultimoDiaValido < fecha.Date)
                    errores.Add("La tarjeta está vencida.");
            }

            if (!Regex.IsMatch(tarjeta.Cvv ?? "", @"^\d{3,4}$"))
                errores.Add("El código de seguridad tiene que tener 3 o 4 dígitos.");

            return errores;
        }

        /// <summary>Dígito verificador de las tarjetas (algoritmo de Luhn).</summary>
        public static bool CumpleLuhn(string numero)
        {
            int suma = 0;
            bool duplicar = false;
            for (int i = numero.Length - 1; i >= 0; i--)
            {
                int digito = numero[i] - '0';
                if (duplicar)
                {
                    digito *= 2;
                    if (digito > 9) digito -= 9;
                }
                suma += digito;
                duplicar = !duplicar;
            }
            return suma % 10 == 0;
        }
    }
}
