using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using ApiAluguel.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartaodeCreditoController : ControllerBase
{
    private readonly ICartaodeCreditoRepositorio _cartaoRepositorio;
    private readonly CartaoValidador _cartaoValidador;

    public CartaodeCreditoController(ICartaodeCreditoRepositorio cartao, 
        CartaoValidador cartaoValidador)
    {
        _cartaoRepositorio = cartao;
        _cartaoValidador = cartaoValidador;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartaoDeCredito>> CadatrarCartao(int id)
    {
        CartaoDeCredito cartao = await _cartaoRepositorio.BuscarPorId(id);

        if (cartao is null) return NotFound(new { codigo = "404", mensagem = $"Cartão com o ID {id} não foi encontrado." });

        return Ok(cartao);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartaoDeCredito))]
    public async Task<ActionResult<CartaoDeCredito>> AtualizaCartao(int id, [FromBody] NovoCartaoDeCredito cartaoatualizado)
    {
        var cartaoexixtente = await _cartaoRepositorio.BuscarPorId(id);

        if (cartaoexixtente is null)
        {
            return NotFound(new { codigo = "404", mensagem = $"Cartão com o ID {id} não foi encontrado." });
        }

        cartaoexixtente.NomeTitular = cartaoatualizado.NomeTitular;
        cartaoexixtente.Numero = cartaoatualizado.Numero;
        cartaoexixtente.Validade = cartaoatualizado.Validade;
        cartaoexixtente.Validade = DateTime.SpecifyKind(cartaoexixtente.Validade, DateTimeKind.Utc);
        cartaoexixtente.Cvv = cartaoatualizado.Cvv;

        List<Erro> listaErros = await _cartaoValidador.GerarListaErros(cartaoexixtente);
        if (listaErros.Count > 0)
        {
            return StatusCode(422, listaErros);
        }

        await _cartaoRepositorio.Atualizar(cartaoexixtente);

        return Ok(cartaoexixtente);
    }
}
