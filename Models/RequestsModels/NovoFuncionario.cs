using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aluguel.Models.RequestsModels
{
    public class NovoFuncionario
    {

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "O nome do funcionário não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        public int Idade { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Funcao Funcao { get; set; }

        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter exatamente 11 dígitos.")]
        public string Cpf { get; set; }
    }
}
