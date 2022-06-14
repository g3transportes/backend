using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class ClienteAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ClienteAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdCliente, e.IdAnexo });

                //properties
                entity.Property(e => e.IdCliente).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Cliente).WithMany(a => a.Anexos).HasForeignKey(a => a.IdCliente).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Clientes).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
