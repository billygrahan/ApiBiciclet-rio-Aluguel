using System.Text.RegularExpressions;

namespace ApiAluguel.Validation
{
    public class ValidadorPrincipal
    {
        public static bool NomeEhValido(string nome)
        {
            return nome.Length > 10 && nome.Length <= 100;
        }

        public static bool EmailEhValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        public static bool IdadeEhValida(int idade)
        {
            return idade >= 18;
        }

        public static bool UrlEhValida(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        // Por enquanto não validei função já que está mapeada como um Enum


    }
}
