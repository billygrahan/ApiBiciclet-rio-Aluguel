using Aluguel.Models;

namespace Aluguel.Tools
{
    public class GeradorMatricula
    {
        public static string GerarMatricula(Funcao f)
        {
            var matricula = f.ToString().Substring(0,3)+ Guid.NewGuid().ToString().Substring(0, 8);
            return matricula;
        }
    }
}
