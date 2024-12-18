using System.ComponentModel.DataAnnotations;

namespace Aluguel.Models;

public class Aluguel
{
    [Key]
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
}
