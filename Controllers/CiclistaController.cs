using Aluguel.Models;
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

    public CiclistaController(ICiclistaRepositorio ciclistaRepositorio)
    {
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
    public async Task<ActionResult<Ciclista>> CadastrarCiclista([FromBody] Ciclista ciclista)
    {
        
        await _ciclistaRepositorio.Adicionar(ciclista);
        

        // Retorna um Status 201 e um header Location com a URL para acessar o funcionario cadastrado 
        return CreatedAtAction(nameof(BuscarCiclistaPorId), new { id = ciclista.Id }, ciclista);
    }
}
