using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Lancamento
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Lancamento>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.IdFormaPagamento).IsRequired();
                entity.Property(e => e.Tipo).HasMaxLength(5);
                entity.Property(e => e.Favorecido).HasMaxLength(250);
                entity.Property(e => e.Observacao).HasColumnType("text");
                entity.Property(e => e.ValorBruto).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorDesconto).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorAcrescimo).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorLiquido).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorBaixado).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorSaldo).HasColumnType("double(12,2)");
                entity.Property(e => e.BancoNome).HasMaxLength(250);
                entity.Property(e => e.BancoAgencia).HasMaxLength(250);
                entity.Property(e => e.BancoOperacao).HasMaxLength(250);
                entity.Property(e => e.BancoConta).HasMaxLength(250);
                entity.Property(e => e.BancoTitular).HasMaxLength(250);
                entity.Property(e => e.BancoDocumento).HasMaxLength(250);

                //relationships
                entity.HasOne(a => a.Pedido).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdPedido).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Cliente).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdCliente).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Caminhao).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdCaminhao).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.FormaPagamento).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdFormaPagamento).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.ContaBancaria).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdContaBancaria).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.CentroCusto).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdCentroCusto).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.TipoDocumento).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdTipoDocumento).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
