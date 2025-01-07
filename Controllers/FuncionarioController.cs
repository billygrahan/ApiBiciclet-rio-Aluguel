using Aluguel.Exceptions;
using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories.Interfaces;
using Aluguel.Tools;
using Aluguel.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;

    private readonly FuncionarioValidador _funcionarioValidador;

    public FuncionarioController(IFuncionarioRepositorio funcionarioRepositorio, FuncionarioValidador funcionarioValidador)
    {
        _funcionarioRepositorio = funcionarioRepositorio;
        _funcionarioValidador = funcionarioValidador;
    }

    [HttpGet]
    public async Task<ActionResult<List<Funcionario>>> BuscarTodosFuncionarios()
    {
        List<Funcionario> funcionarios = await _funcionarioRepositorio.BuscarTodos();
        return Ok(funcionarios);
    }

    [HttpGet("{idFuncionario}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
    public async Task<ActionResult<Funcionario>> BuscarFuncionarioPorId(int idFuncionario)
    {

        if (idFuncionario <= 0)
            return StatusCode(422, new List<Erro> { new Erro("422", "O id inserido não é válido") });


        try
        {
            Funcionario funcionario = await _funcionarioRepositorio.BuscarPorId(idFuncionario);
            return Ok(funcionario);
        }
        catch (IdNaoEncontradoException ex)
        {
            return NotFound(new Erro("404", ex.Message));
        }
     

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
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
            List<Erro> listaErros = await _funcionarioValidador.GerarListaErros(false, novofuncionario);

            if (listaErros.Count > 0)
                return StatusCode(422, listaErros);

            await _funcionarioRepositorio.Adicionar(funcionario);


            // Retorna um Status 201 e um header Location com a URL para acessar o funcionario cadastrado 
            // (LINHA ANTIGA) return CreatedAtAction(nameof(BuscarFuncionarioPorId), new { id = funcionario.Id }, funcionario);

            // Retornei um 200 para ficar igual ao swagger modelo

            return Ok(funcionario);
        }
        catch (DbUpdateException ex) when (ex.InnerException != null)
        {
            // DbUpdateException é lançada por SaveChanges() no repositório e encapsula uma
            // exceção específica (InnerException) do BD usado

            return BadRequest(new { mensagem = "O funcionário não pode ser cadastrado" });

        }



    }

    [HttpPut("{idFuncionario}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
    public async Task<ActionResult<Funcionario>> AtualizarFuncionario([FromBody] NovoFuncionario novofuncionario, int idFuncionario)
    {

        Funcionario funcionarioAtualizado;

        try
        {
            novofuncionario.Id = idFuncionario;
            List<Erro> listaErros = await _funcionarioValidador.GerarListaErros(true, novofuncionario);

            if (listaErros.Count > 0)
                return StatusCode(422, listaErros);

            funcionarioAtualizado = await _funcionarioRepositorio.Atualizar(novofuncionario, idFuncionario);
            return Ok(funcionarioAtualizado);

        }
        catch (IdNaoEncontradoException ex)
        {
            return NotFound(new Erro("404", ex.Message));
        }
        catch (DbUpdateException ex) when (ex.InnerException != null)
        {
            return BadRequest(new {mensagem = "O funcionário não pode ser atualizado"});
        }

    }

    [HttpDelete("{idFuncionario}")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<Erro>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeletarFuncionario(int idFuncionario)
    {

        if (idFuncionario <= 0)
            return StatusCode(422, new List<Erro> { new Erro("422", "O id inserido não é válido") });

        try
        {
            await _funcionarioRepositorio.Apagar(idFuncionario);
            return Ok();
        }catch(IdNaoEncontradoException ex)
        {
            return NotFound(new Erro("404",ex.Message));
        }
    }

}
