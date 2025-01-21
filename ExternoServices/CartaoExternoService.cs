
using ApiAluguel.ExternoServices.Interfaces;
using System.Text.Json;
using System.Text;
using ApiAluguel.Models.RequestsModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using System.Net.Http;
using Externo.DTO;

namespace ApiAluguel.ExternoServices;

public class CartaoExternoService : ICartaoExternoService
{
    private readonly HttpClient _httpClient;

    public CartaoExternoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ValidaCartao(CartaoDTO cartao)
    {
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(cartao),
            Encoding.UTF8,
            "application/json"
        );
        Console.WriteLine( _httpClient.BaseAddress );
        var response = await _httpClient.PostAsync("validarCartaoDeCredito", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(responseContent);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ArgumentException($"Erro de validação: {errorContent}");
        }

        response.EnsureSuccessStatusCode(); 
        return false;
    }
}

