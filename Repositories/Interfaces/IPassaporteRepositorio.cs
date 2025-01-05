using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces;

public interface IPassaporteRepositorio
{
    Task<Passaporte> Adicionar(Passaporte passaporte);

    Task<Passaporte> BuscarPorId(int id);

    Task<Passaporte> Atualizar(Passaporte passaporte);
}
