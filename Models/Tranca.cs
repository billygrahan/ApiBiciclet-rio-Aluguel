using System.Text.Json.Serialization;

namespace ApiAluguel.Models
{
    public class Tranca
    {
        public int Id { get; set; }
        public int BicicletaIdValidado => _bicicletaId ?? 0;

        [JsonPropertyName("bicicletaId")]
        public int? _bicicletaId { get; set; }
        public int Numero {  get; set; }
        public string Localizacao { get; set; }
        public string AnoDeFabricacao {  get; set; }
        public string Modelo {  get; set; }
        public string Status {  get; set; }
    }
}
