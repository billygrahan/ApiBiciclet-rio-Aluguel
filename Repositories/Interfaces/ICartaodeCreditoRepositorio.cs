using ApiAluguel.Models;

namespace ApiAluguel.Repositories.Interfaces;

public interface ICartaodeCreditoRepositorio
{
    Task<CartaoDeCredito> Adicionar(CartaoDeCredito cartaodecredito);

    Task<CartaoDeCredito> BuscarPorId(int id);

    Task<List<CartaoDeCredito>> BuscarTodos();

    Task<CartaoDeCredito> Atualizar(CartaoDeCredito novocartaodecredito);

    Task<bool> Apagar(int id);
}
