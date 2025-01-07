using Aluguel.Models.RequestsModels;
using Aluguel.Models;
using Aluguel.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace Aluguel.Validation;

public class CiclistaValidador
{
    private readonly ICiclistaRepositorio _ciclistaRepositorio;

    public CiclistaValidador(ICiclistaRepositorio ciclistaRepositorio)
    {
        _ciclistaRepositorio = ciclistaRepositorio;
    }

    public List<Erro> GerarListaErros(Ciclista ciclista)
    {
        List<Erro> listaErros = new List<Erro>();

        // Validação do Nome
        if (string.IsNullOrWhiteSpace(ciclista.Nome) || ciclista.Nome.Length > 100)
        {
            listaErros.Add(new Erro("422", "O nome precisa ter no máximo 100 caracteres e não pode ser vazio."));
        }

        // Validação da Data de Nascimento
        if (ciclista.Nascimento > DateTime.Now.AddYears(-18))
        {
            listaErros.Add(new Erro("422", "O ciclista deve ter pelo menos 18 anos."));
        }

        // Validação do CPF
        if (!string.IsNullOrWhiteSpace(ciclista.Cpf) && !Regex.IsMatch(ciclista.Cpf, @"^\d{11}$"))
        {
            listaErros.Add(new Erro("422", "O CPF deve conter exatamente 11 dígitos."));
        }

        // Validação do Email
        if (!ValidadorPrincipal.EmailEhValido(ciclista.Email))
        {
            listaErros.Add(new Erro("422", "O e-mail fornecido é inválido."));
        }

        // Validação da URL da Foto do Documento
        if (!ValidadorPrincipal.UrlEhValida(ciclista.UrlFotoDocumento))
        {
            listaErros.Add(new Erro("422", "A URL da foto do documento é inválida."));
        }

        // Validação do Status
        if (!Enum.IsDefined(typeof(StatusCiclista), ciclista.Status))
        {
            listaErros.Add(new Erro("422", "O status do ciclista é inválido."));
        }

        // Validação da Nacionalidade
        if (!Enum.IsDefined(typeof(NacionalidadeCiclista), ciclista.Nacionalidade))
        {
            listaErros.Add(new Erro("422", "A nacionalidade do ciclista é inválida."));
        }

        return listaErros;
    }
}
