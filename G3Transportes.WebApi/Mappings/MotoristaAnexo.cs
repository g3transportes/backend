using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class MotoristaAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.MotoristaAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdMotorista, e.IdAnexo });

                //properties
                entity.Property(e => e.IdMotorista).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Motorista).WithMany(a => a.Anexos).HasForeignKey(a => a.IdMotorista).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Motoristas).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
