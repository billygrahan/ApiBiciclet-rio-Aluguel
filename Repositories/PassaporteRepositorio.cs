using ApiAluguel.Context;
using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiAluguel.Repositories;

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
