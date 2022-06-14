using System;
namespace G3Transportes.WebApi.Models
{
    public class LancamentoAnexo
    {
        public LancamentoAnexo()
        {
        }

        public int IdLancamento { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Lancamento Lancamento { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
