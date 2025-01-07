using ApiAluguel.Context;
using ApiAluguel.Models;
using ApiAluguel.Repositories.Interfaces;

namespace ApiAluguel.Repositories
{
    public class DevolucaoRepositorio : IDevolucaoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public DevolucaoRepositorio(AppDbContext context) {
            _appDbContext = context;
        }
        public async Task<Devolucao> DevolverBicicleta(Devolucao devolucao)
        {
            await _appDbContext.Devolucoes.AddAsync(devolucao);
            await _appDbContext.SaveChangesAsync();

            return devolucao;
        }
    }
}
