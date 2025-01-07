using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAluguel.Models;

public class Aluguel
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public int Bicicleta { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime HoraInicio { get; set; }

    [Required]
    public int TrancaFim { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? HoraFim { get; set; }

    [Required]
    public int Cobranca { get; set; }

    [Required]
    public int Ciclista { get; set; }

    [Required]
    public int TrancaInicio { get; set; }

    [JsonIgnore]
    public bool Ativo { get; set; }
}
