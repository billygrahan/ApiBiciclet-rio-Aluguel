using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces;

public interface ICiclistaRepositorio
{
    Task<Ciclista> Adicionar(Ciclista ciclista);

    Task<Ciclista> BuscarPorId(int id);

    Task<Ciclista> BuscaPorEmail(string email);

    Task<List<Ciclista>> BuscarTodos();

    Task<Ciclista> Atualizar(Ciclista novociclista);

    Task<bool> Apagar(int id);
}
