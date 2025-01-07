using ApiAluguel.Context;
using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiAluguel.Repositories;

public class CiclistaRepositorio : ICiclistaRepositorio
{
    private readonly AppDbContext _appDbContext;

    public CiclistaRepositorio(AppDbContext context)
    {
        _appDbContext = context;
    }

    public async Task<Ciclista> Adicionar(Ciclista ciclista)
    {
        var ciclistaInserido = await _appDbContext.Ciclistas.AddAsync(ciclista);
        await _appDbContext.SaveChangesAsync();

        return ciclistaInserido.Entity;
    }

    public async Task<Ciclista> BuscarPorId(int id)
    {

        var ciclista = await _appDbContext.Ciclistas.Include(c => c.Passaporte).FirstOrDefaultAsync(c => c.Id == id);

        //tratar erros e null!

        return ciclista;
    }

    public async Task<Ciclista> BuscarPorEmail(string email)
    {

        var ciclista = await _appDbContext.Ciclistas.FirstOrDefaultAsync(c => c.Email == email);

        //tratar erros e null!

        return ciclista;
    }

    public async Task<List<Ciclista>> BuscarTodos()
    {
        //tratar erros!
        return await _appDbContext.Ciclistas.ToListAsync();
    }

    public async Task<Ciclista> Atualizar(Ciclista novociclista)
    {
        //tratar erros!
        _appDbContext.Entry(novociclista).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();

        return novociclista;
    }

    public async Task<bool> Apagar(int id)
    {
        var ciclista = await _appDbContext.Ciclistas.FirstOrDefaultAsync(c => c.Id == id);

        //tratar erros e null!

        try
        {
            _appDbContext.Ciclistas.Remove(ciclista);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        catch
        {
            return false;
        }
    }

    public async Task<bool> VerificarCpfEmOutroId(string cpf, int id)
    {
        return await _appDbContext.Ciclistas.AnyAsync(f => f.Cpf == cpf && f.Id != id);

    }
}
