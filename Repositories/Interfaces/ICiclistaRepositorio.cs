using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces;

public interface ICiclistaRepositorio
{
    Task<Ciclista> Adicionar(Ciclista ciclista);

    Task<Ciclista> BuscarPorId(int id);

    Task<Ciclista> BuscarPorEmail(string email);

    Task<List<Ciclista>> BuscarTodos();

    Task<Ciclista> Atualizar(Ciclista novociclista);

    Task<bool> Apagar(int id);

    Task<bool> VerificarCpfEmOutroId(string cpf, int id);
}
