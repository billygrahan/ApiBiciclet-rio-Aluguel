using ApiAluguel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiAluguel.Maps
{
    public class AluguelMap : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Ativo).HasDatabaseName("idx_aluguel_ativo");
        }
    }
}
