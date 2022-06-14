using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            this.Lancamentos = new List<Lancamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public virtual List<Lancamento> Lancamentos { get; set; }
    }
}
