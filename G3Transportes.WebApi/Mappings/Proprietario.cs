using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Proprietario
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Proprietario>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.Nome).HasMaxLength(250);
                entity.Property(e => e.Documento).HasMaxLength(250);
                entity.Property(e => e.Documento2).HasMaxLength(250);
                entity.Property(e => e.Antt).HasMaxLength(250);
                entity.Property(e => e.Tipo).HasMaxLength(250);
                entity.Property(e => e.Pis).HasMaxLength(250);
                entity.Property(e => e.Filiacao).HasMaxLength(250);
                entity.Property(e => e.Telefone1).HasMaxLength(250);
                entity.Property(e => e.Telefone2).HasMaxLength(250);

                entity.Property(e => e.EndRua).HasMaxLength(250);
                entity.Property(e => e.EndNumero).HasMaxLength(250);
                entity.Property(e => e.EndComplemento).HasMaxLength(250);
                entity.Property(e => e.EndBairro).HasMaxLength(250);
                entity.Property(e => e.EndCidade).HasMaxLength(250);
                entity.Property(e => e.EndEstado).HasMaxLength(250);
                entity.Property(e => e.EndCep).HasMaxLength(250);

                entity.Property(e => e.BancoNome).HasMaxLength(250);
                entity.Property(e => e.BancoAgencia).HasMaxLength(250);
                entity.Property(e => e.BancoOperacao).HasMaxLength(250);
                entity.Property(e => e.BancoConta).HasMaxLength(250);
                entity.Property(e => e.BancoTitular).HasMaxLength(250);
                entity.Property(e => e.BancoDocumento).HasMaxLength(250);

                entity.Property(e => e.Observacao).HasColumnType("text");

                //relationships
            });
        }
    }
}
