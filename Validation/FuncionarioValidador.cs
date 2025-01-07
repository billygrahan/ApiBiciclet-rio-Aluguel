using ApiAluguel.Extensions;
using ApiAluguel.Models;
using ApiAluguel.Models.RequestsModels;
using ApiAluguel.Repositories.Interfaces;

namespace ApiAluguel.Validation
{
    public class FuncionarioValidador
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioValidador(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public async Task<List<Erro>> GerarListaErros(bool ehAtualizacao, NovoFuncionario funcionario)
        {
            List<Erro> listaErros = new List<Erro>();

            if (!ValidadorPrincipal.NomeEhValido(funcionario.Nome))
            {
                listaErros.Add(new Erro("422", "O nome precisa ter entre 10 e 100 caracteres"));
            }

            if (!ValidadorPrincipal.EmailEhValido(funcionario.Email))
            {
                listaErros.Add(new Erro("422", "O formato do Email não é válido"));
            }
            if (!ValidadorPrincipal.IdadeEhValida(funcionario.Idade))
            {
                listaErros.Add(new Erro("422", "O funcionário precisa ter 18 ou mais anos de idade"));
            }

            if (!funcionario.Cpf.ehValido())
            {
                listaErros.Add(new Erro("422", "O CPF fornecido não é válido"));
            }
            // Caso a validação seja em update funcionario, é verificado se o CPF já existe em outro usuário de Id diferente o inserido na URL
            // Caso a validação seja no cadastro é verificado se qualquer usuario tem o mesmo CPF
            else
            {
                var cpfEmUso = ehAtualizacao
                ? await _funcionarioRepositorio.VerificarCpfEmOutroId(funcionario.Cpf, funcionario.Id)
                : await _funcionarioRepositorio.VerificarCPF(funcionario.Cpf);

                if (cpfEmUso)
                {
                    listaErros.Add(new Erro("409", "O CPF fornecido já está em uso"));
                }
            }


            return listaErros;
        }
    }
}
