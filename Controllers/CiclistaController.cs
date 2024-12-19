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

    /*[HttpPost]
    public async Task<ActionResult<Ciclista>> CadastrarCiclista([FromBody] Ciclista ciclista)
    {
        
        await _ciclistaRepositorio.Adicionar(ciclista);
        

        // Retorna um Status 201 e um header Location com a URL para acessar o funcionario cadastrado 
        return CreatedAtAction(nameof(BuscarCiclistaPorId), new { id = funcionario.Id }, funcionario);
    }*/
}
