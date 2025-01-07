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
    private readonly IAluguelRepositorio _aluguelRepositorio;
    private readonly CiclistaValidador _ciclistaValidador;
    private readonly CartaoValidador _cartaoValidador;

    public CiclistaController(ICiclistaRepositorio ciclistaRepositorio, ICartaodeCreditoRepositorio cartao, IAluguelRepositorio aluguelRepositorio,
        CiclistaValidador ciclistaValidador, CartaoValidador cartaovalidador)
    {
        _cartaoRepositorio = cartao;
        _ciclistaRepositorio = ciclistaRepositorio;
        _aluguelRepositorio = aluguelRepositorio;
        _ciclistaValidador = ciclistaValidador;
        _cartaoValidador = cartaovalidador;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ciclista))]
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

        List<Erro> listaErros = _ciclistaValidador.GerarListaErros(ciclista);
        List<Erro> listaErros2 = _cartaoValidador.GerarListaErros(cartao);
        if (listaErros.Count > 0 || listaErros2.Count > 0)
        {
            listaErros.AddRange(listaErros2);
            return StatusCode(422, listaErros);
        }

        var ciclistaInserido = await _ciclistaRepositorio.Adicionar(ciclista);
        await _cartaoRepositorio.Adicionar(cartao);

        return CreatedAtAction(nameof(BuscarCiclistaPorId), new { id = ciclista.Id }, ciclistaInserido);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ciclista))]
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

        List<Erro> listaErros = _ciclistaValidador.GerarListaErros(ciclistaExistente);
        if (listaErros.Count > 0)
        {
            return StatusCode(422, listaErros);
        }

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

    [HttpGet("{id}/permiteAluguel")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<ActionResult<bool>> PermiteAluguel(int id)
    {
        var ciclista = await _ciclistaRepositorio.BuscarPorId(id);
        if(ciclista == null)
            return NotFound(new Erro("404", "Nenhum ciclista com o Id especificado foi encontrado."));

        bool temAluguel = await _aluguelRepositorio.VerificarSeExisteAluguelAtivo(id);

        return Ok(!temAluguel);
    }

    [HttpGet("{idCiclista}/bicicletaAlugada")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bicicleta))]
    public async Task<ActionResult<Bicicleta>> ObterBicicleta(int idCiclista)
    {
        var ciclista = await _ciclistaRepositorio.BuscarPorId(idCiclista);
        if (ciclista == null)
            return NotFound(new Erro("404", "Nenhum ciclista com o Id especificado foi encontrado."));

        Aluguel aluguel = await _aluguelRepositorio.PegarAluguelAtivoPorCiclistaId(idCiclista);

        // Ciclista sem Aluguel/Bicicleta 
        if(aluguel == null)
            return Ok(new { });

        // Integração para pegar Bicicleta usando Id deve acontecer aqui
        Bicicleta bicicleta = new Bicicleta
        {
            Id = aluguel.Bicicleta,
            Marca = "Caloi",
            Modelo = "Bicicleta Placeholder",
            Numero = 1,
            Status = "EM_USO"

        };

        return Ok(bicicleta);
    }
}
