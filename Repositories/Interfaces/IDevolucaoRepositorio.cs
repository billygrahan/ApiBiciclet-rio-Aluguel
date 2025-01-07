using ApiAluguel.Models;

namespace ApiAluguel.Repositories.Interfaces
{
    public interface IDevolucaoRepositorio
    {
        Task<Devolucao> DevolverBicicleta(Devolucao devolucao);
    }
}
