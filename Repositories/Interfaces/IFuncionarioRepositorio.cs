using Aluguel.Models;

namespace Aluguel.Repositories.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        Task<List<Funcionario>> Adicionar(Funcionario funcionario);

        Task<Funcionario> BuscarTodos();

        Task<Funcionario> BuscarPorId(int id);

        Task<Funcionario> Atualizar(Funcionario funcionario);

        Task<Funcionario> Apagar(int id);
    }
}
