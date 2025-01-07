using ApiAluguel.Repositories.Interfaces;
using ApiAluguel.Models;
using System.Text.RegularExpressions;

namespace ApiAluguel.Validation;

public class CartaoValidador
{
    public List<Erro> GerarListaErros(CartaoDeCredito cartao)
    {
        List<Erro> listaErros = new List<Erro>();

        if (string.IsNullOrWhiteSpace(cartao.NomeTitular) || cartao.NomeTitular.Length > 100)
        {
            listaErros.Add(new Erro("422", "O nome do titular deve ter no máximo 100 caracteres e não pode ser vazio."));
        }

        if (string.IsNullOrWhiteSpace(cartao.Numero) || !Regex.IsMatch(cartao.Numero, @"^\d+$"))
        {
            listaErros.Add(new Erro("422", "O número do cartão deve conter apenas dígitos e não pode ser vazio."));
        }

        if (cartao.Validade <= DateTime.Now)
        {
            listaErros.Add(new Erro("422", "A validade do cartão deve ser uma data futura."));
        }

        if (string.IsNullOrWhiteSpace(cartao.Cvv) || !Regex.IsMatch(cartao.Cvv, @"^\d{3,4}$"))
        {
            listaErros.Add(new Erro("422", "O CVV deve conter 3 ou 4 dígitos e não pode ser vazio."));
        }

        return listaErros;
    }
}
