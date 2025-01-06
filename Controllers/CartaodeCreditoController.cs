using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartaodeCreditoController : ControllerBase
{
    private readonly ICartaodeCreditoRepositorio _cartaoRepositorio;

    public CartaodeCreditoController(ICartaodeCreditoRepositorio cartao)
    {
        _cartaoRepositorio = cartao;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartaoDeCredito>> CadatrarCartao(int id)
    {
        CartaoDeCredito cartao = await _cartaoRepositorio.BuscarPorId(id);

        if (cartao is null) return NotFound(new { codigo = "404", mensagem = $"Cartão com o ID {id} não foi encontrado." });

        return Ok(cartao);
    }

    [HttpPut("{id}")]
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
        cartaoexixtente.Cvv = cartaoatualizado.Cvv;

        cartaoexixtente.Validade = DateTime.SpecifyKind(cartaoexixtente.Validade, DateTimeKind.Utc);

        await _cartaoRepositorio.Atualizar(cartaoexixtente);

        return Ok(cartaoexixtente);
    }
}
