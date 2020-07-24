using System.ComponentModel.DataAnnotations;

namespace API.Utils.Validation
{
    /// <summary>
    /// Cnpj validation attribute.
    /// </summary>
    public class CnpjValidationAttribute : ValidationAttribute
    {
        static readonly int[] Multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] Multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// CNPJ Validation
        /// </summary>
        /// <returns><c>true</c>, if CNPJ is valid, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public override bool IsValid(object value)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return true;

            if (value == null)
                return false;

            var digitosIdenticos = true;
            var ultimoDigito = -1;
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;

            foreach (var c in value.ToString())
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                    {
                        digitosIdenticos = false;
                    }

                    ultimoDigito = digito;
                    if (posicao < 12)
                    {
                        totalDigito1 += digito * Multiplicador1[posicao];
                        totalDigito2 += digito * Multiplicador2[posicao];
                    }
                    else if (posicao == 12)
                    {
                        var dv1 = (totalDigito1 % 11);
                        dv1 = dv1 < 2
                            ? 0
                            : 11 - dv1;

                        if (digito != dv1)
                            return false;

                        totalDigito2 += dv1 * Multiplicador2[12];
                    }
                    else if (posicao == 13)
                    {
                        var dv2 = (totalDigito2 % 11);

                        dv2 = dv2 < 2
                            ? 0
                            : 11 - dv2;

                        if (digito != dv2)
                            return false;
                    }

                    posicao++;
                }
            }

            return (posicao == 14) && !digitosIdenticos;
        }
    }
}
