using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Pedido
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Pedido>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.IdCaminhao).IsRequired(false);
                entity.Property(e => e.IdRemetente).IsRequired(false);
                entity.Property(e => e.IdCliente).IsRequired();
                entity.Property(e => e.OrdemServico).HasMaxLength(250);
                entity.Property(e => e.NumPedido).HasMaxLength(250);
                entity.Property(e => e.Destinatario).HasColumnType("text");
                entity.Property(e => e.LocalColeta).HasColumnType("text");
                entity.Property(e => e.Observacao).HasColumnType("text");
                entity.Property(e => e.Quantidade).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorUnitario).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorBruto).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorLiquido).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorPedagio).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorAcrescimo).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorDesconto).HasColumnType("double(12,2)");
                entity.Property(e => e.FreteUnitario).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorFrete).HasColumnType("double(12,2)");
                entity.Property(e => e.ComissaoUnitario).HasColumnType("double(12,2)");
                entity.Property(e => e.ValorComissao).HasColumnType("double(12,2)");
                entity.Property(e => e.ComissaoMargem).HasColumnType("double(12,2)");
                entity.Property(e => e.CTe).HasMaxLength(250);
                entity.Property(e => e.NFe).HasMaxLength(250);
                entity.Property(e => e.Boleto).HasMaxLength(250);

                //relationships
                entity.HasOne(a => a.Cliente).WithMany(a => a.Pedidos).HasForeignKey(a => a.IdCliente).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.Caminhao).WithMany(a => a.Pedidos).HasForeignKey(a => a.IdCaminhao).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.Remetente).WithMany(a => a.Pedidos).HasForeignKey(a => a.IdRemetente).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
