using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aluguel.Models;


public class Ciclista
{
    [Key]
    public int Id { get; set; }

    [Required]
    [EnumDataType(typeof(StatusCiclista))]
    public StatusCiclista Status { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
    public string Nome { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Nascimento { get; set; }

    [Required]
    [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter exatamente 11 dígitos.")]
    public string? Cpf { get; set; }

    // Relacionamento com o passaporte
    public Passaporte? Passaporte { get; set; }

    [Required]
    [EnumDataType(typeof(NacionalidadeCiclista))]
    public NacionalidadeCiclista Nacionalidade { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }

    [Required]
    [Url(ErrorMessage = "URL inválida.")]
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

