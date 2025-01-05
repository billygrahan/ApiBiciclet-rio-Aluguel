using Microsoft.EntityFrameworkCore;

using Aluguel.Repositories.Interfaces;
using Aluguel.Repositories;
using Aluguel.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configurando Swagger para detectar automaticamente os atributos n�o nulos
builder.Services.AddSwaggerGen(c => c.SupportNonNullableReferenceTypes());


string postgreSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(postgreSqlConnection));

// Registra a interface IFuncionarioRepositorio com a implementa��o FuncionarioRepositorio no cont�iner de inje��o de depend�ncias.
// Define o ciclo de vida como Scoped (uma inst�ncia por requisi��o HTTP).
// Permite a inje��o de depend�ncia em controladores, servi�os e outras classes.
builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<ICiclistaRepositorio, CiclistaRepositorio>();
builder.Services.AddScoped<ICartaodeCreditoRepositorio, CartaodeCreditoRepositorio>();
builder.Services.AddScoped<IPassaporteRepositorio, PassaporteRepositorio>();


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
