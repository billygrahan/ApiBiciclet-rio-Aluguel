using Aluguel.Context;
using Aluguel.Models;
using Aluguel.Repositories.Interfaces;

namespace Aluguel.Repositories;

public class CiclistaRepositorio : ICiclistaRepositorio
{
    private readonly AppDbContext _appDbContext;


    public CiclistaRepositorio(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<Ciclista> Adicionar(Ciclista ciclista)
    {
        await _appDbContext.Ciclistas.AddAsync(ciclista);
        await _appDbContext.SaveChangesAsync();

        return ciclista;
    }
}
