using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories;
using ApiAluguel.Validation;
using ApiAluguel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CiclistaController : ControllerBase
{
    private readonly ICiclistaRepositorio _ciclistaRepositorio;
    private readonly ICartaodeCreditoRepositorio _cartaoRepositorio;
    private readonly CiclistaValidador _ciclistavalidador;

    public CiclistaController(ICiclistaRepositorio ciclistaRepositorio, ICartaodeCreditoRepositorio cartao, CiclistaValidador ciclistavalidador)
    {
        _cartaoRepositorio = cartao;
        _ciclistaRepositorio = ciclistaRepositorio;
        _ciclistavalidador = ciclistavalidador;
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
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ciclista))]
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

        List<Erro> listaErros = _ciclistavalidador.GerarListaErros(ciclista);
        if (listaErros.Count > 0)
            return StatusCode(422, listaErros);

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
        ciclistaExistente.Nascimento = DateTime.SpecifyKind(novoCiclista.Nascimento, DateTimeKind.Utc);
        ciclistaExistente.Cpf = novoCiclista.Cpf;
        ciclistaExistente.Passaporte = novoCiclista.Passaporte;
        ciclistaExistente.Nacionalidade = novoCiclista.Nacionalidade;
        ciclistaExistente.Email = novoCiclista.Email;
        ciclistaExistente.UrlFotoDocumento = novoCiclista.UrlFotoDocumento;

        await _ciclistaRepositorio.Atualizar(ciclistaExistente);

        return Ok(ciclistaExistente);
    }

    [HttpPost("{id}/ativar")]
    public async Task<ActionResult<Ciclista>> AtivarCadastro(int id)
    {
        var ciclistaExistente = await _ciclistaRepositorio.BuscarPorId(id);

        if (ciclistaExistente == null)
        {
            return NotFound(new { codigo = "404", mensagem = $"Ciclista com o ID {id} não foi encontrado." });
        }

        ciclistaExistente.Status = StatusCiclista.ATIVO;

        await _ciclistaRepositorio.Atualizar(ciclistaExistente);

        return Ok(ciclistaExistente);
    }

    [HttpGet("existeEmail/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Erro))]
    public async Task<ActionResult<bool>> ExisteEmail(string email)
    {
        if (email is null)
        {
            return BadRequest(new Erro("400", "Email não enviado como parâmetro"));
        }
        var ciclistaexistente = await _ciclistaRepositorio.BuscarPorEmail(email);

        if (ciclistaexistente == null) { return false; }
        else return true;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeletarCicista(int id)
    {
        return await _ciclistaRepositorio.Apagar(id);
    }
}
