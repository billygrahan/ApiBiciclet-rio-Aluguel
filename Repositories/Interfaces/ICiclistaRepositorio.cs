using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces;

public interface ICiclistaRepositorio
{
    Task<Ciclista> Adicionar(Ciclista ciclista);

    //Task<Ciclista> BuscarPorId(int id);
}
