using Aluguel.Exceptions;
using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories.Interfaces;
using Aluguel.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Funcionario>>> BuscarTodosFuncionarios()
        {
            List<Funcionario> funcionarios = await _funcionarioRepositorio.BuscarTodos();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        public async Task<ActionResult<Funcionario>> BuscarFuncionarioPorId(int id)
        {

            if (id <= 0)
                return StatusCode(422,new Erro("404", "O id inserido não é válido"));

           
            try
            {
                Funcionario funcionario = await _funcionarioRepositorio.BuscarPorId(id);
                return Ok(funcionario);
            }
            catch (IdNaoEncontradoException ex)
            {
                return NotFound(new Erro("404", ex.Message));
            }
         

        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> CadastrarFuncionario([FromBody] NovoFuncionario novofuncionario)
        {

            // A fazer: separar criação de model do dominio em alguma outra parte do projeto
            Funcionario funcionario = new Funcionario
            {
                Email = novofuncionario.Email,
                Matricula = GeradorMatricula.GerarMatricula(novofuncionario.Funcao),
                Nome = novofuncionario.Nome,
                Idade = novofuncionario.Idade,
                Funcao = novofuncionario.Funcao,
                Cpf = novofuncionario.Cpf
            };

            try
            {
                await _funcionarioRepositorio.Adicionar(funcionario);
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                // DbUpdateException é lançada por SaveChanges() no repositório e encapsula uma
                // exceção específica (InnerException) do BD usado


                if (ex.InnerException.Message.Contains("UNIQUE"))
                {
                    return StatusCode(422, new { codigo = "422", mensagem = "Já existe um funcionário cadastrado com a matrícula informada" });
                }

                return BadRequest(new { mensagem = "O funcionário não pode ser cadastrado" });

            }

            // Retorna um Status 201 e um header Location com a URL para acessar o funcionario cadastrado 
            return CreatedAtAction(nameof(BuscarFuncionarioPorId), new { id = funcionario.Id }, funcionario);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        public async Task<ActionResult<Funcionario>> AtualizarFuncionario([FromBody] NovoFuncionario novofuncionario, int id)
        {


            if (id <= 0)
                return StatusCode(422, new { mensagem = "O id inserido na URL não é válido" });

            Funcionario funcionarioAtualizado;

            try
            {
                funcionarioAtualizado = await _funcionarioRepositorio.Atualizar(novofuncionario, id);
                return Ok(funcionarioAtualizado);

            }
            catch (IdNaoEncontradoException ex)
            {
                return NotFound(new Erro("404", ex.Message));
            }
            catch (DbUpdateException ex) when (ex.InnerException != null)
            {
                if (ex.InnerException.Message.Contains("UNIQUE")){
                    return StatusCode(422, new { codigo = "422", mensagem = "Já existe um funcionário cadastrado com a matrícula informada" });
                }

                return BadRequest(new {mensagem = "O funcionário não pode ser atualizado"});
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeletarFuncionario(int id)
        {

            if (id <= 0)
                return StatusCode(422, new { mensagem = "O id inserido na URL não é válido" });

            try
            {
                await _funcionarioRepositorio.Apagar(id);
                return Ok();
            }catch(IdNaoEncontradoException ex)
            {
                return NotFound(new Erro("404",ex.Message));
            }
        }

    }
}
