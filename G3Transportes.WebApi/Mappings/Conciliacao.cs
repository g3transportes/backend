using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Conciliacao
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Conciliacao>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.IdConta).IsRequired();
                entity.Property(e => e.Anexo).HasMaxLength(250);
                entity.Property(e => e.Saldo).HasColumnType("double(12,2)");


                //relationships
                entity.HasOne(a => a.Conta).WithMany(a => a.Conciliacoes).HasForeignKey(a => a.IdConta).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
