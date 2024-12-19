using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aluguel.Models;

public class Passaporte
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "O número do passaporte não pode exceder 20 caracteres.")]
    public string Numero { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Validade { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "O país não pode ter mais que 50 caracteres.")]
    public string Pais { get; set; }

    // Chave estrangeira para associar ao Ciclista
    [ForeignKey("Ciclista")]
    public int CiclistaId { get; set; }
    public Ciclista Ciclista { get; set; }
}
