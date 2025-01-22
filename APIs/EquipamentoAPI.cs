using ApiAluguel.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;

namespace ApiAluguel.APIs
{
    public class EquipamentoAPI
    {
        private readonly HttpClient _client;
        private readonly EquipamentoAPISettings _equipamentoAPISettings;


        public EquipamentoAPI(HttpClient client, IOptions<EquipamentoAPISettings> equipamentoAPISettings)
        {
            _client = client;
            _equipamentoAPISettings = equipamentoAPISettings.Value;
            _client.BaseAddress = new Uri(_equipamentoAPISettings.BaseAddress);

        }

        public async Task<Bicicleta> PegarBicicleta(int idBicicleta)
        {
            var response = await _client.GetAsync($"bicicleta/{idBicicleta}");

            if (response.IsSuccessStatusCode)
            {
                var bicicleta = await response.Content.ReadFromJsonAsync<Bicicleta>();

                if (bicicleta != null)
                {
                    return bicicleta;
                }
                else
                {
                    throw new Exception($"A resposta da API Get Bicicleta {idBicicleta} é nula.");
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Erro na Requisição. Código de Status {response.StatusCode}: {errorContent}");
            }
        }

        public async Task<Tranca> PegarTranca(int idTranca)
        {
            var response = await _client.GetAsync($"tranca/{idTranca}");

            if (response.IsSuccessStatusCode)
            {
                var tranca = await response.Content.ReadFromJsonAsync<Tranca>();

                if (tranca != null)
                {
                    return tranca;
                }
                else
                {
                    throw new Exception($"A resposta da API Get Tranca {idTranca} é nula.");
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Erro na Requisição. Código de Status {response.StatusCode}: {errorContent}");
            }
        }

        public async Task<Tranca> ModificarTranca(int idTranca, int idBicicleta, bool trancar)
        {
            var payload = new StringContent(idBicicleta.ToString(), Encoding.UTF8, "application/json");

            string url = trancar ? $"tranca/{idTranca}/trancar" : $"tranca/{idTranca}/destrancar";

            var response = await _client.PostAsync(url, payload);

            if (response.IsSuccessStatusCode)
            {
                var tranca = await response.Content.ReadFromJsonAsync<Tranca>();


                if (tranca != null)
                {
                    return tranca;
                }
                else
                {
                    throw new Exception($"A resposta da API Post Trancar/Destrancar {idTranca} é nula.");
                }

            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Erro na Requisição. Código de Status {response.StatusCode}: {errorContent}");
            }

        }
    }



}
