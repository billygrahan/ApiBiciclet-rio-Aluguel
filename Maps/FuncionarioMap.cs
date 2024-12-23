using Aluguel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aluguel.Maps
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasIndex(f => f.Cpf).IsUnique();
            builder.HasIndex(f => f.Matricula).IsUnique();
            builder.Property(f => f.Funcao)
            .HasConversion(
                v => v.ToString(),
                v => (Funcao)Enum.Parse(typeof(Funcao), v));

        }
    }
}
