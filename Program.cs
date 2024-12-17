using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Aluguel.Repositories.Interfaces;
using Aluguel.Repositories;
using Aluguel.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string postgreSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(postgreSqlConnection));

// Registra a interface IFuncionarioRepositorio com a implementa��o FuncionarioRepositorio no cont�iner de DI.
// Define o ciclo de vida como Scoped (uma inst�ncia por requisi��o HTTP).
// Permite a inje��o de depend�ncia em controladores, servi�os e outras classes.
builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
