using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class CentroCusto
    {
        public CentroCusto()
        {
            this.Lancamentos = new List<Lancamento>();    
        }

        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public bool UltimoNivel { get; set; }
        public bool Padrao { get; set; }
        public bool Ativo { get; set; }

        public List<Lancamento> Lancamentos { get; set; }
    }
}
