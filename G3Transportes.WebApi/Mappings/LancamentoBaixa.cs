using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class LancamentoBaixa
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.LancamentoBaixa>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.IdLancamento).IsRequired();
                entity.Property(e => e.Valor).HasColumnType("double(12,2)");
                entity.Property(e => e.Observacao).HasColumnType("text");

                //relationships
                entity.HasOne(a => a.Lancamento).WithMany(a => a.Baixas).HasForeignKey(a => a.IdLancamento).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.ContaBancaria).WithMany(a => a.Baixas).HasForeignKey(a => a.IdContaBancaria).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.FormaPagamento).WithMany(a => a.Baixas).HasForeignKey(a => a.IdFormaPagamento).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
