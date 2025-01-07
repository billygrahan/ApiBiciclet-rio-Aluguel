using System.ComponentModel.DataAnnotations;

namespace ApiAluguel.Models.RequestsModels;

public class NovoCartaoDeCredito
{
    [StringLength(100, ErrorMessage = "O nome do titular não pode exceder 100 caracteres.")]
    public string NomeTitular { get; set; }

    [RegularExpression(@"\d+", ErrorMessage = "O número do cartão deve conter apenas dígitos.")]
    public string Numero { get; set; }

    [DataType(DataType.Date)]
    public DateTime Validade { get; set; }

    [RegularExpression(@"\d{3,4}", ErrorMessage = "O CVV deve conter 3 ou 4 dígitos.")]
    public string Cvv { get; set; }
}
