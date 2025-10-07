using BinaryOperations.Web.Models; // ✅ AGREGAR ESTA LÍNEA

namespace BinaryOperations.Web.Services
{
    public interface IBinaryOperationService
    {
        ResultTable CalculateOperations(string a, string b);
    }

    public class BinaryOperationService : IBinaryOperationService
    {
        public ResultTable CalculateOperations(string a, string b)
        {
            var result = new ResultTable();

            // Convertir a formato byte (8 bits)
            string byteA = a.PadLeft(8, '0');
            string byteB = b.PadLeft(8, '0');

            // Calcular valores para a y b
            result.A = CalculateRowValues(byteA);
            result.B = CalculateRowValues(byteB);

            // Operaciones binarias
            result.And = CalculateRowValues(BinaryAND(byteA, byteB));
            result.Or = CalculateRowValues(BinaryOR(byteA, byteB));
            result.Xor = CalculateRowValues(BinaryXOR(byteA, byteB));

            // Operaciones aritméticas
            int decimalA = Convert.ToInt32(byteA, 2);
            int decimalB = Convert.ToInt32(byteB, 2);

            result.Sum = CalculateRowValues(Convert.ToString(decimalA + decimalB, 2));
            result.Multiply = CalculateRowValues(Convert.ToString(decimalA * decimalB, 2));

            return result;
        }

        private TableRow CalculateRowValues(string binary)
        {
            // Asegurar que el binario tenga formato adecuado
            string cleanBinary = binary.TrimStart('0');
            if (string.IsNullOrEmpty(cleanBinary)) cleanBinary = "0";

            int decimalValue = Convert.ToInt32(cleanBinary, 2);

            return new TableRow
            {
                Binary = cleanBinary,
                Octal = Convert.ToString(decimalValue, 8),
                Decimal = decimalValue.ToString(),
                Hexadecimal = Convert.ToString(decimalValue, 16).ToUpper()
            };
        }

        private string BinaryAND(string a, string b)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = (a[i] == '1' && b[i] == '1') ? '1' : '0';
            }
            return new string(result).TrimStart('0');
        }

        private string BinaryOR(string a, string b)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = (a[i] == '1' || b[i] == '1') ? '1' : '0';
            }
            return new string(result).TrimStart('0');
        }

        private string BinaryXOR(string a, string b)
        {
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = (a[i] != b[i]) ? '1' : '0';
            }
            return new string(result).TrimStart('0');
        }
    }
}