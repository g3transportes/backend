using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class RemetenteAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.RemetenteAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdRemetente, e.IdAnexo });

                //properties
                entity.Property(e => e.IdRemetente).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Remetente).WithMany(a => a.Anexos).HasForeignKey(a => a.IdRemetente).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Remetentes).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
