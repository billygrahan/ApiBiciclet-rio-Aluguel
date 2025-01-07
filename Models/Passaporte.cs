using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAluguel.Models;

public class Passaporte
{
    [Key]
    [StringLength(20, ErrorMessage = "O número do passaporte não pode exceder 20 caracteres.")]
    public string Numero { get; set; }

    [DataType(DataType.Date)]
    public DateTime Validade { get; set; }

    [StringLength(50, ErrorMessage = "O país não pode ter mais que 50 caracteres.")]
    public string Pais { get; set; }

    // Chave estrangeira para associar ao Ciclista
    [JsonIgnore]
    [ForeignKey("Ciclista")]
    public int CiclistaId { get; set; }

    [JsonIgnore]
    public Ciclista? Ciclista { get; set; }
}
