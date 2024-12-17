using Aluguel.Context;
using Aluguel.Models;
using Aluguel.Repositories.Interfaces;

namespace Aluguel.Repositories
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AppDbContext _appDbContext;


        public FuncionarioRepositorio(AppDbContext context)
        {
            _appDbContext = context;
        }

        public Task<List<Funcionario>> Adicionar(Funcionario funcionario)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> Atualizar(Funcionario funcionario)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Funcionario> BuscarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
