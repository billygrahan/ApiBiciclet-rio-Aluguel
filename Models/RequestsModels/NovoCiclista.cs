using System.ComponentModel.DataAnnotations;

namespace ApiAluguel.Models.RequestsModels;

public class NovoCiclista
{
    
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

public class Ciclista_Cartao
{
    public NovoCiclista ciclista { get; set; }
    public NovoCartaoDeCredito cartaoDeCredito { get; set; }
}