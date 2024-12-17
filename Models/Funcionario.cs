namespace Aluguel.Models
{
    public class Funcionario
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public string Matricula { get; set; }

        public int Idade {  get; set; }

        public string Funcao { get; set; }
        
        public string Cpf {  get; set; } 

        // Não coloquei o atributo senha já que não haverá autenticação por enquanto
    }
}
