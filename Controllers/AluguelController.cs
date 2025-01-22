using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using ApiAluguel.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiAluguel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AluguelController : ControllerBase
    {
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ICiclistaRepositorio _ciclistaRepositorio;
        private readonly IEquipamentoService _equipamentoService;

        public AluguelController(IAluguelRepositorio aluguelRepositorio, ICiclistaRepositorio ciclistaRepositorio, IEquipamentoService equipamentoService)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _ciclistaRepositorio = ciclistaRepositorio;
            _equipamentoService = equipamentoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Aluguel))]
        public async Task<ActionResult<Aluguel>> AlugarBicicleta([FromBody] NovoAluguel novoAluguel) {

            Ciclista ciclista = await _ciclistaRepositorio.BuscarPorId(novoAluguel.ciclista);

            List<Erro> erros = new List<Erro>();

            if (ciclista.Status != StatusCiclista.ATIVO)
                erros.Add(new Erro("422", "O ciclista precisa ter o status ativo para realizar um aluguel"));

            bool temAluguelAtivo = await _aluguelRepositorio.VerificarSeExisteAluguelAtivo(novoAluguel.ciclista);


            // Será preciso Mandar um email para o ciclista com todos os dados
            // do aluguel em andamento no caso abaixo
            if (temAluguelAtivo)
                erros.Add(new Erro("422", "O ciclista já possui um aluguel em andamento e não pode pegar outra bicicleta."));
            if (erros.Count > 0)
                return StatusCode(422, erros);




            try
            {
                var trancaResponse = await _equipamentoService.PegarTranca(novoAluguel.trancaInicio);

                // Tratar os outros fluxos de exceção/alternativo que fazem parte da integração

                // Integrações devem acontecer aqui para obter os ID's de Cobranca, Bicicleta
                // Deverá ser realizado uma Cobrança

                // Valores aleatórios para enquanto não é realizado as integrações
                Random random = new Random();

                Console.WriteLine(trancaResponse._bicicletaId);
                Console.WriteLine(trancaResponse.BicicletaIdValidado);

                Aluguel aluguel = new Aluguel
                {

                    Bicicleta = trancaResponse.BicicletaIdValidado,
                    HoraInicio = DateTime.UtcNow,
                    // HoraFim com valor placeholder
                    HoraFim = DateTime.MinValue,
                    TrancaInicio = novoAluguel.trancaInicio,
                    // TrancaFim precisará ser atualizada na devolução, assim como HoraFim
                    TrancaFim = 0,
                    Cobranca = random.Next(1, 21),
                    Ciclista = novoAluguel.ciclista,
                    Ativo = true
                };

                var trancaAtualizada = await _equipamentoService.DestrancarBicicleta(novoAluguel.trancaInicio, trancaResponse.BicicletaIdValidado);

                aluguel = await _aluguelRepositorio.AlugarBicicleta(aluguel);

                // Depois de realizar o aluguel será preciso mudar os status da Tranca e da Bicicleta


                // Também será necessário enviar um email para o Ciclista com todos os dados do aluguel

                return Ok(aluguel);
                }
            catch (Exception ex) {
                // Temporário
                return BadRequest(ex.Message);
            }
        }
    }
}
