namespace Aluguel.Models
{
    public class Funcionario
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        // Ainda não tenho certeza se é um dado gerado automaticamente ou se é inputado no front
        // Não pode ser modificado
        public string Matricula { get; set; }

        public int Idade {  get; set; }

        public string Funcao { get; set; }
        
        // Precisa ser único no banco
        public string Cpf {  get; set; } 

        // Não coloquei o atributo senha já que não haverá autenticação por enquanto
    }
}
