﻿using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Models;
using ApiAluguel.Repositories.Interfaces;
using System.Text.RegularExpressions;
using ApiAluguel.Extensions;

namespace ApiAluguel.Validation;

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
        /*if (ciclista.Nascimento > DateTime.Now.AddYears(-18))
        {
            listaErros.Add(new Erro("422", "O ciclista deve ter pelo menos 18 anos."));
        }*/

        // Validação do CPF
        if(!(ciclista.Cpf is null))
        {
            if (!ciclista.Cpf.ehValido())
            {
                listaErros.Add(new Erro("422", "O CPF não é válido"));
            }
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
        /*if (!Enum.IsDefined(typeof(StatusCiclista), ciclista.Status))
        {
            listaErros.Add(new Erro("422", "O status do ciclista é inválido."));
        }*/

        // Validação da Nacionalidade
        if (!Enum.IsDefined(typeof(NacionalidadeCiclista), ciclista.Nacionalidade))
        {
            listaErros.Add(new Erro("422", "A nacionalidade do ciclista é inválida."));
        }

        if(ciclista.Passaporte is not null){
            if (string.IsNullOrWhiteSpace(ciclista.Passaporte.Numero) || ciclista.Passaporte.Numero.Length > 20)
            {
                listaErros.Add(new Erro("422", "O numero precisa ter no máximo 20 caracteres e não pode ser vazio."));
            }
            if (string.IsNullOrWhiteSpace(ciclista.Passaporte.Pais) || ciclista.Passaporte.Pais.Length > 50)
            {
                listaErros.Add(new Erro("422", "O numero precisa ter no máximo 50 caracteres."));
            }
        }

        return listaErros;
    }
}
