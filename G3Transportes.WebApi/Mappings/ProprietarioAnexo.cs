using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class ProprietarioAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ProprietarioAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdProprietario, e.IdAnexo });

                //properties
                entity.Property(e => e.IdProprietario).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Proprietario).WithMany(a => a.Anexos).HasForeignKey(a => a.IdProprietario).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Proprietarios).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
