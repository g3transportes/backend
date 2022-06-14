using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class FormaPagamento
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.FormaPagamento>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Nome).HasMaxLength(250);

                //relationships
            });
        }
    }
}
