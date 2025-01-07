using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aluguel.Models;


public class Ciclista
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [EnumDataType(typeof(StatusCiclista))]
    public StatusCiclista Status { get; set; }

    public string Nome { get; set; }

    [DataType(DataType.Date)]
    public DateTime Nascimento { get; set; }

    public string? Cpf { get; set; }

    // Relacionamento com o passaporte
    public Passaporte? Passaporte { get; set; }

    [EnumDataType(typeof(NacionalidadeCiclista))]
    public NacionalidadeCiclista Nacionalidade { get; set; }

    public string Email { get; set; }

    public string UrlFotoDocumento { get; set; }
}



// Enum para o Status
public enum StatusCiclista
{
    ATIVO,
    INATIVO,
    AGUARDANDO_CONFIRMACAO
}

// Enum para a Nacionalidade
public enum NacionalidadeCiclista
{
    BRASILEIRO,
    ESTRANGEIRO
}

