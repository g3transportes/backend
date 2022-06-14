using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class ContaBancaria
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ContaBancaria>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Nome).HasMaxLength(250);
                entity.Property(e => e.Banco).HasMaxLength(250);
                entity.Property(e => e.Agencia).HasMaxLength(250);
                entity.Property(e => e.Operacao).HasMaxLength(250);
                entity.Property(e => e.Conta).HasMaxLength(250);
                entity.Property(e => e.Titular).HasMaxLength(250);
                entity.Property(e => e.Documento).HasMaxLength(250);
                entity.Property(e => e.Observacao).HasColumnType("text");
                entity.Property(e => e.SaldoInicial).HasColumnType("double(12,2)");
                entity.Property(e => e.SaldoAtual).HasColumnType("double(12,2)");
                entity.Property(e => e.Creditos).HasColumnType("double(12,2)");
                entity.Property(e => e.Debitos).HasColumnType("double(12,2)");

                //relationships
            });
        }
    }
}
