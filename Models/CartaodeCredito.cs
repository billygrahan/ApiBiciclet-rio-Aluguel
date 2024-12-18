using System.ComponentModel.DataAnnotations;

namespace Aluguel.Models;

public class CartaoDeCredito
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O nome do titular não pode exceder 100 caracteres.")]
    public string NomeTitular { get; set; }

    [Required]
    [RegularExpression(@"\d+", ErrorMessage = "O número do cartão deve conter apenas dígitos.")]
    public string Numero { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Validade { get; set; }

    [Required]
    [RegularExpression(@"\d{3,4}", ErrorMessage = "O CVV deve conter 3 ou 4 dígitos.")]
    public string Cvv { get; set; }
}
