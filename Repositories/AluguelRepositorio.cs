using ApiAluguel.Context;
using ApiAluguel.Exceptions;
using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiAluguel.Repositories
{
    public class AluguelRepositorio : IAluguelRepositorio
    {
        private readonly AppDbContext _appDbContext;


        public AluguelRepositorio(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        }

        public async Task<Aluguel> AlugarBicicleta(Aluguel aluguel)
        {
            await _appDbContext.Alugueis.AddAsync(aluguel);
            await _appDbContext.SaveChangesAsync();

            return aluguel;

        }
        public async Task<Aluguel> BuscarPorId(int id)
        {
            var aluguel = await _appDbContext.Alugueis.FirstOrDefaultAsync(x => x.Id == id);

            if (aluguel == null)
            {
                throw new IdNaoEncontradoException($"Nenhum aluguel com o id {id} foi encontrado no banco de dados.");
            }

            return aluguel;
        }

        public async Task<Aluguel> Atualizar(Aluguel aluguel)
        {
            Aluguel aluguelEncontrado = await this.BuscarPorId(aluguel.Id);

            aluguelEncontrado.HoraInicio = aluguel.HoraInicio;
            aluguelEncontrado.HoraFim = aluguel.HoraFim;
            aluguelEncontrado.TrancaInicio = aluguel.TrancaInicio;
            aluguelEncontrado.TrancaFim = aluguel.TrancaFim;
            aluguelEncontrado.Cobranca = aluguel.Cobranca;
            aluguelEncontrado.Ciclista = aluguel.Ciclista;
            aluguelEncontrado.Ativo = aluguel.Ativo;
            aluguelEncontrado.Bicicleta = aluguel.Bicicleta;
            _appDbContext.Alugueis.Update(aluguelEncontrado);
            await _appDbContext.SaveChangesAsync();

            return aluguelEncontrado;
        }


        public async Task<Aluguel> PegarAluguelAtivoPorCiclistaId(int ciclistaId)
        {
           var aluguel = await _appDbContext.Alugueis.FirstOrDefaultAsync(a=> a.Ciclista == ciclistaId && a.Ativo);

            return aluguel;
        }

        public async Task<bool> VerificarSeExisteAluguelAtivo(int ciclistaId)
        {
            return await _appDbContext.Alugueis.AnyAsync(a => a.Ciclista == ciclistaId && a.Ativo);
        }
    }
}
