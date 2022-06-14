using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Caminhao
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Caminhao>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Nome).HasMaxLength(250);
                entity.Property(e => e.Modelo).HasMaxLength(250);
                entity.Property(e => e.Capacidade).HasColumnType("double(12,2)");
                entity.Property(e => e.Placa).HasMaxLength(250);
                entity.Property(e => e.Placa2).HasMaxLength(250);
                entity.Property(e => e.Placa3).HasMaxLength(250);
                entity.Property(e => e.Placa4).HasMaxLength(250);
                entity.Property(e => e.Renavam).HasMaxLength(250);
                entity.Property(e => e.Renavam2).HasMaxLength(250);
                entity.Property(e => e.Renavam3).HasMaxLength(250);
                entity.Property(e => e.Renavam4).HasMaxLength(250);
                entity.Property(e => e.Cidade).HasMaxLength(250);
                entity.Property(e => e.Estado).HasMaxLength(250);


                //relationships
                entity.HasOne(a => a.Motorista).WithMany(a => a.Caminhoes).HasForeignKey(a => a.IdMotorista).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Proprietario).WithMany(a => a.Caminhoes).HasForeignKey(a => a.IdProprietario).OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
