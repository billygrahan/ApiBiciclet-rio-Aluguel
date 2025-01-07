using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAluguel.Models
{

    public class Funcionario
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Matricula { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        public string Nome { get; set; }

        public int Idade {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Funcao Funcao { get; set; }

        public string Cpf {  get; set; }

        // Não coloquei o atributo senha já que não haverá autenticação por enquanto



    }
}
