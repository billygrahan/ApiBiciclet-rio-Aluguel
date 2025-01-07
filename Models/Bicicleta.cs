using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAluguel.Models
{
    public class Bicicleta
    {
        [Key]
        public int Id { get; set; }

        public string Marca {  get; set; }

        public string Modelo {  get; set; }

        public int Numero {  get; set; }

        public string Status { get; set; }
    }
}
