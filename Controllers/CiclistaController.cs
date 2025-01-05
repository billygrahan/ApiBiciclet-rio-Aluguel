using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories;
using Aluguel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CiclistaController : ControllerBase
{
    private readonly ICiclistaRepositorio _ciclistaRepositorio;
    private readonly ICartaodeCreditoRepositorio _cartaoRepositorio;

    public CiclistaController(ICiclistaRepositorio ciclistaRepositorio, ICartaodeCreditoRepositorio cartao)
    {
        _cartaoRepositorio = cartao;
        _ciclistaRepositorio = ciclistaRepositorio;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ciclista>> BuscarCiclistaPorId(int id)
    {
        Ciclista ciclista = await _ciclistaRepositorio.BuscarPorId(id);

        if (ciclista == null)
        {
            return NotFound(new { codigo = "404", mensagem = $"Ciclista com o ID {id} não foi encontrado." });
        }


        return Ok(ciclista);
    }

    [HttpPost]
    public async Task<ActionResult<Ciclista>> CadastrarCiclista([FromBody] Ciclista_Cartao ciclistaCartao)
    {
        var novoCiclista = ciclistaCartao.ciclista;
        var novoCartao = ciclistaCartao.cartaoDeCredito;

        // Transformar NovoCiclista em Ciclista
        Ciclista ciclista = new Ciclista
        {
            Status = StatusCiclista.AGUARDANDO_CONFIRMACAO,
            Nome = novoCiclista.Nome,
            Nascimento = novoCiclista.Nascimento,
            Cpf = novoCiclista.Cpf,
            Passaporte = novoCiclista.Passaporte,
            Nacionalidade = novoCiclista.Nacionalidade,
            Email = novoCiclista.Email,
            UrlFotoDocumento = novoCiclista.UrlFotoDocumento
        };

        // Transformar NovoCartaoDeCredito em CartaoDeCredito
        CartaoDeCredito cartao = new CartaoDeCredito
        {
            NomeTitular = novoCartao.NomeTitular,
            Numero = novoCartao.Numero,
            Validade = novoCartao.Validade,
            Cvv = novoCartao.Cvv
        };

        ciclista.Nascimento = DateTime.SpecifyKind(ciclista.Nascimento, DateTimeKind.Utc);
        if (ciclista.Passaporte != null)
        {
            ciclista.Passaporte.Validade = DateTime.SpecifyKind(ciclista.Passaporte.Validade, DateTimeKind.Utc);
        }
        cartao.Validade = DateTime.SpecifyKind(cartao.Validade, DateTimeKind.Utc);

        await _ciclistaRepositorio.Adicionar(ciclista);
        await _cartaoRepositorio.Adicionar(cartao);

        return CreatedAtAction(nameof(BuscarCiclistaPorId), new { id = ciclista.Id }, ciclista);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Ciclista>> AtualizarCiclista(int id, [FromBody] NovoCiclista novoCiclista)
    {
        var ciclistaExistente = await _ciclistaRepositorio.BuscarPorId(id);

        if (ciclistaExistente == null)
        {
            return NotFound(new { codigo = "404", mensagem = $"Ciclista com o ID {id} não foi encontrado." });
        }

        ciclistaExistente.Nome = novoCiclista.Nome;
        ciclistaExistente.Nascimento = novoCiclista.Nascimento;
        ciclistaExistente.Cpf = novoCiclista.Cpf;
        //ciclistaExistente.Passaporte = novoCiclista.Passaporte;
        ciclistaExistente.Nacionalidade = novoCiclista.Nacionalidade;
        ciclistaExistente.Email = novoCiclista.Email;
        ciclistaExistente.UrlFotoDocumento = novoCiclista.UrlFotoDocumento;


        ciclistaExistente.Nascimento = DateTime.SpecifyKind(ciclistaExistente.Nascimento, DateTimeKind.Utc);
        if (ciclistaExistente.Passaporte != null)
        {
            ciclistaExistente.Passaporte.Validade = DateTime.SpecifyKind(ciclistaExistente.Passaporte.Validade, DateTimeKind.Utc);
        }

        await _ciclistaRepositorio.Atualizar(ciclistaExistente);
        
        return Ok(ciclistaExistente);
    }




    [HttpDelete("{id}")]
    public async Task<bool> DeletarCicista(int id)
    {
        return await _ciclistaRepositorio.Apagar(id);
    }
}
