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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FuncionarioMap());
        base.OnModelCreating(modelBuilder);
    }
}
