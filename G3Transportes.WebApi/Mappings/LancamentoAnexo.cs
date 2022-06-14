using System;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Mappings
{
    public class LancamentoAnexo
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.LancamentoAnexo>(entity =>
            {
                //key
                entity.HasKey(e => new { e.IdLancamento, e.IdAnexo });

                //properties
                entity.Property(e => e.IdLancamento).IsRequired();
                entity.Property(e => e.IdAnexo).IsRequired();

                //relationships
                entity.HasOne(a => a.Lancamento).WithMany(a => a.Anexos).HasForeignKey(a => a.IdLancamento).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Anexo).WithMany(a => a.Lancamentos).HasForeignKey(a => a.IdAnexo).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
