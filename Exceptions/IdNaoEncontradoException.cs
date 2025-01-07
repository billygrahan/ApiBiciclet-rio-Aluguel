namespace ApiAluguel.Exceptions
{
    public class IdNaoEncontradoException : Exception
    {
        public IdNaoEncontradoException()
        {
        }

        public IdNaoEncontradoException(string mensagem)
            : base(mensagem)
        {
        }

        public IdNaoEncontradoException(string mensagem, Exception innerException)
            : base(mensagem, innerException)
        {
        }
    }
}
