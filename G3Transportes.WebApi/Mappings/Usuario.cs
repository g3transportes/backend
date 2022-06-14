using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Usuario
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Nome).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Senha).HasMaxLength(250);

                //relationships
            });
        }
    }
}
