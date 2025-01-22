using ApiAluguel.APIs;
using ApiAluguel.Models;
using ApiAluguel.Services.Interfaces;

namespace ApiAluguel.Services
{

    public class EquipamentoService : IEquipamentoService
    {
        private readonly EquipamentoAPI _equipamentoAPI;

        public EquipamentoService(EquipamentoAPI equipamentoAPI)
        {
            _equipamentoAPI = equipamentoAPI;
        }

        public async Task<Tranca> DestrancarBicicleta(int idTranca, int idBicicleta)
        {
           return await _equipamentoAPI.ModificarTranca(idTranca, idBicicleta, false);
        }
        public async Task<Tranca> TrancarBicicleta(int idTranca, int idBicicleta)
        {
            return await _equipamentoAPI.ModificarTranca(idTranca, idBicicleta, true);
        }

        public async Task<Bicicleta> PegarBicicleta(int idBicicleta)
        {
            return await _equipamentoAPI.PegarBicicleta(idBicicleta);
        }

        public async Task<Tranca> PegarTranca(int idTranca)
        {
            return await _equipamentoAPI.PegarTranca(idTranca);
        }

    }
}
