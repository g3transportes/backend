using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class CaminhaoAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.CaminhaoAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdCaminhao, e.IdAnexo });

                //properties
                entity.Property(e => e.IdCaminhao).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Caminhao).WithMany(a => a.Anexos).HasForeignKey(a => a.IdCaminhao).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Caminhoes).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
