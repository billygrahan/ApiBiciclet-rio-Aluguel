using Aluguel.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartaodeCreditoController : ControllerBase
{
    private readonly ICartaodeCreditoRepositorio _cartaoRepositorio;

    public CartaodeCreditoController(ICartaodeCreditoRepositorio cartao)
    {
        _cartaoRepositorio = cartao;
    }


}
