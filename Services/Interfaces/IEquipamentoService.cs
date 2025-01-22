using ApiAluguel.Models;

namespace ApiAluguel.Services.Interfaces
{
    public interface IEquipamentoService
    {
        Task<Tranca> TrancarBicicleta(int idTranca, int idBicicleta);
        Task<Tranca> DestrancarBicicleta(int idTranca, int idBicicleta);
        Task<Bicicleta> PegarBicicleta(int idBicicleta);
        Task<Tranca> PegarTranca(int idTranca);
    }
}
