using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using ApiAluguel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAluguel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucaoController : ControllerBase
    {
        private readonly IDevolucaoRepositorio _devolucaoRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IEquipamentoService _equipamentoService;

        public DevolucaoController(IDevolucaoRepositorio devolucaoRepositorio, IAluguelRepositorio aluguelRepositorio, IEquipamentoService equipamentoService)
        {
            _devolucaoRepositorio = devolucaoRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
            _equipamentoService = equipamentoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Devolucao))]
        public async Task<ActionResult<Devolucao>> DevolverBicicleta([FromBody] NovoDevolucao novoDevolucao)
        {
            // Integração:
            // Tratar fluxos de exceção/alternativos da integração
            // Deverá ser calculado as Taxas Extras se o tempo do aluguel passar das 2 horas fixas.
            // Os status da tranca e da bicicleta precisarão ser alterados 
            try
            {
                var aluguel = await _aluguelRepositorio.PegarAluguelAtivoPorCiclistaId(novoDevolucao.ciclista);


                var tranca = await _equipamentoService.TrancarBicicleta(novoDevolucao.trancaFim, aluguel.Bicicleta);

                var horaFim = DateTime.UtcNow;

                Devolucao devolucao = new Devolucao
                {
                    Ciclista = novoDevolucao.ciclista,
                    TrancaFim = novoDevolucao.trancaFim,
                    Bicicleta = aluguel.Bicicleta,
                    HoraInicio = aluguel.HoraInicio,
                    HoraFim = horaFim,
                    Cobranca = aluguel.Cobranca
                };

                await _devolucaoRepositorio.DevolverBicicleta(devolucao);

                // Atualiza Aluguel com horaFim e TrancaFim e coloca ele como Inativo
                aluguel.HoraFim = horaFim;
                aluguel.TrancaFim = novoDevolucao.trancaFim;
                aluguel.Ativo = false;

                await _aluguelRepositorio.Atualizar(aluguel);

                return Ok(devolucao);

            }
            catch (Exception ex) { 
                // Temporário
                return BadRequest(ex.Message);
            }
        }
    }
}
