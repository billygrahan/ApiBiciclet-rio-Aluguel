using System.ComponentModel.DataAnnotations;

namespace ApiAluguel.Models.RequestsModels;

public class NovoCartaoDeCredito
{
    public string NomeTitular { get; set; }

    public string Numero { get; set; }

    [DataType(DataType.Date)]
    public DateTime Validade { get; set; }

    public string Cvv { get; set; }
}
