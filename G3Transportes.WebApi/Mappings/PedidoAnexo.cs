using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class PedidoAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.PedidoAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdPedido, e.IdAnexo });

                //properties
                entity.Property(e => e.IdPedido).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Pedido).WithMany(a => a.Anexos).HasForeignKey(a => a.IdPedido).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Pedidos).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
