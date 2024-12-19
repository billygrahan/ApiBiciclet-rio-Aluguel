using System.ComponentModel.DataAnnotations;

namespace Aluguel.Models
{
    public class Funcionario
    {

        // Chave-primária configurada no map
        public string Matricula { get; set; }

        [StringLength(100, ErrorMessage = "O nome do funcionário não pode exceder 100 caracteres.")]
        public string Nome { get; set; }


        public int Idade {  get; set; }

        public string Funcao { get; set; }

        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter exatamente 11 dígitos.")]
        public string Cpf {  get; set; } 

        // Não coloquei o atributo senha já que não haverá autenticação por enquanto
    }
}
