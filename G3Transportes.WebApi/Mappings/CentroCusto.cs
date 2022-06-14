using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class CentroCusto
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.CentroCusto>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Referencia).HasMaxLength(250);
                entity.Property(e => e.Nome).HasMaxLength(250);
                entity.Property(e => e.Tipo).HasMaxLength(5);
                entity.Property(e => e.Descricao).HasColumnType("text");

                //relationships
            });
        }
    }
}
