using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class RemetenteEstoque
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.RemetenteEstoque>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.IdRemetente).IsRequired();
                entity.Property(e => e.Tipo).HasMaxLength(5);
                entity.Property(e => e.Descricao).HasColumnType("text");
                entity.Property(e => e.Usuario).HasMaxLength(250);

                //relationships
                entity.HasOne(a => a.Remetente).WithMany(a => a.Estoques).HasForeignKey(a => a.IdRemetente).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Pedido).WithMany(a => a.Estoques).HasForeignKey(a => a.IdPedido).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
