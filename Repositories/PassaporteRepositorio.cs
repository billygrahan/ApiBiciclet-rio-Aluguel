using Aluguel.Context;
using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Repositories;

public class PassaporteRepositorio : IPassaporteRepositorio
{
    private readonly AppDbContext _appDbContext;

    public PassaporteRepositorio(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<Passaporte> Adicionar(Passaporte passaporte)
    {
        await _appDbContext.Passaporte.AddAsync(passaporte);
        await _appDbContext.SaveChangesAsync();
        return passaporte;
    }

    public async Task<Passaporte> BuscarPorId(int id)
    {
        var passaporte = await _appDbContext.Passaporte.FirstOrDefaultAsync(c => c.CiclistaId == id);

        return passaporte;
    }

    public async Task<Passaporte> Atualizar(Passaporte passaporte)
    {
        _appDbContext.Entry(passaporte).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();

        return passaporte;
    }
}
