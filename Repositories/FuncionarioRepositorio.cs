﻿using Aluguel.Context;
using Aluguel.Exceptions;
using Aluguel.Models;
using Aluguel.Models.RequestsModels;
using Aluguel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Repositories
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly AppDbContext _appDbContext;


        public FuncionarioRepositorio(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<Funcionario> Adicionar(Funcionario funcionario)
        {
            await _appDbContext.Funcionarios.AddAsync(funcionario);
            await _appDbContext.SaveChangesAsync();

            return funcionario;
        }


        public async Task<Funcionario> BuscarPorId(int id)
        {
            var funcionario = await _appDbContext.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
            {
                throw new IdNaoEncontradoException($"Nenhum funcionário com o id {id} foi encontrado no banco de dados.");
            }

            return funcionario;
        }

        public async Task<List<Funcionario>> BuscarTodos()
        {
            return await _appDbContext.Funcionarios.ToListAsync();
        }
        public async Task<Funcionario> Atualizar(NovoFuncionario novoFuncionario, int id)
        {
            // BuscarPorId lança uma exceção se nenhum funcionario for encontrado
            Funcionario funcionarioEncontrado = await this.BuscarPorId(id);

            funcionarioEncontrado.Nome = novoFuncionario.Nome;
            funcionarioEncontrado.Email = novoFuncionario.Email;
            funcionarioEncontrado.Idade = novoFuncionario.Idade;
            funcionarioEncontrado.Cpf = novoFuncionario.Cpf;
            funcionarioEncontrado.Funcao = novoFuncionario.Funcao;
            _appDbContext.Funcionarios.Update(funcionarioEncontrado);
            await _appDbContext.SaveChangesAsync();

            return funcionarioEncontrado;
        }
        public async Task<bool> Apagar(int id)
        {
            // BuscarPorId lança uma exceção se nenhum funcionario for encontrado
            var funcionario = await this.BuscarPorId(id);

            _appDbContext.Funcionarios.Remove(funcionario);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
