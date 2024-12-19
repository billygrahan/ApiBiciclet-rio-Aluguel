using Aluguel.Maps;
using Aluguel.Models;
using Microsoft.EntityFrameworkCore;


namespace Aluguel.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Funcionario> Funcionarios {  get; set; }
    public DbSet<Ciclista> Ciclistas { get; set; }
    public DbSet<CartaoDeCredito> CartoesDeCreditos { get; set; }
    public DbSet<Devolucao> Devolucoes { get; set; }
    public DbSet<Models.Aluguel> Alugueis { get; set; }
    public DbSet<Passaporte> Passaportes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FuncionarioMap());
        base.OnModelCreating(modelBuilder);
    }
}
