using ApiAluguel.Maps;
using ApiAluguel.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiAluguel.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Funcionario> Funcionarios {  get; set; }
    public DbSet<Ciclista> Ciclistas { get; set; }
    public DbSet<Passaporte> Passaporte { get; set; }
    public DbSet<CartaoDeCredito> CartoesDeCreditos { get; set; }
    public DbSet<Devolucao> Devolucoes { get; set; }
    public DbSet<Aluguel> Alugueis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FuncionarioMap());
        modelBuilder.ApplyConfiguration(new AluguelMap());
        base.OnModelCreating(modelBuilder);
    }
}
