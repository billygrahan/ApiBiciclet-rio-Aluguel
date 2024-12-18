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

        public async Task<Funcionario> Adicionar(Funcionario funcionario)
        {
            await _appDbContext.Funcionarios.AddAsync(funcionario);
            await _appDbContext.SaveChangesAsync();

            return funcionario;
        }


        public Task<Funcionario> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Funcionario>> BuscarTodos()
        {
            throw new NotImplementedException();
        }
        public Task<Funcionario> Atualizar(Funcionario funcionario, int id)
        {
            throw new NotImplementedException();
        }
        public Task<Funcionario> Apagar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
