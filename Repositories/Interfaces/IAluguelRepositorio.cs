using ApiAluguel.Models;

namespace ApiAluguel.Repositories.Interfaces
{
    public interface IAluguelRepositorio
    {
        Task<Aluguel> AlugarBicicleta(Aluguel aluguel);

        Task<bool> VerificarSeExisteAluguelAtivo(int ciclistaId);

        Task<Aluguel> PegarAluguelAtivoPorCiclistaId(int ciclistaId);

        Task<Aluguel> BuscarPorId(int id);

        Task<Aluguel> Atualizar(Aluguel aluguel);
    }
}
