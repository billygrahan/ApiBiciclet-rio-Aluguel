using Aluguel.Models;
using Aluguel.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Funcionario>> BuscarFuncionarioPorId(int id)
        {

            if (id <= 0)
                return StatusCode(422, new { mensagem = "O id inserido na URL não é válido"});

            Funcionario funcionario = await _funcionarioRepositorio.BuscarPorId(id);
            if (funcionario == null)
            {
                return NotFound(new { codigo = "404", mensagem = $"Funcionário com o ID {id} não foi encontrado." });
            }
            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> CadastrarFuncionario([FromBody] Funcionario funcionario)
        {
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
        public async Task<ActionResult<Funcionario>> AtualizarFuncionario([FromBody] Funcionario funcionario, int id)
        {


            if (id <= 0)
                return StatusCode(422, new { mensagem = "O id inserido na URL não é válido" });

            Funcionario funcionarioAtualizado;

            try
            {
                funcionarioAtualizado = await _funcionarioRepositorio.Atualizar(funcionario, id);
                if (funcionarioAtualizado == null)
                {
                    return NotFound(new { codigo = "404", mensagem = $"Funcionário com o ID {id} não foi encontrado." });
                }

            }
            catch(DbUpdateException ex) when (ex.InnerException != null)
            {
                if (ex.InnerException.Message.Contains("UNIQUE")){
                    return StatusCode(422, new { codigo = "422", mensagem = "Já existe um funcionário cadastrado com a matrícula informada" });
                }

                return BadRequest(new {mensagem = "O funcionário não pode ser atualizado"});
            }

            return Ok(funcionarioAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarFuncionario(int id)
        {

            if (id <= 0)
                return StatusCode(422, new { mensagem = "O id inserido na URL não é válido" });

            bool deletado = await _funcionarioRepositorio.Apagar(id);
            if (!deletado)
            {
                return NotFound(new {codigo = "404", mensagem = $"Funcionário com o ID {id} não foi encontrado."});
            }
            return NoContent();
        }

    }
}
