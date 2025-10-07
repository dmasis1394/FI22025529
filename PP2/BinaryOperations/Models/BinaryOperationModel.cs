using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BinaryOperations.Web.Models
{
    public class BinaryOperationModel
    {
        [Required(ErrorMessage = "El valor de 'a' es requerido")]
        [StringLength(8, ErrorMessage = "La longitud máxima es de 8 caracteres")]
        [CustomValidation(typeof(BinaryOperationModel), nameof(ValidateBinaryString))]
        public string A { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor de 'b' es requerido")]
        [StringLength(8, ErrorMessage = "La longitud máxima es de 8 caracteres")]
        [CustomValidation(typeof(BinaryOperationModel), nameof(ValidateBinaryString))]
        public string B { get; set; } = string.Empty;

        public bool HasResults { get; set; }
        public ResultTable? Results { get; set; }

        public static ValidationResult? ValidateBinaryString(string value, ValidationContext context)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ValidationResult("El valor no puede estar vacío");
            }

            if (value.Length > 8)
            {
                return new ValidationResult("La longitud no puede exceder los 8 caracteres");
            }

            if (value.Length % 2 != 0)
            {
                return new ValidationResult("La longitud debe ser múltiplo de 2 (2, 4, 6 u 8)");
            }

            if (!Regex.IsMatch(value, "^[01]+$"))
            {
                return new ValidationResult("Solo se permiten los caracteres 0 y 1");
            }

            return ValidationResult.Success;
        }
    }

    public class ResultTable
    {
        public TableRow A { get; set; } = new TableRow();
        public TableRow B { get; set; } = new TableRow();
        public TableRow And { get; set; } = new TableRow();
        public TableRow Or { get; set; } = new TableRow();
        public TableRow Xor { get; set; } = new TableRow();
        public TableRow Sum { get; set; } = new TableRow();
        public TableRow Multiply { get; set; } = new TableRow();
    }

    public class TableRow
    {
        public string Binary { get; set; } = string.Empty;
        public string Octal { get; set; } = string.Empty;
        public string Decimal { get; set; } = string.Empty;
        public string Hexadecimal { get; set; } = string.Empty;
    }
}
