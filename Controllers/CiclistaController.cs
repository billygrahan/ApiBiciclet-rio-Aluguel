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
    public async Task<ActionResult<Ciclista>> CadastrarCiclista([FromBody] Ciclista_Cartao ciclista_cartao)
    {
        var newciclista = ciclista_cartao.ciclista;
        var newcartao = ciclista_cartao.cartaoDeCredito;
        Ciclista ciclista = new Ciclista
        {
            Status = StatusCiclista.AGUARDANDO_CONFIRMACAO,
            Nome = newciclista.Nome,
            Nascimento = newciclista.Nascimento,
            Cpf = newciclista.Cpf,
            Passaporte = newciclista.Passaporte,
            Nacionalidade = newciclista.Nacionalidade,
            Email = newciclista.Email,
            UrlFotoDocumento = newciclista.UrlFotoDocumento
        };
        CartaoDeCredito cartao = new CartaoDeCredito
        {
            NomeTitular = newcartao.NomeTitular,
            Numero = newcartao.Numero,
            Validade = newcartao.Validade,
            Cvv = newcartao.Cvv
        };
        
        ciclista.Nascimento = DateTime.SpecifyKind(ciclista.Nascimento, DateTimeKind.Utc);

        if (ciclista.Passaporte != null)
        {
            ciclista.Passaporte.Validade = DateTime.SpecifyKind(ciclista.Passaporte.Validade, DateTimeKind.Utc);
        }

        cartao.Validade = DateTime.SpecifyKind(cartao.Validade, DateTimeKind.Utc);


        
        await _ciclistaRepositorio.Adicionar(ciclista);
        await _cartaoRepositorio.Adicionar(cartao);


        // Retorna um Status 201 e um header Location com a URL para acessar o funcionario cadastrado 
        return CreatedAtAction(nameof(BuscarCiclistaPorId), new { id = ciclista.Id }, ciclista);
    }

    [HttpPut]




    //[HttpDelete("{id}")]
    /*public async Task<ActionResult> DeletarCicista(int id)
    {

    }*/
}
