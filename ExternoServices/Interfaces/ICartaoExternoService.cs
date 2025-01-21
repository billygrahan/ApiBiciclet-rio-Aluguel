using ApiAluguel.Models.RequestsModels;
using Externo.DTO;

namespace ApiAluguel.ExternoServices.Interfaces;

public interface ICartaoExternoService
{
    Task<bool> ValidaCartao(CartaoDTO cartao);
}
