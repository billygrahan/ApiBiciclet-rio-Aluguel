using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces;

public interface IFuncionarioRepositorio
{
    Task<Funcionario> Adicionar(Funcionario funcionario);

    Task<List<Funcionario>> BuscarTodos();

    Task<Funcionario> BuscarPorId(int id);

    Task<Funcionario> Atualizar(Funcionario funcionario, int id);

    Task<bool> Apagar(int id);
}
