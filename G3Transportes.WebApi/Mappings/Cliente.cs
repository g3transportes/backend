using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class Cliente
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Cliente>(entity =>
            {
                //key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                //properties
                entity.Property(e => e.RazaoSocial).HasMaxLength(250);
                entity.Property(e => e.NomeFantasia).HasMaxLength(250);
                entity.Property(e => e.Documento1).HasMaxLength(250);
                entity.Property(e => e.Documento2).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Contato).HasMaxLength(250);
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
